namespace hjs_encomiendas_servidor.Common.ValueObjects.Usuarios
{
    public class GetDataInUsuariosVO : GetDataInVO
    {
        public int Tipo { get; set; }

        public string categorias { get; set; } = "";
    }
}
