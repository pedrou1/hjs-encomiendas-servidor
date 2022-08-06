using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte
{
    public class UnidadesTransporteVO : GetDataOutVO
    {
        public List<UnidadTransporte>? unidadesTransporte { get; set; }
    }
}
