using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hjs_encomiendas_servidor.Modelo
{
    public class Estado
    {

        [Key]
        public int idEstado { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }
        
    }
}
