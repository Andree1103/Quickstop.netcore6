
using System.ComponentModel.DataAnnotations;

namespace Proyecto_DSW_QuickStop.Models
{
    public class ClienteModelv2

    {
        //Propiedades
        [Required(ErrorMessage = "Codigo Obligatorio")]
        [Display(Name = "ID del Cliente")]
        public string codCliente { get; set; }


        [Required(ErrorMessage = "Nombres Obligatorio")]
        [Display(Name = "Nombres")]
        [RegularExpression(pattern: "[a-zA-Z ]+", ErrorMessage = "Solo Letras y espacio en blanco")]
        public string nomCliente { get; set; }

        [Required(ErrorMessage = "Celular Obligatorio")]
        [Display(Name = "Celular")]
        //[RegularExpression(pattern: "[0-9]{9}", ErrorMessage = "Solo numeros que contenga nueve digitos")]
        public string telCliente { get; set; }

        [Required(ErrorMessage = "Correo Electronico Obligatorio")]
        [Display(Name = "Correo Electronico")]
        public string corCliente { get; set; }

        [Required(ErrorMessage = "Direccion Obligatoria")]
        [Display(Name = "Direccion")]

        public string dirCliente { get; set; }

        [Required(ErrorMessage = "Credito Obligatorio")]
        [Display(Name = "Credito")]
        public int credCliente { get; set; }


        [Required(ErrorMessage = "Fecha Obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime fechaNac { get; set; }

        [Required(ErrorMessage = "Codigo de Distrito Obligatorio")]
        [Display(Name = "Codigo de Distrito ")]
        public int codDistrito { get; set; }
    }
}
