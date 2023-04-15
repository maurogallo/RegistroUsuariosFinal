using RegistroUsuarios.Modelo;
using RegistroUsuarios.Persistencia;

namespace RegistroUsuarios.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly DbContextoClass _dbContext;

         
        public UsuariosServices(DbContextoClass dbContext)
        {
            _dbContext = dbContext;
        }

        public Usuarios AddUsuarios(Usuarios usuarios)
        {
            var result = _dbContext.Usuarios.Add(usuarios);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteUsuarios(int Id)
        {
            var filteredData = _dbContext.Usuarios.Where(x => x.UserId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }
                
        public Usuarios GetUsuarioById(int id)
        {
            return _dbContext.Usuarios.Where(x => x.UserId == id).FirstOrDefault();
        }

        public IEnumerable<Usuarios> GetUsuariosList()
        {
            return _dbContext.Usuarios.ToList();
        }
              

        public List<Usuarios> GetUsuariosById(List<int> ids)
        {
            return _dbContext.Usuarios.Where(x=> x.UserId == ids.FirstOrDefault()).ToList();
        }

        public Usuarios UpdateUsuarios(Usuarios usuarios)
        {
            var result = _dbContext.Usuarios.Update(usuarios);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public Usuarios GetUsuarioByIdMongo(int id)
        {
            return (Usuarios)_dbContext.Usuarios.Where(x => x.UserId == id);
        }
    }
}
