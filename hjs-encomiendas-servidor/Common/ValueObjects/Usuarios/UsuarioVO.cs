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

        public string? ci { get; set; }

        public string? rut { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string? email { get; set; }

        public string? direccion { get; set; }

        public string? telefono { get; set; }

        public string? telefono2 { get; set; }
        
        public string? apartamento { get; set; }
        
        public string? nroPuerta { get; set; }

        public DateTime? fechaCreacion { get; set; }

        public UnidadTransporte? unidadTransporte { get; set; }

    }
}
