namespace WSEmision.Models.DAL.DTO
{
    /// <summary>
    /// Representa el conjunto de resultados del
    /// procedimiento sp_EncabezadoReportesEmision().
    /// </summary>
    public class EncabezadoReportesEmisionResultSet
    {
        /// <summary>
        /// El tipo de endoso de esta póliza.
        /// </summary>
        public string TipoEndoso { get; set; }

        /// <summary>
        /// El tipo de esta póliza.
        /// </summary>
        public string TipoPoliza { get; set; }

        /// <summary>
        /// El ramo comercial del asegurado.
        /// </summary>
        public string RamoComercial { get; set; }

        /// <summary>
        /// La póliza compuesta del coaeguro líder. Su formato es:
        /// [Sucursal]-[Ramo]-[Póliza]-[Endoso]-[Sufijo]
        /// </summary>
        public string Poliza { get; set; }
    }
}