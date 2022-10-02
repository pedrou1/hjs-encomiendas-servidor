using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte
{
    public class UnidadTransporteVO
    {
        public int idUnidadTransporte { get; set; }

        public string nombre { get; set; }

        public int promedioConsumo { get; set; }

        public int capacidad { get; set; }

        public Usuario? chofer { get; set; }

        public int idChofer { get; set; }
    }
}
