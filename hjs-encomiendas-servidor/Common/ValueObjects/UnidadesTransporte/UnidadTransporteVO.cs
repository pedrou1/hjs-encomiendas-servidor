using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte
{
    public class UnidadTransporteVO
    {
        public int idUnidadTransporte { get; set; }

        public string nombre { get; set; }

        public int promedioConsumo { get; set; }

        public Usuario? chofer { get; set; }

        public int idChofer { get; set; }

        public string marca { get; set; } = "";
        
        public string modelo { get; set; } = "";

        public int anio { get; set; }
        
        public string padron { get; set; } = "";
        
        public string matricula { get; set; } = "";

        public DateTime? vtoSeguro { get; set; }

        public DateTime? vtoPatente { get; set; }

        public DateTime? vtoMinisterio { get; set; }

        public DateTime? vtoApplus { get; set; }
    }
}
