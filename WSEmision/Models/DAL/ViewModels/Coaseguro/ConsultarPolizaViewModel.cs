using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSEmision.Models.DAL.ViewModels.Coaseguro
{
    /// <summary>
    /// Incluye las llaves primarias para buscar la póliza
    /// en Coaseguro.
    /// </summary>
    public class ConsultarPolizaViewModel
    {
        /// <summary>
        /// El código de la sucursal [cod_suc].
        /// </summary>
        [Display(Name = "Sucursal")]
        [Required(ErrorMessage = "El código de la sucursal es obligatorio.")]
        public decimal CodSucursal { get; set; }

        /// <summary>
        /// El código de ramo [cod_ramo].
        /// </summary>
        [Display(Name = "Ramo")]
        [Required(ErrorMessage = "El código de ramo es obligatorio.")]
        public decimal CodRamo { get; set; }

        /// <summary>
        /// El número de la póliza a buscar.
        /// </summary>
        [Display(Name = "Póliza")]
        [Required(ErrorMessage = "El número de póliza es obligatorio.")]
        public decimal NroPoliza { get; set; }

        /// <summary>
        /// El número de endoso [nro_endoso].
        /// </summary>
        [Display(Name = "Endoso")]
        [Required(ErrorMessage = "El número de endoso es obligatorio.")]
        public decimal Endoso { get; set; }

        /// <summary>
        /// El número de sufijo [aaaa_endoso].
        /// </summary>
        [Display(Name = "Sufijo")]
        [Required(ErrorMessage = "El sufijo es obligatorio.")]
        public decimal Sufijo { get; set; }   
    }
}