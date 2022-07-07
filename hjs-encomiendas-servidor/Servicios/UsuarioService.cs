using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace hjs_encomiendas_servidor.Servicios
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioService : ControllerBase
    {
        private readonly UsuarioDom usuarioDom;

        public UsuarioService(UsuarioContext context)
        {
            usuarioDom = new UsuarioDom(context);
        }

        [HttpPost]
        public JsonResult Post(Usuario usuario)
        {
            usuarioDom.iniciarSesion(usuario);

            return new JsonResult(OperationResult.UsernameAlreadyExist);

            return new JsonResult("ok");
        }

        [HttpGet("{index}")]
        public JsonResult Get(int index)
        {

            List<Usuario> usuarios = usuarioDom.obtenerUsuarios(index);

            String str = JsonSerializer.Serialize(usuarios);

            JsonResult json = new JsonResult(str);
            return json;

        }

     
    }
}
