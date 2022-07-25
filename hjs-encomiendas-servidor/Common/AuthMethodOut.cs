using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common
{
    public class AuthMethodOut : BaseMethodOut
    {
        public string jwtToken { get; set; }

        public Usuario usuario { get; set; }
    }
}
