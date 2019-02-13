using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using WSEmision.Models.Business.Extensions;
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
            indice = plantilla.FindIndex(linea => linea.Contains("<TIPO-ENDO>"));
            plantilla[indice] = plantilla[indice]
                .Replace("<TIPO-ENDO>", encabezado.TipoEndoso)
                .Replace("<TIPO-POLIZA>", encabezado.TipoPoliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<DESC-RAMO-COMERCIAL>"), indice);
            plantilla[indice] = plantilla[indice].Replace("<DESC-RAMO-COMERCIAL>", encabezado.RamoComercial);

            indice = plantilla.FindIndex(linea => linea.Contains("<SUC-COD-RAMO-POLIZA-ENDO-SUF>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<SUC-COD-RAMO-POLIZA-ENDO-SUF>", cedula.DatosGenerales.Poliza);

            // Cédula de Participación
            indice = plantilla.FindIndex(linea => linea.Contains("<SUC-COD-RAMO-POLIZA-ENDO-SUF>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<SUC-COD-RAMO-POLIZA-ENDO-SUF>", cedula.DatosGenerales.Poliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<ASEGURADO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<ASEGURADO>", cedula.DatosGenerales.Asegurado);

            indice = plantilla.FindIndex(linea => linea.Contains("<CEDULA-TABLA-COASEGURADORAS>"), indice);
            plantilla[indice] = ObtenerTablaCoaseguradorasCedulaParticipacion();

            indice = plantilla.FindIndex(linea => linea.Contains("<CEDULA-DIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<CEDULA-DIA>", cedula.DatosGenerales.FechaEmision.Day.ToString().PadLeft(2, '0'))
                .Replace("<CEDULA-MES>", cedula.DatosGenerales.FechaEmision.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")))
                .Replace("<CEDULA-ANIO>", cedula.DatosGenerales.FechaEmision.Year.ToString());

            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-FIRMAS>"), indice);
            plantilla[indice] = ObtenerTablaRepresentantes();
        }

        /// <summary>
        /// Regresa el código para la tabla de coaseguradoras con sus respectivos porcentajes y montos
        /// de participación para la sección de Cédula de Participación.
        /// </summary>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private string ObtenerTablaCoaseguradorasCedulaParticipacion()
        {
            var builder = new StringBuilder($@"GRUPO MEXICANO DE SEGUROS, S.A. DE C.V. & Líder & ")
                .Append($@"{cedula.DatosGenerales.PorcentajeGMX}\% & ")
                .AppendLine($@"\$ {cedula.DatosGenerales.MontoParticipacionGMX.ToString("N2")}\\\hline");

            foreach (var coas in cedula.Coaseguradoras) {
                builder
                    .Append($@"{coas.Coaseguradora} & Seguidor & ")
                    .Append($@"{coas.PorcentajeParticipacion}\% & ")
                    .AppendLine($@"\$ {coas.MontoParticipacion.ToString("N2")}\\\hline");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Regresa el código para la tabla de las firmas de los representantes legales de cada coaseguradora
        /// para la sección de Cédula de Participación y Anexo de Condiciones Específicas.
        /// </summary>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private string ObtenerTablaRepresentantes()
        {
            var builder = new StringBuilder();
            var coas = cedula.Coaseguradoras;
            var numCoas = coas.Count();

            for (int i = 0; i < numCoas; i++) {
                builder
                    .AppendLine($@"\textbf{{{coas.ElementAt(i).Coaseguradora}}} &\\")
                    .Append(@"Nombre y Firma Representante Legal & \underline{\hspace{5cm}}")
                    .AppendLine((i < numCoas - 1) ? @"\\\\\\\\" : string.Empty);
            }

            return builder.ToString();
        }
    }
}