using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Modelo;

namespace hjs_encomiendas_servidor.Common.ValueObjects.Gastos
{
    public class GastosVO : GetDataOutVO
    {
        public List<Gasto>? gastos { get; set; }
    }
}
