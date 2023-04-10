using UsuariosMongoDB.Entities;

namespace UsuariosMongoDB.Repositories
{
    public interface IUsuarioService
    {
        public Task<List<UsuarioDetails>> UsuarioListAsync();

        public Task<UsuarioDetails> GetUsuarioDetailsByIdAsync(int usuariosId);

        public Task AddUsuarioAsync(UsuarioDetails usuarioDetails);

        public Task UpdateUsuarioAsync(int usuarioId, UsuarioDetails usuarioDetails);

        public Task DeleteUsuarioAsync(int usuarioId);

        public Task<List<int>> GetUsuariosId(string nombre, string apellido, string nombreUsuario);
    }
}
