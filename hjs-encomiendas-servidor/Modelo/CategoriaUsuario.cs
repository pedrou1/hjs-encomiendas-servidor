using System.ComponentModel.DataAnnotations;

namespace hjs_encomiendas_servidor.Modelo
{
    public class CategoriaUsuario
    {
        [Key]
        public int idCategoria { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

    }
}
