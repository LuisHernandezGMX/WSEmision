using System;
using System.Configuration;
using System.IO;

using WSEmision.Models.DAL.DAO.Coaseguro;
using WSEmision.Models.Business.IO;
using WSEmision.Models.Business.IO.Coaseguro;

namespace WSEmision.Models.Business.Service.Coaseguro
{
    /// <summary>
    /// Contiene métodos de utilería para el módulo de Coaseguro de esta aplicación.
    /// </summary>
    public class CoaseguroService
    {
        #region Variables Privadas
        /// <summary>
        /// La ruta absoluta al directorio con las plantillas y ejecutables de LaTex (distribución MikTex).
        /// </summary>
        private static string rutaLatex = ConfigurationManager.AppSettings["DirectorioLatex"];

        /// <summary>
        /// La ruta absoluta al compilador de LaTex.
        /// </summary>
        private static string rutaEjecutable = ConfigurationManager.AppSettings["RutaXelatex"];

        /// <summary>
        /// La ruta absoluta al directorio de multimedia.
        /// </summary>
        private static string inputDir = ConfigurationManager.AppSettings["DirectorioEntradaLatex"];

        /// <summary>
        /// Indica el tipo de reporte que se va a generar.
        /// </summary>
        private enum TipoReporteCoaseguro
        {
            /// <summary>
            /// Se genera el reporte únicamente
            /// con la Cédula de Participación.
            /// </summary>
            CedulaParticipacion,

            /// <summary>
            /// Se genera el reporte únicamente con
            /// el Anexo de Condiciones Particulares.
            /// </summary>
            AnexoCondicionesParticulares,

            /// <summary>
            /// Se genera el reporte con ambos apartados.
            /// </summary>
            Ambos
        }
        #endregion Variables Privadas

        /// <summary>
        /// Genera el reporte PDF con la Cédula de Participación y el
        /// Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="IdPv">El Id de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarCedulaYAnexo(int idPv, string rutaPlantilla)
        {
            return GenerarReporte(idPv, rutaPlantilla, TipoReporteCoaseguro.Ambos);
        }

        /// <summary>
        /// Genera el reporte PDF únicamente con la Cédula de Participación.
        /// </summary>
        /// <param name="IdPv">El Id de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarCedula(int idPv, string rutaPlantilla)
        {
            return GenerarReporte(idPv, rutaPlantilla, TipoReporteCoaseguro.CedulaParticipacion);
        }

        /// <summary>
        /// Genera el reporte PDF únicamente con el Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="IdPv">El Id de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarAnexo(int idPv, string rutaPlantilla)
        {
            return GenerarReporte(idPv, rutaPlantilla, TipoReporteCoaseguro.AnexoCondicionesParticulares);
        }

        /// <summary>
        /// Genera el reporte PDF indicado.
        /// </summary>
        /// <param name="idPv">El Id de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <param name="tipo">El tipo de reporte que se quiere generar.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        private static byte[] GenerarReporte(int idPv, string rutaPlantilla, TipoReporteCoaseguro tipo)
        {
            var outputDir = Path.Combine(rutaLatex, Guid.NewGuid().ToString());
            var latexIO = ObtenerLectorEscritor(idPv, tipo);
            var plantilla = latexIO.LeerPlantilla(rutaPlantilla);

            latexIO.GenerarReporte(plantilla, outputDir);
            var pdf = latexIO.LeerReporte(outputDir);
            Directory.Delete(outputDir, true);

            return pdf;
        }

        /// <summary>
        /// Regresa una nueva instancia de <see cref="LatexLectorEscritor"/> dependiendo
        /// del tipo requerido.
        /// </summary>
        /// <param name="idPv">El Id de la póliza en coaseguro.</param>
        /// <param name="tipo">El tipo de reporte que se quiere generar.</param>
        /// <returns>Una nueva instancia de <see cref="LatexLectorEscritor"/>.</returns>
        private static LatexLectorEscritor ObtenerLectorEscritor(int idPv, TipoReporteCoaseguro tipo)
        {
            using (var dao = new CoaseguroDao()) {
                var encabezado = dao.ObtenerEncabezado(idPv);

                switch (tipo) {
                    case TipoReporteCoaseguro.CedulaParticipacion:
                        var datosCedula = dao.ObtenerCedulaParticipacion(idPv);
                        return new CedulaLectorEscritor(datosCedula, encabezado, rutaEjecutable, inputDir);

                    case TipoReporteCoaseguro.AnexoCondicionesParticulares:
                        var datosAnexo = dao.ObtenerAnexoCondicionesParticulares(idPv);
                        return new AnexoLectorEscritor(datosAnexo, encabezado, rutaEjecutable, inputDir);

                    case TipoReporteCoaseguro.Ambos:
                    default:
                        var cedula = dao.ObtenerCedulaParticipacion(idPv);
                        var anexo = dao.ObtenerAnexoCondicionesParticulares(idPv);

                        return new CedulaAnexoLectorEscritor(cedula, anexo, encabezado, rutaEjecutable, inputDir);
                }
            }
        }
    }
}