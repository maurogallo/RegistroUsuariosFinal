using MediatR;
using Microsoft.EntityFrameworkCore;
using RegistroUsuarios.Modelo;
using RegistroUsuarios.Persistencia;

namespace RegistroUsuarios.Aplicacion
{
    public class ConsultaFiltro
    {
        public class UsuarioId : IRequest<Usuarios> {
           
            public string UsuarioPorId { get; set; }
        
        }

        public class Manejador : IRequestHandler<UsuarioId, Usuarios>
        {
            private readonly DbContextoClass _contexto;


            public Manejador(DbContextoClass contexto)
            {
                _contexto = contexto;

            }

            public async Task<Usuarios> Handle(UsuarioId request, CancellationToken cancellationToken)
            {
                var usuarios = await _contexto.Usuarios.Where(x => x.UserId.ToString() == request.UsuarioPorId).FirstOrDefaultAsync();
                if (usuarios == null)
                {
                    throw new Exception("No se encontro el usuario por id");
                }
                return usuarios;
            }
        }
    }
}
