using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hjs_encomiendas_servidor.Servicios
{
    [Authorize]
    [Route("api/pedido")]
    [ApiController]
    public class PedidoService : ControllerBase
    {
        private readonly dPedido dPedido;

        public PedidoService(ProjectContext context)
        {
            dPedido = new dPedido(context);
        }


        [HttpPost("crear")]
        public BaseMethodOut agregarPedido(PedidoVO pedido)
        {
            try
            {
                var result = dPedido.agregarPedido(pedido);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerPedidos([FromQuery] GetDataInVO getData)
        {
            PedidosVO pedidos = dPedido.obtenerPedidos(getData);

            JsonResult json = new JsonResult(pedidos);
            return json;

        }

        [HttpGet("{idPedido}")]
        public JsonResult otenerPedido(int idPedido)
        {
            Pedido? pedido = dPedido.obtenerPedido(idPedido);

            JsonResult json = new JsonResult(pedido);
            return json;
        }

        [HttpDelete("{idPedido}")]
        public BaseMethodOut borrarPedido(int idPedido)
        {
            return dPedido.eliminarPedido(idPedido);

        }
    }
}
