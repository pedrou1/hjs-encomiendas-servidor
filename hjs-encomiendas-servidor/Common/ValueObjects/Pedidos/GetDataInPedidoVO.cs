namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class GetDataInPedidoVO : GetDataInVO
    {
        public int idUsuarioPedido { get; set; } = 0;

        public int idUsuarioChofer { get; set; } = 0;

        public DateTime fecha { get; set; } = DateTime.Now;
    }
}
