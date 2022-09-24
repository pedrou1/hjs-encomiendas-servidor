using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Gastos;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace hjs_encomiendas_servidor.Servicios
{
    [Authorize]
    [Route("api/gastos")]
    [ApiController]
    public class GastoService : ControllerBase
    {
        private readonly dGasto dGasto;

        public GastoService(ProjectContext context)
        {
            dGasto = new dGasto(context);
        }


        [HttpPost("crear")]
        public BaseMethodOut agregarGasto(GastoVO gasto)
        {
            try
            {
                var result = dGasto.agregarGasto(gasto);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerGastos([FromQuery] GetDataInVO getData)
        {
            GastosVO gastos = dGasto.obtenerGastos(getData);

            JsonResult json = new JsonResult(gastos);
            return json;

        }

        [HttpGet("{idGasto}")]
        public JsonResult otenerGasto(int idGasto)
        {
            Gasto? gasto = dGasto.obtenerGasto(idGasto);

            JsonResult json = new JsonResult(gasto);
            return json;
        }

        [HttpPut("modificar")]
        public BaseMethodOut modificarGasto(GastoVO gastoVO)
        {

            if (gastoVO == null) return new BaseMethodOut { OperationResult = OperationResult.Error };

            try
            {
                var result = dGasto.modificarGasto(gastoVO);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{idGasto}")]
        public BaseMethodOut borrarGasto(int idGasto)
        {
            return dGasto.eliminarGasto(idGasto);

        }
    }
}
