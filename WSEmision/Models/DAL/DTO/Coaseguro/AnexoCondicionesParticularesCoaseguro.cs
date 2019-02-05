using System;
using System.Collections.Generic;

namespace WSEmision.Models.DAL.DTO.Coaseguro
{
    /// <summary>
    /// Representa el conjunto de resultados del procedimiento
    /// sp_AnexoCondicionesParticularesCoaseguro().
    /// </summary>
    public class AnexoCondicionesParticularesCoaseguroResultSet
    {
        /// <summary>
        /// Los datos generales de este anexo.
        /// </summary>
        public DatosGeneralesAnexoRS DatosGenerales { get; set; }

        /// <summary>
        /// El ramo y participación de GMX.
        /// </summary>
        public GMXAnexoRS GMX { get; set; }

        /// <summary>
        /// Las coaseguradoras participantes.
        /// </summary>
        public IEnumerable<CoaseguradorasAnexoRS> Coaseguradoras { get; set; }

        /// <summary>
        /// Los datos específicos del coaseguro.
        /// </summary>
        public DatosEspecificosAnexoRS DatosEspecificos { get; set; }
    }

    /// <summary>
    /// Sección de Datos Generales del Anexo de Condiciones Particulares del Coaseguro.
    /// </summary>
    public class DatosGeneralesAnexoRS
    {
        /// <summary>
        /// El nombre completo del asegurado.
        /// </summary>
        public string Asegurado { get; set; }

        /// <summary>
        /// EL RFC del asegurado.
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// El domicilio completo fiscal del asegurado.
        /// </summary>
        public string DomicilioFiscal { get; set; }

        /// <summary>
        /// El giro del negocio.
        /// </summary>
        public string Giro { get; set; }

        /// <summary>
        /// El número de póliza de la coaseguradora líder.
        /// </summary>
        public decimal PolizaLider { get; set; }

        /// <summary>
        /// La fecha de vigencia de la póliza líder.
        /// </summary>
        public DateTime? FechaVigencia { get; set; }
    }

    /// <summary>
    /// Sección de ramo y participación de GMX.
    /// </summary>
    public class GMXAnexoRS
    {
        /// <summary>
        /// El ramo técnico de la cobertura.
        /// </summary>
        public string Ramo { get; set; }

        /// <summary>
        /// El porcentaje de participación de GMX.
        /// </summary>
        public decimal PorcentajeGMX { get; set; }
    }

    /// <summary>
    /// Sección de ramo, participación y fees de las coaseguradoras.
    /// </summary>
    public class CoaseguradorasAnexoRS
    {
        /// <summary>
        /// El ramo técnico de la cobertura.
        /// </summary>
        public string Ramo { get; set; }

        /// <summary>
        /// El nombre de la coaseguradora.
        /// </summary>
        public string Coaseguradora { get; set; }

        /// <summary>
        /// El porcentaje de participación de la coaseguradora.
        /// </summary>
        public decimal PorcentajeParticipacion { get; set; }

        /// <summary>
        /// El porcentaje de fee por administración de la coaseguradora.
        /// </summary>
        public decimal PorcentajeFee { get; set; }

        /// <summary>
        /// El monto de fee por administración de la coaseguradora.
        /// </summary>
        public decimal MontoFee { get; set; }
    }

    /// <summary>
    /// Sección de datos específicos sobre el coaseguro.
    /// </summary>
    public class DatosEspecificosAnexoRS
    {
        /// <summary>
        /// La fecha de emisión de la póliza.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        /// <summary>
        /// La sucursal donde se emitió la póliza.
        /// </summary>
        public string Sucursal { get; set; }

        /// <summary>
        /// El tipo de moneda de la póliza.
        /// </summary>
        public string Moneda { get; set; }

        /// <summary>
        /// La forma de pago del siniestro.
        /// </summary>
        public string FormaPago { get; set; }

        /// <summary>
        /// El método de pago del siniestro.
        /// </summary>
        public string MetodoPago { get; set; }

        /// <summary>
        /// Los días que se tienen para realizar el pago del siniestro.
        /// </summary>
        public string GarantiaPago { get; set; }

        /// <summary>
        /// La forma de pago al asegurado por parte de las coaseguradoras.
        /// </summary>
        public string PagoComisionAgente { get; set; }

        /// <summary>
        /// La forma de pago del siniestro por parte del líder.
        /// </summary>
        public string PagoSiniestro { get; set; }

        /// <summary>
        /// El porcentaje de la participación para el siniestro.
        /// </summary>
        public string PorcentajeSiniestro { get; set; }

        /// <summary>
        /// El monto del siniestro.
        /// </summary>
        public string MontoSiniestro { get; set; }
    }
}