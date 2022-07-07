using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hjs_encomiendas_servidor.Modelo
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [ForeignKey("categoriaUsuario")]
        public int idCategoria { get; set; }

        public CategoriaUsuario categoriaUsuario { get; set; }

        [StringLength(250)]
        public string usuario { get; set; }

        [StringLength(250)]
        public string password { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [StringLength(150)]
        public string apellido { get; set; }

        [StringLength(150)]
        public string? telefono { get; set; }

        public bool activo { get; set; }
    }
}
