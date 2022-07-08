using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dUsuario : IDominio
    {
        private readonly UsuarioContext context;

        public dUsuario(UsuarioContext _context)
        {
            context = _context;
        }

        public Usuario iniciarSesion(UsuarioSignInVO usuarioIn)
        {
            var usuarioOut = context.Usuarios.FirstOrDefault(u => u.usuario == usuarioIn.usuario
                      && u.password == usuarioIn.password && u.activo == true);

            return usuarioOut;
        }

        public OperationResult agregarUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);

            context.SaveChanges();

            return OperationResult.Success;
        }

        public List<Usuario> obtenerUsuarios(int position)
        {
            var usuarios = context.Usuarios
                .Where(u => u.activo == true)
                .OrderBy(u => u.nombre)
                .Skip(position)
                .Take(10)
                .ToList();


            String str = "";
            foreach (Usuario u in usuarios)
            {
                str += $"Id:    {u.idUsuario}";
                str += $"Nombre:  {u.nombre}";
            }

            return usuarios;
        }

        public bool existeNombreUsuario(String usuarioIn)
        {

           bool exists = context.Usuarios.Any(u => u.usuario == usuarioIn && u.activo == true);

            return exists;
        }

        public OperationResult eliminarUsuario(int idUsuario)
        {
            var usuario = context.Usuarios.FirstOrDefault(u => u.idUsuario == idUsuario);

            if (usuario != null)
            {
                usuario.activo = false;
                context.SaveChanges();

                return OperationResult.Success;
            }

            return OperationResult.Error;
        }

    }
}
