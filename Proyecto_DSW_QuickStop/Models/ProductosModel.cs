using System.ComponentModel.DataAnnotations;

namespace Proyecto_DSW_QuickStop.Models
{
    public class ProductosModel
    {


        [Required(ErrorMessage = "Codigo Obligatorio")]
        [Display(Name = "Codigo")]

        public string codProd { get; set; }
        [Required(ErrorMessage = "Nombres Obligatorio")]
        [Display(Name = "Nombre del Producto")]

        public string nomProd { get; set; }

        [Required(ErrorMessage = "Categoria Obligatorio")]
        [Display(Name = "Codigo de Categoria")]

        public string codCat { get; set; }
        [Required(ErrorMessage = "Precio Obligatorio")]
        [Display(Name = "Precio")]

        public decimal preProd { get; set; }
        [Required(ErrorMessage = "Stock Obligatorio")]
        [Display(Name = "Stock")]
        public int stokProd { get; set; }

        public string? eliProd { get; set; }



    }
}
