var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//using Gc_Broadcasting_Api.Models;
//using MongoDB.Driver;

//string connectionString = "mongodb://localhost:27017";
//string dbName = "Gc_Broadcasting_Football";
//string collectionName = "Player";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(dbName);
//var collection = db.GetCollection<Player>(collectionName);

//var player = new Player {
//    Name = "Soumil",
//    TeamId = 1
//};

//await collection.InsertOneAsync(player);

//var result = await collection.FindAsync(_ => true);
//foreach(var res in result.ToList()) {
//    Console.WriteLine(res.Name);
//    Console.WriteLine(res.Id);
//    Console.WriteLine(res.TeamId);
//}
