using System.ComponentModel.DataAnnotations;

namespace Proyecto_DSW_QuickStop.Modelos
{
    public class Favoritos
    {
        [Required] public string? cod_fav { get; set; }
        [Required] public string? cod_cli { get; set; }
        [Required] public string? cod_prod { get; set; }

        [Required] public string? descripcion { get; set; }

        public string? eliminado { get; set; }
    }
}
