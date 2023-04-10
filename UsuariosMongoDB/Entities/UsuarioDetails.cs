using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UsuariosMongoDB.Entities
{
    public class UsuarioDetails
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
    }
}
