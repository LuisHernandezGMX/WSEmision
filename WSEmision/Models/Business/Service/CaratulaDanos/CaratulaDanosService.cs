using System;
using System.Configuration;
using System.IO;

using WSEmision.Models.Business.IO;
using WSEmision.Models.Business.IO.CaratulaDanos;
using WSEmision.Models.DAL.DAO.CaratulaDanos;

namespace WSEmision.Models.Business.Service.CaratulaDanos
{
    /// <summary>
    /// Contiene métodos de utilería para el módulo de 
    /// Carátula de Daños de esta aplicación.
    /// </summary>
    public static class CaratulaDanosService
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
        #endregion

        /// <summary>
        /// Genera el reporte PDF con la Carátula de Daños.
        /// </summary>
        /// <param name="idPv">El Id de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarCaratula(int idPv, string rutaPlantilla)
        {
            LatexLectorEscritor latexIO;

            using (var dao = new CaratulaDanosDao()) {
                var caratula = dao.ObtenerCaratulaDanos(idPv);
                var encabezado = dao.ObtenerEncabezado(idPv);

                latexIO = new DanosLectorEscritor(encabezado, caratula, rutaEjecutable, inputDir);
            }

            var outputDir = Path.Combine(rutaLatex, Guid.NewGuid().ToString());
            var plantilla = latexIO.LeerPlantilla(rutaPlantilla);

            latexIO.GenerarReporte(plantilla, outputDir);
            var pdf = latexIO.LeerReporte(outputDir);
            Directory.Delete(outputDir, true);

            return pdf;
        }
    }
}