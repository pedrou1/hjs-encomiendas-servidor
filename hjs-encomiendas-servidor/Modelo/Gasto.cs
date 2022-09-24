using hjs_encomiendas_servidor.Common.ValueObjects.Gastos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace hjs_encomiendas_servidor.Modelo
{
    public class Gasto
    {
        public Gasto()
        {
        }
        
        public Gasto(GastoVO gastoVO)
        {
            this.idGasto = gastoVO.idGasto;
            this.idUsuario = gastoVO.idUsuario;
            this.idTransporte = gastoVO.idTransporte;
            this.descripcion = gastoVO.descripcion;
            this.costo = gastoVO.costo;
            this.fecha = gastoVO.fecha;
        }

        [Key]
        public int idGasto { get; set; }

        [ForeignKey("usuario")]
        public int idUsuario { get; set; }
        
        public Usuario usuario { get; set; }
        
        [ForeignKey("transporte")]
        public int idTransporte { get; set; }

        public UnidadTransporte transporte { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }
        
        public int costo { get; set; }
        
        public DateTime fecha { get; set; }
        
        [JsonIgnore]
        public bool activo { get; set; } = true;

        public void update(GastoVO gastoVO)
        {
            this.idUsuario = gastoVO.idUsuario;
            this.idTransporte = gastoVO.idTransporte;
            this.descripcion = gastoVO.descripcion;
            this.costo = gastoVO.costo;
            this.fecha = gastoVO.fecha;
        }
    }
}
