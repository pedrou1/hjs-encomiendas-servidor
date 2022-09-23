using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class TiposPedidoVO : GetDataOutVO
    {
        public List<TipoPedido>? tiposPedidos { get; set; }
    }
}
