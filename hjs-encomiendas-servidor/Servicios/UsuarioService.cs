using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace hjs_encomiendas_servidor.Servicios
{
    [Authorize]
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioService : ControllerBase
    {
        private readonly dUsuario dUsuario;
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public UsuarioService(UsuarioContext context, IJWTAuthenticationManager jWTAuthenticationManager)
        {
            dUsuario = new dUsuario(context);
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]

        public BaseMethodOut SignIn(UsuarioSignInVO userSignIn)
        {
            AuthMethodOut result = new AuthMethodOut { OperationResult = OperationResult.InvalidUser };

            if (userSignIn == null) return result;

            try
            {
                Usuario usuario = dUsuario.iniciarSesion(userSignIn);
                if(usuario != null) {
                    var token = jWTAuthenticationManager.Authenticate(userSignIn.usuario, "");
                    result.usuario = usuario;
                    result.jwtToken = token;
                    result.OperationResult = OperationResult.Success;
                }

                return result;
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

                var result = dUsuario.agregarUsuario(usuario);

                if (result == null) return OperationResult.Error;


                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerUsuarios([FromQuery] GetDataVO getData)
            //getData?? paginationData??
        {
            UsuariosVO usuarios = dUsuario.obtenerUsuarios(getData);

            JsonResult json = new JsonResult(usuarios);
            return json;

        }

        [HttpDelete("{idUsuario}")]
        public BaseMethodOut borrarUsuario(int idUsuario)
        {
            return dUsuario.eliminarUsuario(idUsuario);

        }

        // Solo para testing
        [HttpPost("registrarVarios")]
        public OperationResult registrarVariosUsuarios(List<UsuarioVO> usuarios)
        {
            foreach (UsuarioVO usuarioVO in usuarios)
            {
                if (usuarioVO == null) return OperationResult.InvalidUser;
                try
                {
                    if (dUsuario.existeNombreUsuario(usuarioVO.usuario))
                        return OperationResult.UsernameAlreadyExist;

                    var result = dUsuario.agregarUsuario(usuarioVO);

                    if (result == null) return OperationResult.Error;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return OperationResult.Error;
        }

    }
}
