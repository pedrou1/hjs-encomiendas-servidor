using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hjs_encomiendas_servidor.Servicios
{
    [Authorize]
    [Route("api/unidadtransporte")]
    [ApiController]
    public class UnidadTransporteService : ControllerBase
    {
        private readonly dUnidadTransporte dUnidadTransporte;

        public UnidadTransporteService(ProjectContext context)
        {
            dUnidadTransporte = new dUnidadTransporte(context);
        }

        [HttpPost("crear")]
        public BaseMethodOut agregarUnidad(UnidadTransporteVO unidadTransporte)
        {
            try
            {
                var result = dUnidadTransporte.agregarUnidadTransporte(unidadTransporte);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerUnidades([FromQuery] GetDataInVO getData)
        {
            try
            {
                UnidadesTransporteVO unidades = dUnidadTransporte.obtenerUnidadesTransportes(getData);

                JsonResult json = new JsonResult(unidades);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{idUnidad}")]
        public JsonResult otenerUnidad(int idUnidad)
        {
            try
            {
                UnidadTransporte? unidad = dUnidadTransporte.obtenerUnidadTransporte(idUnidad);

                JsonResult json = new JsonResult(unidad);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{idUsuario}")]
        public BaseMethodOut borrarUnidad(int idUnidad)
        {
            try
            {
                return dUnidadTransporte.eliminarUnidadTransporte(idUnidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
