namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class GetDataInPedidoVO : GetDataInVO
    {
        public int idUsuarioPedido { get; set; } = 0;

        public int idUsuarioChofer { get; set; } = 0;
        
        public DateTime fecha { get; set; } = DateTime.Now;
        
        public int estado { get; set; } = 0;

        public string estados { get; set; } = "";

        public DateTime? fechaDesde { get; set; }

        public DateTime? fechaHasta { get; set; }

        public int idUnidad { get; set; } = 0;
    }
}
