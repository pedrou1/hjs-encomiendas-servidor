using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
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
        private readonly dUsuario dUsuario;

        public UsuarioService(UsuarioContext context)
        {
            dUsuario = new dUsuario(context);
        }

        [HttpPost("login")]

        public OperationResult SignIn(UsuarioSignInVO userSignIn)
        {
            if (userSignIn == null) return OperationResult.InvalidUser;

            try
            {
                Usuario usuario = dUsuario.iniciarSesion(userSignIn);

                return usuario != null ? OperationResult.Success : OperationResult.InvalidUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("registrar")]
        public OperationResult registrarUsuario(UsuarioVO usuario)
        {

            if (usuario == null) return OperationResult.InvalidUser;
            try
            {
                if (dUsuario.existeNombreUsuario(usuario.usuario))
                    return OperationResult.UsernameAlreadyExist;

                dUsuario.agregarUsuario(new Usuario(usuario));

                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{index}")]
        public JsonResult obtenerUsuarios(int index)
        {
            List<Usuario> usuarios = dUsuario.obtenerUsuarios(index);

            String str = JsonSerializer.Serialize(usuarios);

            JsonResult json = new JsonResult(str);
            return json;

        }

        [HttpDelete("{idUsuario}")]
        public OperationResult borrarUsuario(int idUsuario)
        {
            return dUsuario.eliminarUsuario(idUsuario);

        }

    }
}
