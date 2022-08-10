using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dUsuario : IDominio
    {
        private readonly ProjectContext context;
        
        public dUsuario(ProjectContext _context)
        {
            context = _context;
        }
        
        public Usuario iniciarSesion(UsuarioSignInVO usuarioIn)
        {
            string hashedPassword = Utils.hashPassword(usuarioIn.password);
            
            var usuarioOut = context.Usuarios.FirstOrDefault(u => u.usuario == usuarioIn.usuario
                      && u.password == hashedPassword && u.activo == true);

            return usuarioOut;
        }

        public BaseMethodOut? agregarUsuario(UsuarioVO usuarioVO)
        {
            string hashedPassword = Utils.hashPassword(usuarioVO.password);
            usuarioVO.password = hashedPassword;
            
            Usuario usuario = new Usuario(usuarioVO);
            CategoriaUsuario? categoria = obtenerCategoria(usuarioVO.categoriaUsuario.idCategoria);
            if (categoria == null) return null;
            
            usuario.categoriaUsuario = categoria;

            context.Usuarios.Add(usuario);

            context.SaveChanges();

            return new BaseMethodOut { OperationResult = OperationResult.Success };
        }

        public UsuariosVO obtenerUsuarios(GetDataInVO getData)
        {
            var qry = (from u in context.Usuarios where u.activo == true select u);
            var count = qry.Count();
            var usuarios = qry.OrderBy(u => u.nombre)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize)
                .ToList();

            UsuariosVO usuariosVO = new UsuariosVO{ usuarios = usuarios, totalRows = count, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public Usuario? obtenerUsuario(int idUsuario)
        {
            var usuario = context.Usuarios.Where(u => u.idUsuario == idUsuario && u.activo == true).FirstOrDefault();
            CategoriaUsuario? categoria = obtenerCategoria(usuario.idCategoria);
            usuario.categoriaUsuario = categoria;

            return usuario;
        }

        public bool existeNombreUsuario(String usuarioIn)
        {

           bool exists = context.Usuarios.Any(u => u.usuario == usuarioIn && u.activo == true);

            return exists;
        }

        public BaseMethodOut modificarUsuario(UsuarioVO usuarioVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var usuario = obtenerUsuario(usuarioVO.idUsuario);

            if (usuario != null)
            {
                if (usuarioVO.password != null) { 
                string hashedPassword = Utils.hashPassword(usuarioVO.password);
                usuario.password = hashedPassword;
                }
                usuario.update(usuarioVO);
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut eliminarUsuario(int idUsuario)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var usuario = obtenerUsuario(idUsuario);

            if (usuario != null)
            {
                usuario.activo = false;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public CategoriaUsuario? obtenerCategoria(int idCategoria)
        {
            var categoria = context.CategoriaUsuario.Where(c => c.idCategoria == idCategoria).FirstOrDefault();

            return categoria;
        }

    }
}
