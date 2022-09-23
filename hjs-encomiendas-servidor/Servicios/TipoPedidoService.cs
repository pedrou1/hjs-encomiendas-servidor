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
    [Route("api/tipospedido")]
    [ApiController]
    public class TipoPedidoService : ControllerBase
    {
        private readonly dTipoPedido dTipoPedido;

        public TipoPedidoService(ProjectContext context)
        {
            dTipoPedido = new dTipoPedido(context);
        }

        [HttpPost("crear")]
        public BaseMethodOut agregarTipoPedido(TipoPedidoVO tipoPedido)
        {
            try
            {
                var result = dTipoPedido.agregarTipoPedido(tipoPedido);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerTiposPedido([FromQuery] GetDataInVO getData)
        {
            try
            {
                TiposPedidoVO tiposPedido = dTipoPedido.obtenerTiposPedido(getData);

                JsonResult json = new JsonResult(tiposPedido);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{idTipoPedido}")]
        public JsonResult otenerTipoPedido(int idTipoPedido)
        {
            try
            {
                TipoPedido? tipoPedido = dTipoPedido.obtenerTipoPedido(idTipoPedido);

                JsonResult json = new JsonResult(tipoPedido);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("modificar")]
        public BaseMethodOut modificarPedido(TipoPedidoVO tipoPedido)
        {

            if (tipoPedido == null) return new BaseMethodOut { OperationResult = OperationResult.Error };

            try
            {
                var result = dTipoPedido.modificarPedido(tipoPedido);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{idTipoPedido}")]
        public BaseMethodOut borrarTipoPedido(int idTipoPedido)
        {
            try
            {
                return dTipoPedido.eliminarTipoPedido(idTipoPedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
