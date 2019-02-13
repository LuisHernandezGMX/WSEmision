using System.Collections.Generic;

using WSEmision.Models.DAL.DTO.Coaseguro;

namespace WSEmision.Models.Business.IO.Coaseguro
{
    /// <summary>
    /// Lee y genera los reportes para la Cédula de Participación
    /// en Coaseguro y el Anexo de Condiciones Particulares
    /// en un solo archivo PDF.
    /// </summary>
    public sealed class CedulaAnexoLectorEscritor : LatexLectorEscritor
    {
        #region Variables Privadas
        /// <summary>
        /// Contiene los datos de la Cédula de Participación en Coaseguro.
        /// </summary>
        private CedulaParticipacionCoaseguroResultSet cedula;

        /// <summary>
        /// Contiene los datos del Anexo y Condiciones Particulares en Coaseguro.
        /// </summary>
        private AnexoCondicionesParticularesCoaseguroResultSet anexo;

        /// <summary>
        /// Contiene los datos del encabezado del reporte.
        /// </summary>
        private EncabezadoReportesEmisionResultSet encabezado;
        #endregion

        /// <summary>
        /// Genera una nueva instancia con la cédula, el anexo y el encabezado indicados.
        /// </summary>
        /// <param name="cedula">Los datos de la Cédula de Participación en Coaseguro.</param>
        /// <param name="anexo">Los datos del Anexo y Condiciones Particulares en Coaseguro.</param>
        /// <param name="encabezado">Los datos del encabezado del reporte.</param>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public CedulaAnexoLectorEscritor(
            CedulaParticipacionCoaseguroResultSet cedula,
            AnexoCondicionesParticularesCoaseguroResultSet anexo,
            EncabezadoReportesEmisionResultSet encabezado,
            string rutaCompilador,
            string inputDir)
        : base(rutaCompilador, inputDir)
        {
            this.cedula = cedula;
            this.anexo = anexo;
            this.encabezado = encabezado;
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante sobre la Cédula
        /// de Participación y el Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected override void RellenarPlantilla(IList<string> plantilla)
        {
            int indice;
            var polizaCompuesta = encabezado.Poliza.Split('-');

            // Header
            indice = this.RellenarEncabezado(plantilla, encabezado, 0);

            // Cédula de Participación
            indice = this.RellenarCedulaParticipacion(plantilla, cedula, indice);

            // Anexo y Condiciones Particulares
            indice = this.RellenarAnexoCondiciones(plantilla, anexo, polizaCompuesta[2], indice);
        }
    }
}