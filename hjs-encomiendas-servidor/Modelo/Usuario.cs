using hjs_encomiendas_servidor.Common.ValueObjects;
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
            this.categoriaUsuario = usuarioVO.categoriaUsuario;
            this.usuario = usuarioVO.usuario;
            this.password = usuarioVO.password;
            this.nombre = usuarioVO.nombre;
            this.apellido = usuarioVO.apellido;
            this.email = usuarioVO.email;
            this.telefono = usuarioVO.telefono;
        }

        [Key]
        public int idUsuario { get; set; }

        [ForeignKey("categoriaUsuario")]
        public int idCategoria { get; set; }

        public CategoriaUsuario categoriaUsuario { get; set; }

        [StringLength(250)]
        public string usuario { get; set; }

        [StringLength(250), JsonIgnore]
        public string password { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [StringLength(150)]
        public string apellido { get; set; }

        [StringLength(250)]
        public string? email { get; set; }

        [StringLength(150)]
        public string? telefono { get; set; }

        [JsonIgnore]
        public bool activo { get; set; } = true;

    }
}
