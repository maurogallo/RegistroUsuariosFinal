using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UsuariosMongoDB.Configurations;
using UsuariosMongoDB.Entities;

namespace UsuariosMongoDB.Repositories
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMongoCollection<UsuarioDetails> usuarioCollection;

        public UsuarioService(
         IOptions<UsuarioDBSettings> usuarioDatabaseSetting)
        {
            var mongoClient = new MongoClient(
                usuarioDatabaseSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                usuarioDatabaseSetting.Value.DatabaseName);

            usuarioCollection = mongoDatabase.GetCollection<UsuarioDetails>(
                usuarioDatabaseSetting.Value.UsuarioCollectionName);
        }

        public async Task<List<UsuarioDetails>> UsuarioListAsync()
        {
            return await usuarioCollection.Find(_ => true).ToListAsync();
        }


        public async Task<UsuarioDetails> GetUsuarioDetailsByIdAsync(int usuariosId)
        {
            return await usuarioCollection.Find(x => x.UserId == usuariosId).FirstOrDefaultAsync();
        }


        public async Task AddUsuarioAsync(UsuarioDetails usuarioDetails)
        {
            await usuarioCollection.InsertOneAsync(usuarioDetails);
        }

        public async Task UpdateUsuarioAsync(int userId, UsuarioDetails usuarioDetails)
        {
            await usuarioCollection.ReplaceOneAsync(x => x.UserId == userId, usuarioDetails);
        }

        public async Task DeleteUsuarioAsync(int productId)
        {
            await usuarioCollection.DeleteOneAsync(x => x.UserId == productId);
        }

        public async Task<List<int>> GetUsuariosId(string nombre, string apellido, string nombreUsuario)
        {
            nombre ??= "";

            apellido ??= "";

            nombreUsuario ??= "";

            var results =
             from usuario in usuarioCollection.AsQueryable()
             where usuario.Nombre.Contains(nombre) &&
             usuario.Apellido.Contains(apellido) &&
             usuario.NombreUsuario.Contains(nombreUsuario)

             select usuario.UserId;

            return results.ToList();
        }





    }
}
