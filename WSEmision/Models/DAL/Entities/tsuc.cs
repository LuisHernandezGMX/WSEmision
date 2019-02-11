namespace WSEmision.Models.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tsuc")]
    public partial class tsuc
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal cod_suc { get; set; }

        [Required]
        [StringLength(40)]
        public string txt_nom_suc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_tipo_dir { get; set; }

        [StringLength(180)]
        public string txt_direccion { get; set; }

        [StringLength(10)]
        public string nro_cod_postal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_zona_dir { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_colonia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_municipio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_dpto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_pais { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_telef { get; set; }

        [Required]
        [StringLength(15)]
        public string txt_telefono { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cod_tipo_iva { get; set; }

        [Required]
        [StringLength(20)]
        public string nro_nit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_pol_desde { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_pol_hasta { get; set; }

        [Column(TypeName = "numeric")]
        public decimal aaaa_endoso { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_endoso_desde { get; set; }

        [Column(TypeName = "numeric")]
        public decimal nro_endoso_hasta { get; set; }

        [Column(TypeName = "numeric")]
        public decimal pje_igss { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_regional { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_suc_reg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? id_bco_default { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fec_cierre_diario { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_linea { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? dias_desvio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_calle1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nro_nro1 { get; set; }

        [StringLength(100)]
        public string txt_desc1 { get; set; }

        [StringLength(10)]
        public string nro_apto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cod_ciudad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_caja { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sn_sucursal_debito { get; set; }
    }
}
