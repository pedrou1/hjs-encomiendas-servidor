using hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.idChofer = unidadTransporteVO.idChofer;
            this.promedioConsumo = unidadTransporteVO.promedioConsumo;
            this.nombre = unidadTransporteVO.nombre;
            this.marca = unidadTransporteVO.marca;
            this.modelo = unidadTransporteVO.modelo;
            this.anio = unidadTransporteVO.anio;
            this.padron = unidadTransporteVO.padron;
            this.matricula = unidadTransporteVO.matricula;
            this.vtoSeguro = unidadTransporteVO.vtoSeguro;
            this.vtoPatente = unidadTransporteVO.vtoPatente;
            this.vtoMinisterio = unidadTransporteVO.vtoMinisterio;
            this.vtoApplus = unidadTransporteVO.vtoApplus;
        }

        [Key]
        public int idUnidadTransporte { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        public int promedioConsumo { get; set; }

        [ForeignKey("chofer")]
        public int idChofer { get; set; }

        public Usuario? chofer { get; set; }

        [StringLength(150)]
        public string? marca { get; set; } = "";

        [StringLength(150)]
        public string? modelo { get; set; } = "";

        public int? anio { get; set; } = 0;

        [StringLength(150)]
        public string? padron { get; set; } = "";

        [StringLength(150)]
        public string? matricula { get; set; } = "";

        public DateTime? vtoSeguro { get; set; }

        public DateTime? vtoPatente { get; set; }

        public DateTime? vtoMinisterio { get; set; }

        public DateTime? vtoApplus { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

        public void update(UnidadTransporteVO unidadTransporteVO)
        {
            this.idChofer = unidadTransporteVO.idChofer;
            this.promedioConsumo = unidadTransporteVO.promedioConsumo;
            this.nombre = unidadTransporteVO.nombre;
            this.marca = unidadTransporteVO.marca;
            this.modelo = unidadTransporteVO.modelo;
            this.anio = unidadTransporteVO.anio;
            this.padron = unidadTransporteVO.padron;
            this.matricula = unidadTransporteVO.matricula;
            this.vtoSeguro = unidadTransporteVO.vtoSeguro;
            this.vtoPatente = unidadTransporteVO.vtoPatente;
            this.vtoMinisterio = unidadTransporteVO.vtoMinisterio;
            this.vtoApplus = unidadTransporteVO.vtoApplus;
        }

    }
}
