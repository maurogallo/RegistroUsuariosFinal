using RegistroUsuarios.Modelo;

namespace RegistroUsuarios.Services
{
    public interface IUsuariosServices
    {
        public IEnumerable<Usuarios> GetUsuariosList();
        public Usuarios GetUsuarioById(int id);
        public Usuarios AddUsuarios(Usuarios usuarios);
        public Usuarios UpdateUsuarios(Usuarios usuarios);
        public bool DeleteUsuarios(int Id);
    }
}
