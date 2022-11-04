using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        public List<int> obtenerCantidadClientesPorMes(int anio)
        {
            if (anio == 0)
            {
                anio = DateTime.Now.Year;
            }
            
            var usuarios = Enumerable.Range(1, 12).Select(i => context.Usuarios.Where(u => u.fechaCreacion.Month == i && u.fechaCreacion.Year == anio && 
            u.idCategoria != ((int)Constantes.CATEGORIA_ADMINISTRADOR) && u.idCategoria != ((int)Constantes.CATEGORIA_CHOFER)).Count()).ToList();

            return usuarios;
        }

        public UsuariosVO obtenerUsuarios(GetDataInUsuariosVO getData, int[] categorias)
        {
            var query = (from u in context.Usuarios where u.activo == true select u);

         
            string[] filters = JsonConvert.DeserializeObject<string[]>(getData.filters);
            if (filters != null)
            {
                foreach (string filter in filters)
                {
                    if (!string.IsNullOrEmpty(filter))
                    {
                        query = query.Where(collection => EF.Functions.Like(collection.nombre, "%" + filter + "%") 
                        || EF.Functions.Like(collection.apellido, "%" + filter + "%") || (collection.telefono != null && EF.Functions.Like(collection.telefono, "%" + filter + "%"))
                        || (collection.rut != null && EF.Functions.Like(collection.rut, "%" + filter + "%")) || (collection.ci != null && EF.Functions.Like(collection.ci, "%" + filter + "%"))
                        || (collection.email != null && EF.Functions.Like(collection.email, "%" + filter + "%")) || (collection.usuario != null && EF.Functions.Like(collection.usuario, "%" + filter + "%")));
                    }
                }
            }

            if (categorias != null && categorias.Length > 0)
            {
                query = query.Where(collection => categorias.Contains(collection.idCategoria));
            }

            if (getData.Tipo != 0)
            {
                query = query.Where(collection => collection.idCategoria == getData.Tipo);
            }
            
            var count = query.Count();
            var usuarios = query.OrderBy(u => u.nombre)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize).Include(u => u.categoriaUsuario)
                .ToList();

            UsuariosVO usuariosVO = new UsuariosVO{ usuarios = usuarios, totalRows = count, OperationResult = OperationResult.Success };
            
            return usuariosVO;
        }

        public UsuariosVO obtenerUsuariosCantidadPedidos(GetDataInUsuariosVO getData)
        {
            List<UsuariosInformeVO?> usuariosInformeOut = new List<UsuariosInformeVO?>();
            int total = 0;
            if (getData.Tipo == 1)
            {
                var query = (from u in context.Usuarios
                             where u.activo == true && u != null
                             join p in context.Pedido on u.idUsuario equals p.idCliente
                             where p.activo == true && p != null
                             group u by p.idCliente into g
                             orderby g.Count() descending
                             select new UsuariosInformeVO
                             {
                                 usuario = g.FirstOrDefault(),
                                 cantidadPedidos = g.Count()
                             });

                total = countInformeUsuarios(getData.Tipo);
                usuariosInformeOut = query.Skip(getData.PageIndex).Take(getData.PageSize).ToList();

            }
            else
            {
                var query = (from u in context.Usuarios
                             where u.activo == true && u != null
                             join p in context.Pedido on u.idUsuario equals p.idChofer
                             where p.activo == true && p != null
                             group u by p.idChofer into g
                             orderby g.Count() descending
                             select new UsuariosInformeVO
                             {
                                 usuario = g.FirstOrDefault(),
                                 cantidadPedidos = g.Count()
                             });

                total = countInformeUsuarios(getData.Tipo);
                usuariosInformeOut = query.Skip(getData.PageIndex).Take(getData.PageSize).ToList();

            }

            UsuariosVO usuariosVO = new UsuariosVO { usuariosInforme = usuariosInformeOut, totalRows = total, OperationResult = OperationResult.Success };
            
            return usuariosVO;
        }

        public int countInformeUsuarios(int tipo)
        {
            List<UsuariosInformeVO?> usuariosInformeOut = new List<UsuariosInformeVO?>();
            int total = 0;

            if(tipo == 1) {
                var query = (from u in context.Usuarios
                             where u.activo == true && u != null
                             join p in context.Pedido on u.idUsuario equals p.idCliente
                             where p.activo == true && p != null
                             select u).GroupBy(x => x.idUsuario);

                total = query.Count();
            }
            else
            {
                var query = (from u in context.Usuarios
                             where u.activo == true && u != null
                             join p in context.Pedido on u.idUsuario equals p.idChofer
                             where p.activo == true && p != null
                             select u).GroupBy(x => x.idUsuario);

                total = query.Count();
            }
            
            return total;
        }

        public Usuario? obtenerUsuario(int idUsuario)
        {
            var usuario = context.Usuarios.Where(u => u.idUsuario == idUsuario && u.activo == true).FirstOrDefault();
            CategoriaUsuario? categoria = obtenerCategoria(usuario.idCategoria);
            usuario.categoriaUsuario = categoria;

            return usuario;
        }

        public Usuario? obtenerChoferDeReserva()
        {
            var query = (from u in context.Usuarios
                         where u.activo == true && u.idCategoria == ((int)Constantes.CATEGORIA_CHOFER)
                         join unid in context.UnidadTransporte on u.idUsuario equals unid.idChofer
                         where unid.activo == true && unid != null
                         select u).OrderBy(r => EF.Functions.Random()).Take(1);
            Usuario? usuario = query.FirstOrDefault();

            return usuario;
        }
        

        public bool existeNombreUsuario(String usuarioIn)
        {

           bool exists = context.Usuarios.Any(u => u.usuario == usuarioIn && u.activo == true);

            return exists;
        }

        public bool obtenerExisteUsuarioPorUsuario(int idUsuario, String usuarioIn)
        {

            bool exists = context.Usuarios.Any(u => u.usuario == usuarioIn && u.idUsuario != idUsuario && u.activo == true);

            return exists;
        }

        public BaseMethodOut modificarUsuario(UsuarioVO usuarioVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var usuario = obtenerUsuario(usuarioVO.idUsuario);

            if (usuario != null)
            {
                if (usuarioVO.password != null && usuarioVO.password != "") { 
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

        public BaseMethodOut modificarConstraseniaUsuario(UsuarioVO usuarioVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var usuario = obtenerUsuario(usuarioVO.idUsuario);

            if (usuario != null)
            {
                if (usuarioVO.password != null)
                {
                    string hashedPassword = Utils.hashPassword(usuarioVO.password);
                    usuario.password = hashedPassword;
                }
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
