using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Usuarios
{
    public class UsuariosVO
    {
        public List<Usuario>? usuarios { get; set; }
        
        public int totalRows { get; set; }
    }
}
