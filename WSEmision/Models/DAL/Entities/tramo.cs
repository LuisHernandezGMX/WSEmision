namespace WSEmision.Models.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tramo")]
    public partial class tramo
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal cod_ramo { get; set; }

        [Required]
        [StringLength(10)]
        public string txt_desc_abrev { get; set; }

        [Required]
        [StringLength(20)]
        public string txt_desc_redu { get; set; }

        [Required]
        [StringLength(80)]
        public string txt_desc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_bomberos { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_ttipo_ramo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_rrc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ult_nro_poliza { get; set; }

        public int ult_nro_stro { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_det_cta_cte { get; set; }

        [Required]
        [StringLength(2)]
        public string cod_grupo { get; set; }

        [StringLength(2)]
        public string cod_ramo_super { get; set; }

        [StringLength(2)]
        public string cod_ramo_fasecolda { get; set; }

        [StringLength(15)]
        public string cta_primas_dep { get; set; }

        [StringLength(4)]
        public string auxiliar_cble { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_perfil { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_ramo_cumulo_reas { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_tipo_ejer_reas { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pje_recargo_gral { get; set; }

        [StringLength(3)]
        public string auxiliar_cble_pxc { get; set; }

        [StringLength(4)]
        public string auxiliar_cble_ge { get; set; }

        [StringLength(4)]
        public string auxiliar_cble_com { get; set; }

        [StringLength(3)]
        public string auxiliar_cble_stro { get; set; }

        [StringLength(4)]
        public string auxiliar_cble_dev { get; set; }

        [StringLength(1)]
        public string aux_cble_int_ctatec_reas { get; set; }

        [StringLength(1)]
        public string aux_cble_pma_reas_ced { get; set; }

        [StringLength(3)]
        public string aux_cble_com_reas_ced { get; set; }

        [StringLength(3)]
        public string aux_cble_rva_tec { get; set; }

        [StringLength(3)]
        public string auxiliar_cble_recob_salv { get; set; }

        [StringLength(3)]
        public string aux_cble_reas_tom { get; set; }

        [StringLength(3)]
        public string aux_cble_com_reas_tom { get; set; }

        [StringLength(3)]
        public string aux_cble_dev_reas_tom { get; set; }

        [Required]
        [StringLength(20)]
        public string txt_departamento { get; set; }

        public int? cod_grupo_ramo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_ramo_comercial { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_primer_riesgo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sn_requiere_recibo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_calc_res { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_reporte_cad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_riesgo_hipotecario { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_usa_certificado { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_valida_certificado { get; set; }
    }
}
