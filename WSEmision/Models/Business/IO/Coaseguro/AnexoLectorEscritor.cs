using System.Collections.Generic;

using WSEmision.Models.Business.Extensions.Coaseguro;
using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.DTO.Coaseguro;

/// <summary>
/// Lee y genera los reportes para la Cédula de Participación
/// en Coaseguro y el Anexo de Condiciones Particulares
/// en un solo archivo PDF.
namespace WSEmision.Models.Business.IO.Coaseguro
{
    public class AnexoLectorEscritor : LatexLectorEscritor
    {
        /// <summary>
        /// Contiene los datos del Anexo y Condiciones Particulares en Coaseguro.
        /// </summary>
        private AnexoCondicionesParticularesCoaseguroResultSet anexo;

        /// <summary>
        /// Contiene los datos del encabezado del reporte.
        /// </summary>
        private EncabezadoReportesEmisionResultSet encabezado;

        /// <summary>
        /// Genera una nueva instancia con el anexo y el encabezado indicados.
        /// </summary>
        /// <param name="anexo">Los datos del Anexo y Condiciones Particulares en Coaseguro.</param>
        /// <param name="encabezado">Los datos del encabezado del reporte.</param>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public AnexoLectorEscritor(
            AnexoCondicionesParticularesCoaseguroResultSet anexo,
            EncabezadoReportesEmisionResultSet encabezado,
            string rutaCompilador,
            string inputDir)
        : base(rutaCompilador, inputDir)
        {
            this.anexo = anexo;
            this.encabezado = encabezado;
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante sobre el Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected override void RellenarPlantilla(IList<string> plantilla)
        {
            int indice;
            var polizaCompuesta = encabezado.Poliza.Split('-');

            // Header
            indice = this.RellenarEncabezado(plantilla, encabezado, 0);

            // Anexo y Condiciones Particulares
            this.RellenarAnexoCondiciones(plantilla, anexo, polizaCompuesta[2], indice);
        }
    }
}