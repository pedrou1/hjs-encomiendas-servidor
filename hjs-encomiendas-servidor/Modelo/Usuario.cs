using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace hjs_encomiendas_servidor.Modelo
{
    public class Usuario
    {
        public Usuario()
        { 
        }
            public Usuario(UsuarioVO usuarioVO)
        {
            this.idUsuario = usuarioVO.idUsuario;
            this.usuario = usuarioVO.usuario;
            this.password = usuarioVO.password;
            this.nombre = usuarioVO.nombre;
            this.apellido = usuarioVO.apellido;
            this.email = usuarioVO.email;
            this.telefono = usuarioVO.telefono;
            this.direccion = usuarioVO.direccion;
            this.fechaCreacion = (DateTime)(usuarioVO.fechaCreacion != null ? usuarioVO.fechaCreacion : DateTime.Now);
            this.telefono2 = usuarioVO.telefono2;
            this.apartamento = usuarioVO.apartamento;
            this.nroPuerta = usuarioVO.nroPuerta;
            this.ci = usuarioVO.ci;
            this.rut = usuarioVO.rut;
        }

        [Key]
        public int idUsuario { get; set; }

        [ForeignKey("categoriaUsuario")]
        public int idCategoria { get; set; }

        public CategoriaUsuario categoriaUsuario { get; set; }

        [StringLength(250)]
        public string usuario { get; set; }

        [StringLength(1000), JsonIgnore]
        public string password { get; set; }

        [StringLength(20)]
        public string? ci { get; set; }

        [StringLength(20)]
        public string? rut { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [StringLength(150)]
        public string apellido { get; set; }

        [StringLength(250)]
        public string? email { get; set; }

        [StringLength(150)]
        public string? telefono { get; set; }

        [StringLength(150)]
        public string? telefono2 { get; set; }

        [StringLength(150)]
        public string? apartamento { get; set; }

        [StringLength(150)]
        public string? nroPuerta { get; set; }

        [StringLength(250)]
        public string? direccion { get; set; }

        public DateTime fechaCreacion { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

        public void update(UsuarioVO usuarioVO)
        {
            this.usuario = usuarioVO.usuario;
            this.nombre = usuarioVO.nombre;
            this.apellido = usuarioVO.apellido;
            this.email = usuarioVO.email;
            this.telefono = usuarioVO.telefono;
            this.idCategoria = usuarioVO.categoriaUsuario.idCategoria;
            this.direccion = usuarioVO.direccion;
            this.telefono2 = usuarioVO.telefono2;
            this.apartamento = usuarioVO.apartamento;
            this.nroPuerta = usuarioVO.nroPuerta;
            this.fechaCreacion = (DateTime)(usuarioVO.fechaCreacion != null ? usuarioVO.fechaCreacion : DateTime.Now);
            this.ci = usuarioVO.ci;
            this.rut = usuarioVO.rut;
        }

    }
}
