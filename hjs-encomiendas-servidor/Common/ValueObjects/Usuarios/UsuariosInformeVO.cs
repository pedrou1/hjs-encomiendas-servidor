using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Usuarios
{
    public class UsuariosInformeVO
    {
        public Usuario? usuario { get; set; }

        public int cantidadPedidos { get; set; } = 0;
    }
}
