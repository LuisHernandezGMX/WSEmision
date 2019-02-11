using System;
using System.Collections.Generic;

namespace WSEmision.Models.DAL.DTO.Coaseguro
{
    /// <summary>
    /// Representa el conjunto de resultados del procedimiento
    /// sp_CedulaParticipacionCoaseguro().
    /// </summary>
    public class CedulaParticipacionCoaseguroResultSet
    {
        /// <summary>
        /// Los datos generales de esta cédula de participación.
        /// </summary>
        public DatosGeneralesCedulaRS DatosGenerales { get; set; }

        /// <summary>
        /// Las coaseguradoras participantes del coaseguro.
        /// </summary>
        public IEnumerable<CoaseguradorasCedulaRS> Coaseguradoras { get; set; }
    }

    /// <summary>
    /// Sección de Datos Generales de la Cédula de Participación en Coaseguro.
    /// </summary>
    public class DatosGeneralesCedulaRS
    {
        /// <summary>
        /// La póliza compuesta del coaeguro líder. Su formato es:
        /// [Sucursal]-[Ramo]-[Póliza]-[Endoso]-[Sufijo]
        /// </summary>
        public string Poliza { get; set; }

        /// <summary>
        /// El nombre completo del asegurado.
        /// </summary>
        public string Asegurado { get; set; }

        /// <summary>
        /// El ramo comercial del asegurado.
        /// </summary>
        public string RamoComercial { get; set; }

        /// <summary>
        /// El tipo de endoso de esta póliza.
        /// </summary>
        public string TipoEndoso { get; set; }

        /// <summary>
        /// El tipo de esta póliza.
        /// </summary>
        public string TipoPoliza { get; set; }

        /// <summary>
        /// El porcentaje de participación de GMX.
        /// </summary>
        public decimal PorcentajeGMX { get; set; }

        /// <summary>
        /// El monto de participación de GMX.
        /// </summary>
        public decimal MontoParticipacionGMX { get; set; }

        /// <summary>
        /// La fecha de emisión de la póliza.
        /// </summary>
        public DateTime FechaEmision { get; set; }
    }

    /// <summary>
    /// Sección de coaseguradoras participantes con sus respectivos
    /// porcentajes y montos de participación.
    /// </summary>
    public class CoaseguradorasCedulaRS
    {
        /// <summary>
        /// El nombre de la coaseguradora.
        /// </summary>
        public string Coaseguradora { get; set; }

        /// <summary>
        /// El porcentaje de participación de la coaseguradora.
        /// </summary>
        public decimal PorcentajeParticipacion { get; set; }

        /// <summary>
        /// El monto de participación de la coaseguradora.
        /// </summary>
        public decimal MontoParticipacion { get; set; }
    }
}