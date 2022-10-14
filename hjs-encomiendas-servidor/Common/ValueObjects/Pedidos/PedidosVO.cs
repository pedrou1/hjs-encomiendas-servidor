using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class PedidosVO : GetDataOutVO
    {
        public List<Pedido>? pedidos { get; set; }

        public List<int>? ordenPedidos { get; set; }
    }
}
