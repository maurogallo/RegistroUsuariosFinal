using System.ComponentModel.DataAnnotations;

namespace RegistroUsuarios.Modelo
{
    public class Usuarios
    {
        [Key]
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
    }
}
