using Gc_Broadcasting_Api.Models;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api;

public class Database {
    private readonly IConfiguration _configuration;
    public readonly IMongoCollection<Player> Players;
    public readonly IMongoCollection<Team> Teams;
    public Database(IConfiguration configuration, IMongoClient client) {
        _configuration = configuration;
        var db = client.GetDatabase(_configuration.GetConnectionString("MongoDB_URI"));
        Players = db.GetCollection<Player>(_configuration["PlayerCollectionName"]);
        Teams = db.GetCollection<Team>(_configuration["TeamCollectionName"]);
    }
}
