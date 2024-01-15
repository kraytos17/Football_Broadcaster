using System.Security.Cryptography;
using System.Text;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public class AdminRepository : IAdminRepo {
   private readonly IMongoCollection<Admin> _adminCollection;
   public AdminRepository(IOptions<DatabaseSettings> dbSettings) {
       var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
       var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
       _adminCollection = mongoDb.GetCollection<Admin>(dbSettings.Value.AdminCollectionName);
   }
   public async Task<bool> CreateAdmin(Admin? admin) {
       ArgumentNullException.ThrowIfNull(nameof(Admin));

       if (admin != null) await _adminCollection.InsertOneAsync(admin);
       return true;
   }

   public async Task<bool> Login(string username, string? password) {
       ArgumentNullException.ThrowIfNull(password);
       var filter = Builders<Admin>.Filter.Eq(a => a.Username, username);
       var adminDetails = await _adminCollection.Find(filter).FirstOrDefaultAsync();
       return adminDetails != null && HashPassword(adminDetails.Password) == HashPassword(password);
   }

   private static string HashPassword(string? password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return BitConverter.ToString(hash);
    }
}
