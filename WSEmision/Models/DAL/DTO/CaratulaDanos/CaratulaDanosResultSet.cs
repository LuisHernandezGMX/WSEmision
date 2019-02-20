namespace WSEmision.Models.DAL.DTO.CaratulaDanos
{
    /// <summary>
    /// Representa el conjunto de resultados del
    /// procedimiento sp_pp_caratula_danos().
    /// </summary>
    public class CaratulaDanosResultSet
    {
        /// <summary>
        /// El nombre de la sucursal de GMX.
        /// Representa la columna [LUGAR].
        /// </summary>
        public string Oficina { get; set; }

        /// <summary>
        /// La información del asegurado.
        /// </summary>
        public AseguradoRS Asegurado { get; set; }

        /// <summary>
        /// El agente de la carátula.
        /// Representa la columna [AGENTES].
        /// </summary>
        public string Agentes { get; set; }

        /// <summary>
        /// Información adicional acerca de la póliza.
        /// </summary>
        public InfoPolizaRS InfoPoliza { get; set; }

        /// <summary>
        /// Los importes de la carátula.
        /// </summary>
        public ImportesRS Importes { get; set; }

        /// <summary>
        /// La descripción del ramo.
        /// Representa la columna [DESC_POR_RAMO]
        /// </summary>
        public string DescPorRamo { get; set; }

        /// <summary>
        /// El pie de página del documento.
        /// Representa la columna [FOOTER_ID_PV_BARRAS].
        /// </summary>
        public string Footer { get; set; }
    }

    /// <summary>
    /// Representa el conjunto de resultados
    /// con información adicional acerca de la póliza.
    /// </summary>
    public class InfoPolizaRS
    {
        /// <summary>
        /// Los días de vigencia.
        /// Representa la columna [VIGENCIA].
        /// </summary>
        public double Vigencia { get; set; }

        /// <summary>
        /// Representa la columna [DIA1].
        /// </summary>
        public string Dia1 { get; set; }

        /// <summary>
        /// Representa la columna [MES1].
        /// </summary>
        public string Mes1 { get; set; }

        /// <summary>
        /// Representa la columna [ANO1].
        /// </summary>
        public int Ano1 { get; set; }

        /// <summary>
        /// Representa la columna [HORADESDE].
        /// </summary>
        public string HoraDesde { get; set; }

        /// <summary>
        /// Representa la columna [DIA2].
        /// </summary>
        public string Dia2 { get; set; }

        /// <summary>
        /// Representa la columna [MES2].
        /// </summary>
        public string Mes2 { get; set; }

        /// <summary>
        /// Representa la columna [ANO2]
        /// </summary>
        public int Ano2 { get; set; }

        /// <summary>
        /// Representa la columna [HORAHASTA].
        /// </summary>
        public string HoraHasta { get; set; }

        /// <summary>
        /// Representa la columna [DIA].
        /// </summary>
        public string Dia { get; set; }

        /// <summary>
        /// Representa la columna [MES].
        /// </summary>
        public string Mes { get; set; }

        /// <summary>
        /// Representa la columna [ANO]
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Representa la columna [MONEDA].
        /// </summary>
        public string Moneda { get; set; }

        /// <summary>
        /// Representa la columna [PAGO].
        /// </summary>
        public string FormaPago { get; set; }
    }

    /// <summary>
    /// Representa el conjunto de resultados de los importes.
    /// </summary>
    public class ImportesRS
    {
        /// <summary>
        /// La prima neta. Representa la columna [PRIMA].
        /// </summary>
        public string PrimaNeta { get; set; }

        /// <summary>
        /// El monto de recargo fraccionado.
        /// Representa la columna [IMPORTEFRAC].
        /// </summary>
        public string Recargo { get; set; }

        /// <summary>
        /// El campo de Derecho. Representa
        /// la columna [GASTOS].
        /// </summary>
        public string Derecho { get; set; }

        /// <summary>
        /// El monto de IVA. Representa
        /// la columna [IMPORTEIVA].
        /// </summary>
        public string IVA { get; set; }

        /// <summary>
        /// La suma total de los montos.
        /// Representa la columna [TOTAL].
        /// </summary>
        public string Total { get; set; }
    }

    /// <summary>
    /// Representa el conjunto de resultados del asegurado.
    /// </summary>
    public class AseguradoRS
    {
        /// <summary>
        /// El nombre del asegurado.
        /// Representa la columna [NOMBRE].
        /// </summary>
        public string Contratante { get; set; }

        /// <summary>
        /// El RFC del asegurado.
        /// Representa la columna [RFC].
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// La fecha de nacimiento del asegurado, o fecha
        /// de constitución si se trata de una empresa.
        /// Representa la columna [FEC_NACIMIENTO].
        /// </summary>
        public string FechaNacimiento { get; set; }

        /// <summary>
        /// El domicilio del asegurado.
        /// </summary>
        public DomicilioRS Domicilio { get; set; }
    }

    /// <summary>
    /// Representa el conjunto de resultados del domicilio del asegurado.
    /// </summary>
    public class DomicilioRS
    {
        /// <summary>
        /// Representa la columna [CALLE].
        /// </summary>
        public string Calle { get; set; }

        /// <summary>
        /// Representa la columna [NUMERO].
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Representa la columna [NUMERO].
        /// </summary>
        public string Interior { get; set; }

        /// <summary>
        /// Representa la columna [COLONIA].
        /// </summary>
        public string Colonia { get; set; }

        /// <summary>
        /// Representa la columna [POBLACION].
        /// </summary>
        public string Poblacion { get; set; }

        /// <summary>
        /// Representa la columna [CIUDAD].
        /// </summary>
        public string Ciudad { get; set; }

        /// <summary>
        /// Representa la columna [ESTADO].
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Representa la columna [CP].
        /// </summary>
        public string CP { get; set; }
    }
}