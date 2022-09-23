using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hjs_encomiendas_servidor.Modelo
{
    public class TipoPedido
    {
        public TipoPedido()
        {
        }

        public TipoPedido(TipoPedidoVO tipoPedidoVO)
        {
            this.idTipoPedido = tipoPedidoVO.idTipoPedido;
            this.nombre = tipoPedidoVO.nombre;
            this.tarifa = tipoPedidoVO.tarifa;
            this.pesoDesde = tipoPedidoVO.pesoDesde;
            this.pesoHasta = tipoPedidoVO.pesoHasta;
        }

        [Key]
        public int idTipoPedido { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        public int tarifa { get; set; }

        public int pesoDesde { get; set; }

        public int pesoHasta { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

        public void update(TipoPedidoVO tipoPedidoVO)
        {
            this.nombre = tipoPedidoVO.nombre;
            this.tarifa = tipoPedidoVO.tarifa;
            this.pesoDesde = tipoPedidoVO.pesoDesde;
            this.pesoHasta = tipoPedidoVO.pesoHasta;
        }
    }
}
