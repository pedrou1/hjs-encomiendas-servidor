using hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hjs_encomiendas_servidor.Modelo
{
    public class UnidadTransporte
    {
        public UnidadTransporte()
        {
        }
        
        public UnidadTransporte(UnidadTransporteVO unidadTransporteVO)
        {
            this.idUnidadTransporte = unidadTransporteVO.idUnidadTransporte;
            this.promedioConsumo = unidadTransporteVO.promedioConsumo;
            this.capacidad = unidadTransporteVO.capacidad;
            this.nombre = unidadTransporteVO.nombre;
        }

        [Key]
        public int idUnidadTransporte { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        public int promedioConsumo { get; set; }

        public int capacidad { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

    }
}
