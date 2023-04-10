namespace RegistroUsuarios.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendUsuariosMessage<T>(T message);
    }
}
