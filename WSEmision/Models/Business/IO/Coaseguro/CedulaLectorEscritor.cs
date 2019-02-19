using System.Collections.Generic;

using WSEmision.Models.Business.Extensions.Coaseguro;
using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.DTO.Coaseguro;

namespace WSEmision.Models.Business.IO.Coaseguro
{
    /// <summary>
    /// Lee y genera los reportes para la Cédula de Participación
    /// en Coaseguro en formato PDF.
    /// </summary>
    public class CedulaLectorEscritor : LatexLectorEscritor
    {
        /// <summary>
        /// Contiene los datos de la Cédula de Participación en Coaseguro.
        /// </summary>
        private CedulaParticipacionCoaseguroResultSet cedula;

        /// <summary>
        /// Contiene los datos del encabezado del reporte.
        /// </summary>
        private EncabezadoReportesEmisionResultSet encabezado;

        /// <summary>
        /// Genera una nueva instancia con la cédula y el encabezado indicados.
        /// </summary>
        /// <param name="cedula">Los datos de la Cédula de Participación en Coaseguro.</param>
        /// <param name="encabezado">Los datos del encabezado del reporte.</param>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public CedulaLectorEscritor(
            CedulaParticipacionCoaseguroResultSet cedula,
            EncabezadoReportesEmisionResultSet encabezado,
            string rutaCompilador,
            string inputDir)
        : base(rutaCompilador, inputDir)
        {
            this.cedula = cedula;
            this.encabezado = encabezado;
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante
        /// sobre la Cédula de Participación.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected override void RellenarPlantilla(IList<string> plantilla)
        {
            int indice;
            var polizaCompuesta = encabezado.Poliza.Split('-');

            // Header
            indice = this.RellenarEncabezado(plantilla, encabezado, 0);

            // Cédula de Participación
            this.RellenarCedulaParticipacion(plantilla, cedula, indice);
        }
    }
}