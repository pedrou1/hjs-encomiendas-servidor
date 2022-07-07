using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;

namespace hjs_encomiendas_servidor.Dominio
{
    public class UsuarioDom : IDominio
    {
        private readonly UsuarioContext context;

        public UsuarioDom(UsuarioContext _context)
        {
            context = _context;
        }

        public OperationResult iniciarSesion(Usuario usuario)
        {

            context.Usuarios.Add(usuario);

            context.SaveChanges();

            return OperationResult.Success;
        }

        public List<Usuario> obtenerUsuarios(int position)
        {
            var usuarios = context.Usuarios
                .OrderBy(u => u.idUsuario)
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
    }
}
