using RegistroUsuarios.Persistencia;
using RegistroUsuarios.RabitMQ;
using RegistroUsuarios.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUsuariosServices, UsuariosServices>();
builder.Services.AddDbContext<DbContextoClass>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
