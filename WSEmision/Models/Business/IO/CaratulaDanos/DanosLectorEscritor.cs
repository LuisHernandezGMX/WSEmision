using System.Collections.Generic;

using WSEmision.Models.Business.Extensions;
using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.DTO.CaratulaDanos;

namespace WSEmision.Models.Business.IO.CaratulaDanos
{
    /// <summary>
    /// Lee y genera los reportes para la Carátula de Daños
    /// </summary>
    public class DanosLectorEscritor : LatexLectorEscritor
    {
        /// <summary>
        /// Contiene los datos del encabezado del reporte.
        /// </summary>
        private EncabezadoReportesEmisionResultSet encabezado;

        /// <summary>
        /// Contiene los datos de la carátula.
        /// </summary>
        private CaratulaDanosResultSet caratula;

        /// <summary>
        /// Genera una nueva instancia con el encabezado indicado.
        /// </summary>
        /// <param name="encabezado">Los datos del encabezado del reporte.</param>
        /// <param name="caratula">Los datos de la carátula del reporte.</param>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public DanosLectorEscritor(
            EncabezadoReportesEmisionResultSet encabezado,
            CaratulaDanosResultSet caratula,
            string rutaCompilador,
            string inputDir)
        : base(rutaCompilador, inputDir)
        {
            this.encabezado = encabezado;
            this.caratula = caratula;
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante sobre la Carátula de Daños.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected override void RellenarPlantilla(IList<string> plantilla)
        {
            int indice;
            var polizaCompuesta = encabezado.Poliza.Split('-');

            // Encabezado
            indice = plantilla.FindIndex(linea => linea.Contains("<TIPO-ENDO>"));
            plantilla[indice] = plantilla[indice]
                .Replace("<TIPO-ENDO>", encabezado.TipoEndoso)
                .Replace("<TIPO-POLIZA>", encabezado.TipoPoliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<DESC-RAMO-COMERCIAL>"), indice);
            plantilla[indice] = plantilla[indice].Replace("<DESC-RAMO-COMERCIAL>", encabezado.RamoComercial);

            indice = plantilla.FindIndex(linea => linea.Contains("<SUC-COD-RAMO-POLIZA-ENDO-SUF>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<SUC-COD-RAMO-POLIZA-ENDO-SUF>", encabezado.Poliza);

            // Encabezado de la carátula
            indice = plantilla.FindIndex(linea => linea.Contains("<COD-SUC>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<COD-SUC>", polizaCompuesta[0])
                .Replace("<OFICINA>", caratula.Oficina)
                .Replace("<COD-RAMO>", polizaCompuesta[1])
                .Replace("<POLIZA>", polizaCompuesta[2])
                .Replace("<ENDO>", polizaCompuesta[3])
                .Replace("<SUF>", polizaCompuesta[4]);

            // Datos del asegurado
            indice = plantilla.FindIndex(linea => linea.Contains("<NOMBRE>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<NOMBRE>", caratula.Asegurado.Contratante);

            indice = plantilla.FindIndex(linea => linea.Contains("<CALLE>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<CALLE>", caratula.Asegurado.Domicilio.Calle)
                .Replace("<NUMERO>", caratula.Asegurado.Domicilio.Numero)
                .Replace("<INTERIOR>", caratula.Asegurado.Domicilio.Interior)
                .Replace("<COLONIA>", caratula.Asegurado.Domicilio.Colonia)
                .Replace("<POBLACION>", caratula.Asegurado.Domicilio.Poblacion)
                .Replace("<CIUDAD>", caratula.Asegurado.Domicilio.Ciudad == "NO APLICA" ? string.Empty : caratula.Asegurado.Domicilio.Ciudad)
                .Replace("<ESTADO>", caratula.Asegurado.Domicilio.Estado)
                .Replace("<CP>", caratula.Asegurado.Domicilio.CP);

            indice = plantilla.FindIndex(linea => linea.Contains("<RFC>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<RFC>", caratula.Asegurado.RFC);

            indice = plantilla.FindIndex(linea => linea.Contains("<FECHA-NACIMIENTO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<FECHA-NACIMIENTO>", caratula.Asegurado.FechaNacimiento);

            // Agente
            indice = plantilla.FindIndex(linea => linea.Contains("<AGENTES>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<AGENTES>", caratula.Agentes);

            // Vigencia de la póliza
            indice = plantilla.FindIndex(linea => linea.Contains("<VIGENCIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<VIGENCIA>",  caratula.InfoPoliza.Vigencia.ToString());

            indice = plantilla.FindIndex(linea => linea.Contains("<DIA1>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<DIA1>", caratula.InfoPoliza.Dia1)
                .Replace("<MES1>", caratula.InfoPoliza.Mes1)
                .Replace("<ANO1>", caratula.InfoPoliza.Ano1.ToString())
                .Replace("<HORADESDE>", caratula.InfoPoliza.HoraDesde);

            indice = plantilla.FindIndex(linea => linea.Contains("<DIA2>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<DIA2>", caratula.InfoPoliza.Dia2)
                .Replace("<MES2>", caratula.InfoPoliza.Mes2)
                .Replace("<ANO2>", caratula.InfoPoliza.Ano2.ToString())
                .Replace("<HORAHASTA>", caratula.InfoPoliza.HoraHasta);

            indice = plantilla.FindIndex(linea => linea.Contains("<DIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<DIA>", caratula.InfoPoliza.Dia)
                .Replace("<MES>", caratula.InfoPoliza.Mes)
                .Replace("<ANO>", caratula.InfoPoliza.Ano.ToString());

            indice = plantilla.FindIndex(linea => linea.Contains("<MONEDA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<MONEDA>", caratula.InfoPoliza.Moneda);

            indice = plantilla.FindIndex(linea => linea.Contains("<PAGO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<PAGO>", caratula.InfoPoliza.FormaPago);

            // Importes
            indice = plantilla.FindIndex(linea => linea.Contains("<PRIMA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<PRIMA>", caratula.Importes.PrimaNeta.Replace("$", "\\$"))
                .Replace("<IMPORTEFRAC>", caratula.Importes.Recargo.Replace("$", "\\$"))
                .Replace("<GASTOS>", caratula.Importes.Derecho.Replace("$", "\\$"))
                .Replace("<IMPORTEIVA>", caratula.Importes.IVA.Replace("$", "\\$"))
                .Replace("<TOTAL>", caratula.Importes.Total.Replace("$", "\\$"));

            // Desc por ramo
            indice = plantilla.FindIndex(linea => linea.Contains("<DESC-POR-RAMO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<DESC-POR-RAMO>",  caratula.DescPorRamo);
        }
    }
}