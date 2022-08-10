using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Usuarios
{
    public class UsuarioVO
    {
        public int idUsuario { get; set; }

        public int idCategoria { get; set; }

        public CategoriaUsuarioVO? categoriaUsuario { get; set; }

        public string usuario { get; set; }

        public string password { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string? email { get; set; }

        public string? telefono { get; set; }

    }
}
