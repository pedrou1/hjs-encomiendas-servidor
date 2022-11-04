using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Gastos
{
    public class GastoVO
    {
        public int idGasto { get; set; }
        
        public int idUsuario { get; set; }

        public Usuario? usuario { get; set; }
        
        public int? idTransporte { get; set; }

        public UnidadTransporte? transporte { get; set; }

        public string descripcion { get; set; } = "";

        public int costo { get; set; }

        public DateTime fecha { get; set; }
        
    }
}

