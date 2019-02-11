using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WSEmision.Models.DAL.Entities
{
    [Table("pv_header")]
    public partial class pv_header
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_pv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_suc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_ramo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_pol { get; set; }

        [Column(TypeName = "numeric")]
        public decimal aaaa_endoso { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_endoso { get; set; }

        public int cod_aseg { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fec_emi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fec_vig_desde { get; set; }

        public DateTime? fec_vig_hasta { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_moneda { get; set; }

        [Column(TypeName = "numeric")]
        public decimal imp_cambio { get; set; }

        public int? id_pv_modifica { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_operacion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_solicitud { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_cotizacion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_flota { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_impresion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_anula_automatica { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_agente { get; set; }

        public int cod_agente { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_grupo_endo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_tipo_endo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_sistema { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_cob_coas_total { get; set; }

        public int? id_pv_cero { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fec_hora_desde { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_bancaseguros { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_periodo_fact { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_pv_cero { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_fronting { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_grupo { get; set; }

        public int? nro_endo_modifica { get; set; }

        public int? nro_modifica_por { get; set; }

        public int? cod_referida { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_subramo { get; set; }

        [StringLength(1)]
        public string cod_origen { get; set; }

        [StringLength(50)]
        public string txt_partic_acreedor { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_partic_cias { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_revision { get; set; }

        public short cod_nivel_facturacion { get; set; }

        public short cod_nivel_imp_factura { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? id_sol_cotiz { get; set; }
    }
}