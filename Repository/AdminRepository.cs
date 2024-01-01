using System.Diagnostics.CodeAnalysis;
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
   public async Task<bool> CreateAdmin(Admin admin) {
       ArgumentNullException.ThrowIfNull(nameof(Admin));

       try {
           await _adminCollection.InsertOneAsync(admin);
           return true;
       }
       catch (Exception) {
           throw;
       }
   }

   public async Task<bool> Login([NotNull]string username, [NotNull]string password) {
       var filter = Builders<Admin>.Filter.Eq(a => a.Username, username);
       try {
           var adminDetails = await _adminCollection.Find(filter).FirstOrDefaultAsync();
           if (adminDetails.Password == password) {
              return true;
           }
           return false;
       }
       catch (Exception) {
           throw;
       }
   }
}
