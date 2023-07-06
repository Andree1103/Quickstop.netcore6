using System.ComponentModel.DataAnnotations;

namespace Proyecto_DSW_QuickStop.Modelos
{
    public class Producto
    {
        public string? cod_prod { get; set; }
        public string? nom_prod { get; set; }
        public string? cod_cat { get; set; }
        public decimal pre_prod { get; set; }
        public int stk_prod { get; set; }
        public string? eli_prod { get; set; }
    }
}
