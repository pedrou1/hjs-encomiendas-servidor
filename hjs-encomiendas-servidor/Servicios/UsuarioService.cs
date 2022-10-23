using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public UsuarioService(ProjectContext context, IJWTAuthenticationManager jWTAuthenticationManager)
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

        [AllowAnonymous]
        [HttpPost("registrar")]
        public BaseMethodOut registrarUsuario(UsuarioVO usuario)
        {
            try
            {
                BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.InvalidUser };

                if (usuario == null) return result;

                if (usuario.usuario != "" && dUsuario.existeNombreUsuario(usuario.usuario)) {
                    result.OperationResult = OperationResult.UsernameAlreadyExist;
                    return result;
                }

                result = dUsuario.agregarUsuario(usuario);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("modificar")]
        public BaseMethodOut modificarUsuario(UsuarioVO usuario)
        {

            if (usuario == null) return new BaseMethodOut { OperationResult = OperationResult.InvalidUser };

            try
            {

                if (usuario.usuario != "" && dUsuario.obtenerExisteUsuarioPorUsuario(usuario.idUsuario, usuario.usuario))
                {
                    return new BaseMethodOut { OperationResult = OperationResult.UsernameAlreadyExist };
                }
                
                var result = dUsuario.modificarUsuario(usuario);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("modificar-contrasenia")]
        public BaseMethodOut modificarConstraseniaUsuario(UsuarioVO usuario)
        {

            if (usuario == null) return new BaseMethodOut { OperationResult = OperationResult.InvalidUser };

            try
            {
                var result = dUsuario.modificarConstraseniaUsuario(usuario);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        public JsonResult obtenerUsuarios([FromQuery] GetDataInUsuariosVO getData)
        {
            try
            {
                int[] categorias = null;
                if (getData.categorias != null)
                {
                    categorias = JsonConvert.DeserializeObject<int[]>(getData.categorias);
                }
                
                UsuariosVO usuarios = dUsuario.obtenerUsuarios(getData, categorias);

                JsonResult json = new JsonResult(usuarios);

                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("cantidad/pedidos")]
        public JsonResult obtenerUsuariosCantidadPedidos([FromQuery] GetDataInUsuariosVO getData)
        {
            try
            {
                UsuariosVO usuarios = dUsuario.obtenerUsuariosCantidadPedidos(getData);

                JsonResult json = new JsonResult(usuarios);

                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("por-mes")]
        public JsonResult obtenerCantidadClientesPorMes()
        {
            List<int> cantidadPedidos = dUsuario.obtenerCantidadClientesPorMes();

            JsonResult json = new JsonResult(cantidadPedidos);
            return json;
        }

        [HttpGet("{idUsuario}")]
        public JsonResult otenerUsuario(int idUsuario)
        {
            try
            {
                Usuario? usuario = dUsuario.obtenerUsuario(idUsuario);

                JsonResult json = new JsonResult(usuario);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{idUsuario}")]
        public BaseMethodOut borrarUsuario(int idUsuario)
        {
            try
            {
                return dUsuario.eliminarUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
