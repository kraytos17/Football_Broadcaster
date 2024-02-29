using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api;

public sealed class DatabaseService {
    private readonly IMongoDatabase _database;

    public DatabaseService(IOptions<DatabaseSettings> dbSettings) {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        _database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) {
        return _database.GetCollection<T>(collectionName);
    }
}
