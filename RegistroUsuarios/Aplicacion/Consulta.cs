using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RegistroUsuarios.Modelo;
using RegistroUsuarios.Persistencia;

namespace RegistroUsuarios.Aplicacion
{
    public class Consulta
    {
        public class ListaUsuarios : IRequest<List<Usuarios>> { }

        public class Manejador : IRequestHandler<ListaUsuarios, List<Usuarios>>
        {
            private readonly DbContextoClass _contexto;
            

            public Manejador(DbContextoClass contexto)
            {
                _contexto = contexto;
                
            }

            public async Task<List<Usuarios>> Handle(ListaUsuarios request, CancellationToken cancellationToken)
            {
                var usuarios = await _contexto.Usuarios.ToListAsync();
                return usuarios;
            }
        }
    }
}
