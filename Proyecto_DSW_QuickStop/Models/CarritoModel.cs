using System.ComponentModel.DataAnnotations;

namespace Proyecto_DSW_QuickStop.Models
{
    public class CarritoModel
    {
        [Display(Name = "Codigo")]
        public string codigo { get; set; }

        [Display(Name = "Nombre del Producto")]
        public string nombre { get; set; }

        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "Importe")]
        public decimal importe
        {
            get
            {
                return precio * cantidad;
            }
        }

    }
}
