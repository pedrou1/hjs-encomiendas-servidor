﻿using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Pedidos
{
    public class PedidoVO
    {
        public int idPedido { get; set; }

        public int idChofer { get; set; }
        
        public Usuario? chofer { get; set; }

        public int idCliente { get; set; }
        
        public Usuario? cliente { get; set; }

        public int idTransporte { get; set; }

        public UnidadTransporte? transporte { get; set; }

        public int idTipoPedido { get; set; }
        
        public TipoPedido? tipoPedido { get; set; }

        public int estado { get; set; }

        public string? horaLimite { get; set; }

        public int orden { get; set; }
        
        public int tamaño { get; set; }

        public int peso { get; set; }

        public int cubicaje { get; set; }

        public int tarifa { get; set; }

        public int distanciaRecorrida { get; set; }

        public string? nombreDireccion { get; set; }

        public float? latitude { get; set; }

        public float? longitude { get; set; }

        public string? apartamento { get; set; }
        
        public string? nroPuerta { get; set; }
        
        public string? descripcion { get; set; }

        public DateTime? fechaCreacion { get; set; }

        public DateTime? fechaRetiro { get; set; }

        public DateTime? fechaEntrega { get; set; }

    }
}
