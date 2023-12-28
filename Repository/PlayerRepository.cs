using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class PlayerRepository : IPlayerRepo {
    private readonly IMongoCollection<Player> _playerCollection;
    public PlayerRepository(IOptions<DatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _playerCollection = mongoDb.GetCollection<Player>(dbSettings.Value.PlayerCollectionName);
    }

    public async Task<bool> CreatePlayer(Player player, CancellationToken ct) {
        if (player is null) { return false; }

        try {
            await _playerCollection.InsertOneAsync(player, null, ct);
            return true;
        }
        catch (Exception) {
            throw;
        } 
    }

    public async Task<bool> DeletePlayer(string playerId, CancellationToken ct) {
        if (playerId is null) { return false; }

        var filter = Builders<Player>.Filter.Eq(r => r.Id, playerId);
        if (filter is null) { return false; }

        try {
            var res = await _playerCollection.DeleteOneAsync(filter, null, ct);
            if (res.DeletedCount > 0 && res.IsAcknowledged) return true;
            return false;
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<Player> GetPlayer(string name, CancellationToken ct) {
        ArgumentNullException.ThrowIfNull(name);

        var filter = Builders<Player>.Filter.Eq(x => x.Name, name);
        if (filter is null) { return new Player { }; }

        try {
            return await _playerCollection.Find(filter).FirstOrDefaultAsync(ct);
        }
        catch (Exception){
            throw;
        }
    }

    public async Task<IEnumerable<Player>> GetPlayers(int teamId, CancellationToken ct) {
        if(teamId <= 0) { return Enumerable.Empty<Player>(); }

        var filter = Builders<Player>.Filter.Eq(x => x.TeamId, teamId);
        if (filter is null) { return Enumerable.Empty<Player>(); } 

        try {
            var res = await _playerCollection.FindAsync(filter, null, ct);
            return await res.ToListAsync(ct);
        }
        catch (Exception){ return Enumerable.Empty<Player>(); }
    }

    public async Task<bool> UpdatePlayer(Player newPlayerDetails, CancellationToken ct) {
        ArgumentNullException.ThrowIfNull(newPlayerDetails);

        var filter = Builders<Player>.Filter.Eq(p => p.CollegeId, newPlayerDetails.CollegeId);
        if (filter is null) { return false; }

        var oldPlayerDetails = await _playerCollection.Find(filter).FirstOrDefaultAsync(ct);
        if (oldPlayerDetails is null) { return false; }

        try {
            var updateCondition = Builders<Player>.Update
                .Set(u => u.Name, newPlayerDetails.Name)
                .Set(u => u.Position, newPlayerDetails.Position)
                .Set(u => u.Assists, newPlayerDetails.Assists)
                .Set(u => u.Year, newPlayerDetails.Year)
                .Set(u => u.Branch, newPlayerDetails.Branch)
                .Set(u => u.CollegeId, newPlayerDetails.CollegeId)
                .Set(u => u.Goals, newPlayerDetails.Goals)
                .Set(u => u.Imagelink, newPlayerDetails.Imagelink)
                .Set(u => u.Instagram, newPlayerDetails.Instagram)
                .Set(u => u.Age, newPlayerDetails.Age)
                .Set(u => u.TeamId, newPlayerDetails.TeamId);

            var update = await _playerCollection.UpdateOneAsync(p => p.CollegeId == newPlayerDetails.CollegeId, updateCondition, null, ct);
            if (update.ModifiedCount > 0) return true;
            return false;
        }
        catch (Exception) {
            throw;
        }
    }

    //public async Task<Player> UpdatePlayerStats(int goals, int assists, string name, CancellationToken ct = default) {
    //    bool updated = false;
    //    if (goals <= 0 && assists <=0) {
    //        updated = false;
    //    }

    //    var filter = Builders<Player>.Filter.Eq(p => p.Name, name);

    //    var playerStats = await _playerCollection.Find(filter).FirstOrDefaultAsync(ct)
    //        ?? throw new NullReferenceException("PlayerStats object has null reference.");
    //    playerStats.Assists += assists;
    //    playerStats.Goals += goals;
    //    updated = true;
    //    if (!updated) {
    //        return new Player { };
    //    }
    //    return playerStats;
    //}
}