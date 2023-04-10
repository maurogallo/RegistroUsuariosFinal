using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();

//Here we create channel with session and model
using var channel = connection.CreateModel();

//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("usuarios", exclusive: false);

//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Usuarios message received: {message}");

    var connectionString = "mongodb://localhost:27017"; //example: ... = "mongodb://localhost";

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase("usuarios");  //example: ...GetDatabase("test"); 

    //string text = System.IO.File.ReadAllText(@"<File Directory with FileName.json>"); //example: ...ReadAllText(@"MyMovies.json");

    var document = BsonSerializer.Deserialize<BsonDocument>(message);
    var collection = database.GetCollection<BsonDocument>("usuariosregistrados"); // example: ...<BsonDocument>("Movies");
    collection.InsertOneAsync(document);

};

//read the message
channel.BasicConsume(queue: "usuarios", autoAck: true, consumer: consumer);

Console.ReadKey();

