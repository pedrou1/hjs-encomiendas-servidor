using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace hjs_encomiendas_servidor.Modelo
{
    public class Pedido
    {
        public Pedido()
        {
        }

        public Pedido(PedidoVO pedidoVO)
        {
            this.idPedido = pedidoVO.idPedido;
            this.idChofer = pedidoVO.idChofer;
            this.idCliente = pedidoVO.idCliente;
            this.idTransporte = pedidoVO.idTransporte;
            this.estado = pedidoVO.estado;
            this.horaLimite = pedidoVO.horaLimite;
            this.orden = pedidoVO.orden;
            this.idTipoPedido = pedidoVO.idTipoPedido;
            this.distanciaRecorrida = pedidoVO.distanciaRecorrida;
            this.fechaCreacion = (DateTime)(pedidoVO.fechaCreacion != null ? pedidoVO.fechaCreacion : DateTime.Now);
            this.fechaRetiro = pedidoVO.fechaRetiro;
            this.fechaEntrega = pedidoVO.fechaEntrega;
            this.nombreDireccion = pedidoVO.nombreDireccion;
            this.latitude = pedidoVO.latitude;
            this.longitude = pedidoVO.longitude;
            this.apartamento = pedidoVO.apartamento;
            this.nroPuerta = pedidoVO.nroPuerta;
            this.descripcion = pedidoVO.descripcion;
        }

        [Key]
        public int idPedido { get; set; }

        [ForeignKey("chofer")]
        public int idChofer { get; set; }

        public Usuario chofer { get; set; }

        [ForeignKey("cliente")]
        public int idCliente { get; set; }

        public Usuario cliente { get; set; }

        [ForeignKey("transporte")]
        public int idTransporte { get; set; }
        
        public UnidadTransporte transporte { get; set; }

        [ForeignKey("tipoPedido")]
        public int idTipoPedido { get; set; }

        public TipoPedido tipoPedido { get; set; }

        public int estado { get; set; } = 1;

        [StringLength(250)]
        public string? horaLimite { get; set; }

        public int orden { get; set; }

        public int? distanciaRecorrida { get; set; }

        [StringLength(250)]
        public string? nombreDireccion { get; set; }

        public float? latitude { get; set; }

        public float? longitude { get; set; }

        [StringLength(250)]
        public string? apartamento { get; set; }

        [StringLength(60)]
        public string? nroPuerta { get; set; }

        [StringLength(250)]
        public string? descripcion { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime? fechaRetiro { get; set; }

        public DateTime? fechaEntrega { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

        public void update(PedidoVO pedidoVO)
        {
            this.idChofer = pedidoVO.idChofer;
            this.idCliente = pedidoVO.idCliente;
            this.idTransporte = pedidoVO.idTransporte;
            this.estado = pedidoVO.estado;
            this.horaLimite = pedidoVO.horaLimite;
            this.orden = pedidoVO.orden;
            this.idTipoPedido = pedidoVO.idTipoPedido;
            this.distanciaRecorrida = pedidoVO.distanciaRecorrida;
            this.fechaCreacion = (DateTime)(pedidoVO.fechaCreacion != null ? pedidoVO.fechaCreacion : DateTime.Now);
            this.fechaRetiro = pedidoVO.fechaRetiro;
            this.fechaEntrega = pedidoVO.fechaEntrega;
            this.nombreDireccion = pedidoVO.nombreDireccion;
            this.latitude = pedidoVO.latitude;
            this.longitude = pedidoVO.longitude;
            this.apartamento = pedidoVO.apartamento;
            this.nroPuerta = pedidoVO.nroPuerta;
            this.descripcion = pedidoVO.descripcion;
        }

    }
}
