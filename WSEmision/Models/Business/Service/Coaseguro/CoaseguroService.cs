using System;
using System.IO;

using WSEmision.Models.DAL.DAO.Coaseguro;
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
        private static string rutaLatex = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Latex");

        /// <summary>
        /// La ruta absoluta al compilador de LaTex.
        /// </summary>
        private static string rutaEjecutable = Path.Combine(rutaLatex, @"MikTex\texmfs\install\miktex\bin\miktex-xelatex.exe");

        /// <summary>
        /// La ruta absoluta al directorio de multimedia.
        /// </summary>
        private static string inputDir = Path.Combine(rutaLatex, "media");
        #endregion Variables Privadas

        /// <summary>
        /// Genera el reporte en PDF de la Cédula de Participación y el Anexo de
        /// Condiciones del Coaseguro indicado.
        /// </summary>
        /// <param name="idPv">El ID de la póliza en coaseguro.</param>
        /// <param name="rutaPlantilla">La ruta absoluta a la plantilla del reporte.</param>
        /// <returns>Los bytes del reporte generado en PDF.</returns>
        public static byte[] GenerarCedulaParticipacionAnexoCondiciones(int idPv, string rutaPlantilla)
        {
            var datosCedula = CoaseguroDao.ObtenerCedulaParticipacion(idPv);
            var datosAnexo = CoaseguroDao.ObtenerAnexoCondicionesParticulares(idPv);

            var outputDir = Path.Combine(rutaLatex, Guid.NewGuid().ToString());
            var latexIO = new CedulaAnexoLectorEscritor(datosCedula, datosAnexo, rutaEjecutable, inputDir);
            var plantilla = latexIO.LeerPlantilla(rutaPlantilla);

            latexIO.GenerarReporte(plantilla, outputDir);
            var pdf = latexIO.LeerReporte(outputDir);
            Directory.Delete(outputDir, true);

            return pdf;
        }
    }
}