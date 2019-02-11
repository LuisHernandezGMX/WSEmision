using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using WSEmision.Models.DAL.DTO.Coaseguro;
using WSEmision.Models.Business.Extensions;

namespace WSEmision.Models.Business.IO.Coaseguro
{
    /// <summary>
    /// Lee y genera los reportes para la Cédula de Participación
    /// en Coaseguro y el Anexo de Condiciones Particulares
    /// en un solo archivo PDF.
    /// </summary>
    public sealed class CedulaAnexoLectorEscritor : LatexLectorEscritor
    {
        /// <summary>
        /// Contiene los datos de la Cédula de Participación en Coaseguro.
        /// </summary>
        private CedulaParticipacionCoaseguroResultSet cedula;

        /// <summary>
        /// Contiene los datos del Anexo y Condiciones Particulares en Coaseguro.
        /// </summary>
        private AnexoCondicionesParticularesCoaseguroResultSet anexo;

        /// <summary>
        /// Genera una nueva instancia con la cédula y el anexo indicados.
        /// </summary>
        /// <param name="cedula">Los datos de la Cédula de Participación en Coaseguro.</param>
        /// <param name="anexo">Los datos del Anexo y Condiciones Particulares en Coaseguro.</param>
        /// <param name="rutaCompilador">La ruta absoluta al compilador (.exe) de LaTex.</param>
        /// <param name="inputDir">La ruta del directorio de multimedia de la plantilla.</param>
        public CedulaAnexoLectorEscritor(
            CedulaParticipacionCoaseguroResultSet cedula,
            AnexoCondicionesParticularesCoaseguroResultSet anexo,
            string rutaCompilador,
            string inputDir)
        : base(rutaCompilador, inputDir)
        {
            this.cedula = cedula;
            this.anexo = anexo;
        }

        /// <summary>
        /// Rellena la plantilla con la información relevante sobre la Cédula
        /// de Participación y el Anexo de Condiciones Particulares.
        /// </summary>
        /// <param name="plantilla">Las filas de la plantilla leídas desde el archivo .tex.</param>
        protected override void RellenarPlantilla(IList<string> plantilla)
        {
            int indice;
            var polizaCompuesta = cedula.DatosGenerales.Poliza.Split('-');

            // Header
            indice = plantilla.FindIndex(linea => linea.Contains("<TIPO-ENDO>"));
            plantilla[indice] = plantilla[indice]
                .Replace("<TIPO-ENDO>", cedula.DatosGenerales.TipoEndoso)
                .Replace("<TIPO-POLIZA>", cedula.DatosGenerales.TipoPoliza);

            indice = plantilla.FindIndex(linea => linea.Contains("<DESC-RAMO-COMERCIAL>"), indice);
            plantilla[indice] = plantilla[indice].Replace("<DESC-RAMO-COMERCIAL>", cedula.DatosGenerales.RamoComercial);

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

            // Anexo y Condiciones Particulares
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
                .Replace("<NO-POLIZA>", polizaCompuesta[2]);

            indice = plantilla.FindIndex(linea => linea.Contains("<VIGENCIA>"), indice);
            plantilla[indice] = plantilla[indice]
                .Replace("<VIGENCIA>", anexo.DatosGenerales.FechaVigencia?.ToString("dd/MM/yyyy") ?? "Sin Vigencia");

            indice = plantilla.FindIndex(linea => linea.Contains("<TABLA-ANEXO-PARTICIPACION-COASEGURADORAS>"), indice);
            plantilla[indice] = ObtenerTablaParticipacionAnexo();

            AgregarCondicionesEspecíficasAnexo(plantilla, indice);
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

        // <summary>
        /// Regresa el código para la tabla de participación por sección/ramo para la sección de 
        /// Anexo de Condiciones Particulares.
        /// </summary>
        /// <returns>Una cadena con todo el código de la tabla.</returns>
        private string ObtenerTablaParticipacionAnexo()
        {
            var builder = new StringBuilder(anexo.GMX.Ramo)
                .AppendLine($@" & GRUPO MEXICANO DE SEGUROS, S.A. DE C.V. & {anexo.GMX.PorcentajeGMX}\% & Líder\\\hline");

            foreach (var coas in anexo.Coaseguradoras) {
                builder
                    .Append($@"{coas.Ramo} & {coas.Coaseguradora} & ")
                    .AppendLine($@"{coas.PorcentajeParticipacion}\% & Seguidor\\\hline");
            }
            
            return builder.ToString();
        }

        /// <summary>
        /// Agrega las condiciones específicas del anexo, ya que se utilizan varios checkboxes
        /// con sus respectivas condiciones.
        /// </summary>
        /// <param name="plantilla">La plantilla de LaTex.</param>
        /// <param name="indice">El último índice de la fila de la plantilla</param>
        private void AgregarCondicionesEspecíficasAnexo(IList<string> plantilla, int indice)
        {
            var newLine = Environment.NewLine;

            // Moneda
            var moneda = anexo.DatosEspecificos.Moneda;
            indice = plantilla.FindIndex(linea => linea.Contains("<MONEDA>"), indice);
            plantilla[indice] =
                $@"{(moneda.Contains("NACIONAL") ? @"$\boxtimes$" : @"$\Box$")} Pesos Mexicanos (MXP)\\{newLine}"
                + $@"\indent {(moneda.Contains("AMERICANO") ? @"$\boxtimes$" : @"$\Box$")} Dólares (USD)\\{newLine}";

            // Forma de Pago del Asegurado
            var formaPago = anexo.DatosEspecificos.FormaPago;
            indice = plantilla.FindIndex(linea => linea.Contains("<FORMA-PAGO-ASEGURADO>"), indice);
            plantilla[indice] =
                $@"{(formaPago == "ANUAL" ? @"$\boxtimes$" : @"$\Box$")} Anual\\{newLine}"
                + $@"\indent {(formaPago == "CONTADO" ? @"$\boxtimes$" : @"$\Box$")} Contado\\{newLine}"
                + $@"\indent {(formaPago == "SEMESTRAL" ? @"$\boxtimes$" : @"$\Box$")} Semestral\\{newLine}"
                + $@"\indent {(formaPago == "TRIMESTRAL" ? @"$\boxtimes$" : @"$\Box$")} Trimestral\\{newLine}"
                + $@"\indent {(formaPago == "MENSUAL" ? @"$\boxtimes$" : @"$\Box$")} Mensual\\{newLine}";

            // Garantía de Pago
            var garantiaPago = anexo.DatosEspecificos.GarantiaPago;
            var son30Dias = garantiaPago.Contains("Ley");
            indice = plantilla.FindIndex(linea => linea.Contains("<GARANTIA-PAGO>"), indice);

            plantilla[indice] =
                $@"{(son30Dias ? @"$\boxtimes$" : @"$\Box$")} De acuerdo a la Ley Sobre el Contrato de Seguro\\{newLine}"
                + $@"\indent {(!son30Dias ? @"$\boxtimes$" : @"$\Box$")} Otro (especificar): \underline{{{(son30Dias ? @"\hspace{5cm}" : garantiaPago)}}}\\{newLine}";

            // Método de Pago
            var metodoPago = anexo.DatosEspecificos.MetodoPago;
            var esEstadoCuenta = metodoPago == "Estado de Cuenta";
            indice = plantilla.FindIndex(linea => linea.Contains("<METODO-PAGO>"), indice);
            plantilla[indice] =
                $@"{(esEstadoCuenta ? @"$\boxtimes$" : @"$\Box$")} {metodoPago}\\{newLine}"
                + $@"\indent {(!esEstadoCuenta ? @"$\boxtimes$" : @"$\Box$")} Otro (especificar): \underline{{{(esEstadoCuenta ? @"\hspace{5cm}" : metodoPago)}}} \\{newLine}";

            // Pago de Comisión al Agente
            var comisionAgente = anexo.DatosEspecificos.PagoComisionAgente;
            var esLider100 = comisionAgente.Contains("100%");
            indice = plantilla.FindIndex(linea => linea.Contains("<PAGO-COMISION-AGENTE>"), indice);
            plantilla[indice] =
                $@"{(esLider100 ? @"$\boxtimes$" : @"$\Box$")} La COASEGURADORA LÍDER paga el 100\%\\{newLine}"
                + $@"\indent {(!esLider100 ? @"$\boxtimes$" : @"$\Box$")} Cada COASEGURADORA paga su participación \\{newLine}";

            // Siniestros e Indemnizaciones
            var pagoSiniestro = anexo.DatosEspecificos.PagoSiniestro;
            var esParticipacion = pagoSiniestro.Contains("Participación");
            indice = plantilla.FindIndex(linea => linea.Contains("<PAGO-SINIESTRO>"), indice);

            if (pagoSiniestro.Contains("Participación")) {
                plantilla[indice] =
                    $@"$\boxtimes$ Hasta el \underline{{{anexo.GMX.PorcentajeGMX}}}\% del límite de responsabilidad\\{newLine}"
                    + $@"\indent $\Box$ Hasta un límite máximo de \$\underline{{\hspace{{2cm}}}}";
            } else {
                plantilla[indice] =
                    $@"$\Box$ Hasta el \underline{{\hspace{{2cm}}}}\% del límite de responsabilidad\\{newLine}"
                    + $@"\indent $\boxtimes$ Hasta un límite máximo de \$\underline{{{anexo.DatosEspecificos.MontoSiniestro}}}";
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
            plantilla[indice] = ObtenerTablaRepresentantes();
        }
    }
}