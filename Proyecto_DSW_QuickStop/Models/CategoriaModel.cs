using System.ComponentModel.DataAnnotations;


namespace Proyecto_DSW_QuickStop.Models
{
    public class CategoriaModel
    {
        //Propiedades
        [Required(ErrorMessage = "Codigo Obligatorio")]
        [Display(Name = "Codigo")]
        public string cod_cat { get; set; }

        [Required(ErrorMessage = "Nombre Obligatorio")]
        [Display(Name = "Nombre de la Categoria")]
        [RegularExpression(pattern: "[a-zA-Z ]+", ErrorMessage = "Solo Letras y espacio en blanco")]
        public string nombre_categoria { get; set; }

        [Required(ErrorMessage = "Codigo Obligatorio")]
        [Display(Name = "Codigo de la Marca")]
        public int cod_marca { get; set; }




        /*
        [Display(Name = "Eliminado")]
        public string eli_marca { get; set; }*/

    }
}
