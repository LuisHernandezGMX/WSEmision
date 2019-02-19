using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using WSEmision.Models.Business.IO;
using WSEmision.Models.DAL.DTO;
using WSEmision.Models.DAL.DTO.Coaseguro;

namespace WSEmision.Models.Business.Extensions.Coaseguro
{
    public static class CoaseguroExtensions
    {
        /// <summary>
        /// Agrega el encabezado a la plantilla de LaTeX indicada. Regresa el último índice de fila
        /// utilizado (actualiza el índice constantemente para buscar los placeholders).
        /// </summary>
        /// <typeparam name="T">El tipo de dato del lector escritor. Este método debería ser utilizado únicamente con los lectores/escritores de este espacio de nombres.</typeparam>
        /// <param name="lectorEscritor">El lector/escritor que utilizará este método para rellenar su plantilla correspondiente.</param>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="encabezado">Los datos del encabezado.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns>El último índice de fila utilizado.</returns>
        public static int RellenarEncabezado<T>(
            this T lectorEscritor,
            IList<string> plantilla,
            EncabezadoReportesEmisionResultSet encabezado,
            int indice)
        where T : LatexLectorEscritor
        {
            return RellenarEncabezadoGenerico(plantilla, encabezado, indice);
        }

        /// <summary>
        /// Agrega la Cédula de Participación a la plantilla de LaTeX indicada. Regresa el último índice de fila
        /// utilizado (actualiza el índice constantemente para buscar los placeholders).
        /// </summary>
        /// <typeparam name="T">El tipo de dato del lector escritor. Este método debería ser utilizado únicamente con los lectores/escritores de este espacio de nombres.</typeparam>
        /// <param name="lectorEscritor">El lector/escritor que utilizará este método para rellenar su plantilla correspondiente.</param>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="cedula">Los datos de la cédula de participación.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns>El último índice de fila utilizado.</returns>
        public static int RellenarCedulaParticipacion<T>(
            this T lectorEscritor,
            IList<string> plantilla,
            CedulaParticipacionCoaseguroResultSet cedula,
            int indice)
        where T : LatexLectorEscritor
        {
            return RellenarCedulaParticipacionGenerico(plantilla, cedula, indice);
        }

        /// <summary>
        /// Agrega el Anexo de Condiciones Particulares a la plantilla de LaTeX indicada. Regresa el
        /// último índice de fila utilizado (actualiza el índice constantemente para buscar los placeholders).
        /// </summary>
        /// <typeparam name="T">El tipo de dato del lector escritor. Este método debería ser utilizado únicamente con los lectores/escritores de este espacio de nombres.</typeparam>
        /// <param name="lectorEscritor">El lector/escritor que utilizará este método para rellenar su plantilla correspondiente.</param>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <param name="noPoliza">El número de póliza.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns>El último índice de fila utilizado.</returns>
        public static int RellenarAnexoCondiciones<T>(
            this T lectorEscritor,
            IList<string> plantilla,
            AnexoCondicionesParticularesCoaseguroResultSet anexo,
            string noPoliza,
            int indice)
        {
            return RellenarAnexoCondicionesGenerico(plantilla, anexo, noPoliza, indice);
        }

        /// <summary>
        /// Agrega el encabezado a la plantilla de LaTeX indicada. Regresa el último índice de fila
        /// utilizado.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="encabezado">Los datos del encabezado.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns></returns>
        private static int RellenarEncabezadoGenerico(IList<string> plantilla, EncabezadoReportesEmisionResultSet encabezado, int indice)
        {
            indice = plantilla.FindIndex(linea => linea.Contains("<TIPO-ENDO>"));
            plantilla[indice] = plantilla[indice]
                .Replace("<TIPO-ENDO>", encabezado.TipoEndoso)
                .Replace("<TIPO-POLIZA>", encabezado.TipoPoliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<DESC-RAMO-COMERCIAL>"), indice);
            plantilla[indice] = plantilla[indice].Replace("<DESC-RAMO-COMERCIAL>", encabezado.RamoComercial);

            indice = plantilla.FindIndex(linea => linea.Contains("<SUC-COD-RAMO-POLIZA-ENDO-SUF>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<SUC-COD-RAMO-POLIZA-ENDO-SUF>", encabezado.Poliza);

            return indice;
        }

        /// <summary>
        /// Agrega la Cédula de Participación a la plantilla de LaTeX indicada. Regresa el último índice de fila
        /// utilizado (actualiza el índice constantemente para buscar los placeholders).
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="cedula">Los datos de la cédula de participación.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns>El último índice de fila utilizado.</returns>
        private static int RellenarCedulaParticipacionGenerico(IList<string> plantilla, CedulaParticipacionCoaseguroResultSet cedula, int indice)
        {
            indice = plantilla.FindIndex(linea => linea.Contains("<SUC-COD-RAMO-POLIZA-ENDO-SUF>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<SUC-COD-RAMO-POLIZA-ENDO-SUF>", cedula.DatosGenerales.Poliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<ASEGURADO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<ASEGURADO>", cedula.DatosGenerales.Asegurado);

            indice = plantilla.FindIndex(linea => linea.Contains("<CEDULA-TABLA-COASEGURADORAS>"), indice);
            plantilla[indice] = ObtenerTablaCoaseguradorasCedulaParticipacion(cedula);

            indice = plantilla.FindIndex(linea => linea.Contains("<CEDULA-DIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<CEDULA-DIA>", cedula.DatosGenerales.FechaEmision.Day.ToString().PadLeft(2, '0'))
                .Replace("<CEDULA-MES>", cedula.DatosGenerales.FechaEmision.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")))
                .Replace("<CEDULA-ANIO>", cedula.DatosGenerales.FechaEmision.Year.ToString());

            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-FIRMAS>"), indice);
            plantilla[indice] = ObtenerTablaRepresentantes(cedula.Coaseguradoras.Select(coas => coas.Coaseguradora));

            return indice;
        }

        /// <summary>
        /// Agrega el Anexo de Condiciones Particulares a la plantilla de LaTeX indicada. Regresa el
        /// último índice de fila utilizado (actualiza el índice constantemente para buscar los placeholders).
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <param name="noPoliza">El número de póliza.</param>
        /// <param name="indice">El índice de la fila de la plantilla a partir de la cual se empezará la búsqueda.</param>
        /// <returns>El último índice de fila utilizado.</returns>
        private static int RellenarAnexoCondicionesGenerico(IList<string> plantilla, AnexoCondicionesParticularesCoaseguroResultSet anexo, string noPoliza, int indice)
        {
            indice = plantilla.FindIndex(linea => linea.Contains("<ASEGURADO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<ASEGURADO>", anexo.DatosGenerales.Asegurado);

            indice = plantilla.FindIndex(linea => linea.Contains("<RFC>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<RFC>", anexo.DatosGenerales.RFC);

            indice = plantilla.FindIndex(linea => linea.Contains("<DOMICILIO-FISCAL>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<DOMICILIO-FISCAL", anexo.DatosGenerales.DomicilioFiscal.Replace('|', ' '));

            indice = plantilla.FindIndex(linea => linea.Contains("<GIRO>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<GIRO>", anexo.DatosGenerales.Giro);

            indice = plantilla.FindIndex(linea => linea.Contains("<NO-POLIZA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<NO-POLIZA>", noPoliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<VIGENCIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<VIGENCIA>", anexo.DatosGenerales.FechaVigencia?.ToString("dd/MM/yyyy") ?? "Sin Vigencia");

            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-ANEXO-PARTICIPACION-COASEGURADORAS>"), indice);
            plantilla[indice] = ObtenerTablaParticipacionAnexo(anexo);

            AgregarCondicionesEspecíficasAnexo(plantilla, anexo, indice);

            return indice;
        }

        /// <summary>
        /// Regresa el código para la tabla de coaseguradoras con sus respectivos porcentajes y montos
        /// de participación para la sección de Cédula de Participación.
        /// </summary>
        /// <param name="cedula">Los datos de la cédula de participación.</param>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private static string ObtenerTablaCoaseguradorasCedulaParticipacion(CedulaParticipacionCoaseguroResultSet cedula)
        {
            var builder = new StringBuilder($@"GRUPO MEXICANO DE SEGUROS, S.A. DE C.V. & Líder & ")
                .Append($@"{cedula.DatosGenerales.PorcentajeGMX.ToString("N2")} \% & ")
                .AppendLine($@"\$ {cedula.DatosGenerales.MontoParticipacionGMX.ToString("N2")}\\\hline");

            foreach (var coas in cedula.Coaseguradoras) {
                builder
                    .Append($@"{coas.Coaseguradora} & Seguidor & ")
                    .Append($@"{coas.PorcentajeParticipacion.ToString("N2")} \% & ")
                    .AppendLine($@"\$ {coas.MontoParticipacion.ToString("N2")}\\\hline");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Regresa el código para la tabla de participación por sección/ramo para la sección de 
        /// Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private static string ObtenerTablaParticipacionAnexo(AnexoCondicionesParticularesCoaseguroResultSet anexo)
        {
            var builder = new StringBuilder(anexo.GMX.Ramo)
                .AppendLine($@" & GRUPO MEXICANO DE SEGUROS, S.A. DE C.V. & {anexo.GMX.PorcentajeGMX.ToString("N2")} \% & Líder\\\hline");

            foreach (var coas in anexo.Coaseguradoras) {
                builder
                    .Append($@"{coas.Ramo} & {coas.Coaseguradora} & ")
                    .AppendLine($@"{coas.PorcentajeParticipacion.ToString("N2")} \% & Seguidor\\\hline");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Regresa el código para la tabla de porcentajes y montos de Fee por coaseguradora seguidora
        /// para la sección de Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private static string ObtenerTablaFeeAnexo(AnexoCondicionesParticularesCoaseguroResultSet anexo)
        {
            var builder = new StringBuilder();

            foreach (var coas in anexo.Coaseguradoras) {
                builder.AppendLine($@"{coas.Coaseguradora} & {coas.PorcentajeFee.ToString("N2")} \% & \$ {coas.MontoFee.ToString("N2")}\\\hline");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Regresa el código para la sección de conceptos incluidos en el pago de comisión al agente para
        /// la sección de Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <returns>Una cadena con el código de los conceptos.</returns>
        private static string ObtenerConceptosPagoComisionAgenteAnexo(AnexoCondicionesParticularesCoaseguroResultSet anexo)
        {
            var builder = new StringBuilder();
            var primaNeta = anexo.DatosEspecificos.PrimaNetaComisionAgente;
            var recargo = anexo.DatosEspecificos.RecargoPagoFraccionado;
            var sobrecomision = anexo.DatosEspecificos.SobreComision;

            return builder
                .AppendLine($@"${(primaNeta ? @"\boxtimes" : @"\Box")}$ Prima Neta\\")
                .AppendLine($@"\indent ${(recargo ? @"\boxtimes" : @"\Box")}$ Recargos por pago fraccionado\\")
                .AppendLine($@"\indent ${(sobrecomision ? @"\boxtimes" : @"\Box")}$ Sobre comisión\\")
                .ToString();
        }

        /// <summary>
        /// Agrega las condiciones específicas del anexo, ya que se utilizan varios checkboxes
        /// con sus respectivas condiciones.
        /// </summary>
        /// <param name="plantilla">La plantilla de LaTex.</param>
        /// <param name="anexo">Los datos del anexo de condiciones particulares.</param>
        /// <param name="indice">El último índice de la fila de la plantilla</param>
        private static void AgregarCondicionesEspecíficasAnexo(IList<string> plantilla, AnexoCondicionesParticularesCoaseguroResultSet anexo, int indice)
        {
            var newLine = Environment.NewLine;

            // Moneda
            var moneda = anexo.DatosEspecificos.Moneda;
            indice = plantilla.FindIndex(linea => linea.Contains("<MONEDA>"), indice);
            plantilla[indice] =
                $@"${(moneda.Contains("NACIONAL") ? @"\boxtimes" : @"\Box")}$ Pesos Mexicanos (MXP)\\{newLine}"
                + $@"\indent ${(moneda.Contains("AMERICANO") ? @"\boxtimes" : @"\Box")}$ Dólares (USD)\\{newLine}";

            // Forma de Pago del Asegurado
            var formaPago = anexo.DatosEspecificos.FormaPago;
            indice = plantilla.FindIndex(linea => linea.Contains("<FORMA-PAGO-ASEGURADO>"), indice);
            plantilla[indice] =
                $@"${(formaPago == "ANUAL" ? @"\boxtimes" : @"\Box")}$ Anual\\{newLine}"
                + $@"\indent ${(formaPago == "CONTADO" ? @"\boxtimes" : @"\Box")}$ Contado\\{newLine}"
                + $@"\indent ${(formaPago == "SEMESTRAL" ? @"\boxtimes" : @"\Box")}$ Semestral\\{newLine}"
                + $@"\indent ${(formaPago == "TRIMESTRAL" ? @"\boxtimes$" : @"\Box")}$ Trimestral\\{newLine}"
                + $@"\indent ${(formaPago == "MENSUAL" ? @"\boxtimes$" : @"\Box")}$ Mensual\\{newLine}";

            // Garantía de Pago
            var garantiaPago = anexo.DatosEspecificos.GarantiaPago;
            var son30Dias = garantiaPago.Contains("Ley");
            indice = plantilla.FindIndex(linea => linea.Contains("<GARANTIA-PAGO>"), indice);

            plantilla[indice] =
                $@"${(son30Dias ? @"\boxtimes" : @"\Box")}$ De acuerdo a la Ley Sobre el Contrato de Seguro\\{newLine}"
                + $@"\indent ${(!son30Dias ? @"\boxtimes" : @"\Box")}$ Otro (especificar): \underline{{{(son30Dias ? @"\hspace{5cm}" : garantiaPago)}}}\\{newLine}";

            // Método de Pago
            var metodoPago = anexo.DatosEspecificos.MetodoPago;
            var esEstadoCuenta = metodoPago == "Estado de Cuenta";
            indice = plantilla.FindIndex(linea => linea.Contains("<METODO-PAGO>"), indice);
            plantilla[indice] =
                $@"${(esEstadoCuenta ? @"\boxtimes" : @"\Box")}$ {metodoPago}\\{newLine}"
                + $@"\indent ${(!esEstadoCuenta ? @"\boxtimes" : @"\Box")}$ Otro (especificar): \underline{{{(esEstadoCuenta ? @"\hspace{5cm}" : metodoPago)}}} \\{newLine}";

            // Pago de Comisión al Agente
            var comisionAgente = anexo.DatosEspecificos.PagoComisionAgente;
            var esLider100 = comisionAgente.Contains("100%");
            indice = plantilla.FindIndex(linea => linea.Contains("<PAGO-COMISION-AGENTE>"), indice);
            plantilla[indice] =
                $@"${(esLider100 ? @"\boxtimes" : @"\Box")}$ La COASEGURADORA LÍDER paga el 100 \%\\{newLine}"
                + $@"\indent ${(!esLider100 ? @"\boxtimes" : @"\Box")}$ Cada COASEGURADORA paga su participación \\{newLine}";

            // Conceptos de Pago de Comisión al Agente
            indice = plantilla.FindIndex(linea => linea.Contains("<CONCEPTOS-PAGO-COMISION-AGENTE>"), indice);
            plantilla[indice] = ObtenerConceptosPagoComisionAgenteAnexo(anexo);

            // Fees
            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-ANEXO-FEE>"), indice);
            plantilla[indice] = ObtenerTablaFeeAnexo(anexo);

            // Siniestros e Indemnizaciones
            var formaIndemnizacion = anexo.DatosEspecificos.FormaIndemnizacion;
            indice = plantilla.FindIndex(linea => linea.Contains("<PAGO-SINIESTRO>"), indice);

            if (formaIndemnizacion == null) {
                plantilla[indice] =
                    $@"$\boxtimes$ Hasta el \underline{{{anexo.GMX.PorcentajeGMX.ToString("N2")}}} \% del límite de responsabilidad\\{newLine}"
                    + $@"\indent $\Box$ Hasta un límite máximo de \$\underline{{\hspace{{2cm}}}}";
            } else {
                if (formaIndemnizacion == "Monto") {
                    plantilla[indice] =
                        $@"$\Box$ Hasta el \underline{{\hspace{{2cm}}}} \% del límite de responsabilidad\\{newLine}"
                        + $@"\indent $\boxtimes$ Hasta un límite máximo de \$ \underline{{{anexo.DatosEspecificos.MontoSiniestro?.ToString("N2")}}}";
                } else {
                    plantilla[indice] =
                        $@"$\boxtimes$ Hasta el \underline{{{anexo.DatosEspecificos.PorcentajeSiniestro?.ToString("N2")}}} \% del límite de responsabilidad\\{newLine}"
                        + $@"\indent $\Box$ Hasta un límite máximo de \$\underline{{\hspace{{2cm}}}}";
                }
            }

            // Sucursal y Fecha de Emisión
            indice = plantilla.FindIndex(linea => linea.Contains("<ANEXO-SUCURSAL>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<ANEXO-SUCURSAL>", anexo.DatosEspecificos.Sucursal)
                .Replace("<ANEXO-DIA>", anexo.DatosEspecificos.FechaEmision.Day.ToString().PadLeft(2, '0'))
                .Replace("<ANEXO-MES>", anexo.DatosEspecificos.FechaEmision.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")))
                .Replace("<ANEXO-ANIO>", anexo.DatosEspecificos.FechaEmision.Year.ToString());

            // Tabla de Firmas
            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-FIRMAS>"), indice);
            plantilla[indice] = ObtenerTablaRepresentantes(anexo.Coaseguradoras.Select(coas => coas.Coaseguradora));
        }

        /// <summary>
        /// Regresa el código para la tabla de las firmas de los representantes legales de cada coaseguradora
        /// para la sección de Cédula de Participación y Anexo de Condiciones Específicas.
        /// </summary>
        /// <param name="nombresCoas">Los nombres de las coaseguradoras.</param>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private static string ObtenerTablaRepresentantes(IEnumerable<string> nombresCoas)
        {
            var builder = new StringBuilder();
            var numCoas = nombresCoas.Count();

            for (int i = 0; i < numCoas; i++) {
                builder
                    .AppendLine($@"\textbf{{{nombresCoas.ElementAt(i)}}} &\\")
                    .Append(@"Nombre y Firma Representante Legal & \underline{\hspace{5cm}}")
                    .AppendLine((i < numCoas - 1) ? @"\\\\\\\\" : string.Empty);
            }

            return builder.ToString();
        }
    }
}