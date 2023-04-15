using AutoMapper.Configuration.Conventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegistroUsuarios.Aplicacion;
using RegistroUsuarios.Modelo;
using RegistroUsuarios.RabitMQ;
using RegistroUsuarios.Services;

namespace RegistroUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices usuariosServices;
        private readonly IRabitMQProducer _rabitMQProducer;

        public UsuariosController(IUsuariosServices _usuariosServices, IRabitMQProducer rabitMQProducer)
        {
            usuariosServices = _usuariosServices;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpGet("usuarioslist")]
        public IEnumerable<Usuarios> UsuariosList()
        {
            var usuariosList=usuariosServices.GetUsuariosList();
            return usuariosList;

        }
        [HttpGet("getusuariosbyid")]
        public Usuarios GetUsuariosById(int Id)
        {
            return usuariosServices.GetUsuarioById(Id);
        }

        [HttpGet("GetUsuariosByidMongo")]
        public async Task<string> GetUsuariosByidMongo(string nombre, string apellido, string nombreUsuario)
        {
            string apiURL = "https://localhost:7017/api/Usuario";
            UriBuilder builder = new UriBuilder(apiURL);
            builder.Query = "nombre=" + nombre + "&apellido=" + apellido + "&nombreUsuario=" + nombreUsuario + "";

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
                if (response.IsSuccessStatusCode)
                {

                   var  respuesta =  response.Content.ReadAsStringAsync();

                    var json = JsonConvert.SerializeObject(respuesta);

                    return json;       
                    
                    //foreach(var r in json.Replace("[","").Split(","))
                    //{
                    //    var campo = r.Replace("\"", "");
                    //  var usuarios =  GetUsuariosById( Int32.Parse(campo));
                    //}

                }
            }
            return "No exiten usuarios con esos datos";
        }

        [HttpPost("addusuarios")]
        public Usuarios AddUsuarios(Usuarios usuarios)
        {
            var usuariosData = usuariosServices.AddUsuarios(usuarios);

            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabitMQProducer.SendUsuariosMessage(usuariosData);

            return usuariosData;
        }

        [HttpPut("updateusuarios")]
        public Usuarios UpdateUsuarios(Usuarios usuarios)
        {
            return usuariosServices.UpdateUsuarios(usuarios);
        }

        [HttpDelete("deleteusuarios")]
        public bool DeleteUsuarios(int Id)
        {
            return usuariosServices.DeleteUsuarios(Id);
        }
    }
}
