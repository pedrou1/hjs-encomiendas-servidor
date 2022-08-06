using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hjs_encomiendas_servidor.Modelo
{
    public class EstadoPedido
    {
        [Key]
        [ForeignKey("estado")]
        public int idEstado { get; set; }
        
        [Key]
        public Estado estado { get; set; }

        [ForeignKey("pedido")]
        public int idPedido { get; set; }
        
        public Pedido pedido { get; set; }

        public DateTime fechaEstadoPedido { get; set; }
        
    }
}
