//using Gc_Broadcasting_Api.Interfaces;
//using Gc_Broadcasting_Api.Models;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;

//namespace Gc_Broadcasting_Api.Repository;

//public class AdminRepository : IAdminRepo {
//    private readonly IMongoCollection<Admin> _adminCollection;
//    public AdminRepository(IOptions<DatabaseSettings> dbSettings) {
//        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
//        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
//        _adminCollection = mongoDb.GetCollection<Admin>(dbSettings.Value.AdminCollectionName);
//    }
//    public async Task<Admin> CreateAdmin(Admin admin) {
//        ArgumentNullException.ThrowIfNull(admin);
//        bool created = false;
//        try {
//            await _adminCollection.InsertOneAsync(admin);


//        }
//        catch (Exception) {
//            throw;
//        }
//    }

//    public Task<bool> Login(string username, string password) {
//        throw new NotImplementedException();
//    }
//}
