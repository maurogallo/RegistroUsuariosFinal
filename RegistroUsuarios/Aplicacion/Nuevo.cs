using MediatR;
using RegistroUsuarios.Modelo;
using RegistroUsuarios.Persistencia;

namespace RegistroUsuarios.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NombreUsuario  { get; set; }
            
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly DbContextoClass _contexto;

            public Manejador(DbContextoClass contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarios = new Usuarios
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    NombreUsuario = request.NombreUsuario,
                    

                };

                _contexto.Usuarios.Add(usuarios);
                               
                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el usuario");
            }
        }
    }
}