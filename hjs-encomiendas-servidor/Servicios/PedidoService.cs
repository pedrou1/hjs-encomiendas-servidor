using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public JsonResult obtenerPedidos([FromQuery] GetDataInPedidoVO getData)
        {
            int[] estados = null;
            if (getData.estados != null)
            {
                estados = JsonConvert.DeserializeObject<int[]>(getData.estados);
            }
            
            PedidosVO pedidos = dPedido.obtenerPedidos(getData, estados);

            JsonResult json = new JsonResult(pedidos);
            return json;

        }

        [HttpGet("reservados")]
        public JsonResult obtenerPedidosReservados([FromQuery] GetDataInPedidoVO getData)
        {

            PedidosVO pedidos = dPedido.obtenerPedidosReservados(getData);

            JsonResult json = new JsonResult(pedidos);
            return json;

        }

        [HttpGet("chofer")]
        public JsonResult obtenerPedidosChofer([FromQuery] GetDataInPedidoVO getData)
        {
            PedidosVO pedidos = dPedido.obtenerPedidosChofer(getData);

            JsonResult json = new JsonResult(pedidos);
            return json;

        }

        [HttpGet("chofer/dia/estado")]
        public JsonResult obtenerPedidosDiaTodosChofer([FromQuery] GetDataInPedidoVO getData)
        {
            PedidosVO pedidos = dPedido.obtenerPedidosDiaEstadoChofer(getData);

            JsonResult json = new JsonResult(pedidos);
            return json;

        }

        [HttpGet("por-mes/{anio}")]
        public JsonResult obtenerCantidadPedidosPorMes(int anio)
        {
            List<int> cantidadPedidos = dPedido.obtenerCantidadPedidosPorMes(anio);

            JsonResult json = new JsonResult(cantidadPedidos);
            return json;
        }

        [HttpGet("{idPedido}")]
        public JsonResult otenerPedido(int idPedido)
        {
            Pedido? pedido = dPedido.obtenerPedido(idPedido);

            JsonResult json = new JsonResult(pedido);
            return json;
        }

        [HttpGet("posterior/{idChofer}")]
        public JsonResult obtenerUltimosPedidos(int idChofer)
        {
            PedidosVO? pedido = dPedido.obtenerUltimosPedidos(idChofer);

            JsonResult json = new JsonResult(pedido);
            return json;
        }

        [HttpGet("retirados/{idChofer}")]
        public JsonResult obtenerPedidosRetiradosChofer(int idChofer)
        {
            PedidosVO? pedido = dPedido.obtenerPedidosRetiradosChofer(idChofer);

            JsonResult json = new JsonResult(pedido);
            return json;
        }

        [HttpPut("modificar")]
        public BaseMethodOut modificarPedido(PedidoVO pedidoVO)
        {

            if (pedidoVO == null) return new BaseMethodOut { OperationResult = OperationResult.Error };

            try
            {
                var result = dPedido.modificarPedido(pedidoVO);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("modificar/estado/{idPedido}/{estado}/{distancia}")]
        public BaseMethodOut actualizarEstadoPedido(int idPedido, int estado, int distancia)
        {

            if (idPedido == 0) return new BaseMethodOut { OperationResult = OperationResult.Error };

            try
            {
                var result = dPedido.actualizarEstadoPedido(idPedido, estado, distancia);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{idPedido}")]
        public BaseMethodOut borrarPedido(int idPedido)
        {
            return dPedido.eliminarPedido(idPedido);

        }
    }
}
