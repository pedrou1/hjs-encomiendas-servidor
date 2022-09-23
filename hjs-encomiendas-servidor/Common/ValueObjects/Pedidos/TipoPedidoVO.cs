namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class TipoPedidoVO
    {
        public int idTipoPedido { get; set; }

        public string nombre { get; set; }

        public int tarifa { get; set; }

        public int pesoDesde { get; set; }

        public int pesoHasta { get; set; }
    }
}
