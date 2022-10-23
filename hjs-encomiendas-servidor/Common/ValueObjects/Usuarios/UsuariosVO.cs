using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Usuarios
{
    public class UsuariosVO : GetDataOutVO
    {
        public List<Usuario>? usuarios { get; set; }

        public List<UsuariosInformeVO?>? usuariosInforme { get; set; }
        

    }
}
