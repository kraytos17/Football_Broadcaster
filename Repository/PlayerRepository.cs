using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class PlayerRepository : IPlayerRepo {
    private readonly IMongoCollection<Player> _playerCollection;
    
    public PlayerRepository(IOptions<DatabaseSettings> dbSettings) {
        var dbService = new DatabaseService(dbSettings);
        _playerCollection = dbService.GetCollection<Player>(dbSettings.Value.PlayerCollectionName);
    }

    public async Task<bool> CreatePlayer(Player player, CancellationToken ct) {
        await _playerCollection.InsertOneAsync(player, null, ct);
        return true;
    }

    public async Task<bool> DeletePlayer(string playerId, CancellationToken ct) {
        var filter = Builders<Player>.Filter.Eq(r => r.Id, playerId);
        if (filter is null) { return false; }

        var res = await _playerCollection.DeleteOneAsync(filter, null, ct);
        return res.DeletedCount > 0 && res.IsAcknowledged;
    }

    public async Task<Player> GetPlayer(string playerId, CancellationToken ct) {
        var filter = Builders<Player>.Filter.Eq(x => x.Id, playerId);
        if (filter is null) { return new Player(); }

        return await _playerCollection.Find(filter).FirstOrDefaultAsync(ct);
    }

    // public async Task<IEnumerable<Player>> GetPlayers(int teamId, CancellationToken ct) {
    //     if(teamId <= 0) { return Enumerable.Empty<Player>(); }
    //
    //     var filter = Builders<Player>.Filter.Eq(x => x.TeamId, teamId);
    //     if (filter is null) { return Enumerable.Empty<Player>(); } 
    //     var res = await _playerCollection.FindAsync(filter, null, ct);
    //     return await res.ToListAsync(ct);
    // }

    public async Task<bool> UpdatePlayer(Player newPlayerDetails, CancellationToken ct) {
        ArgumentNullException.ThrowIfNull(newPlayerDetails);

        var filter = Builders<Player>.Filter.Eq(p => p.Id, newPlayerDetails.Id);
        if (filter is null) { return false; }

        var oldPlayerDetails = await _playerCollection.Find(filter).FirstOrDefaultAsync(ct);
        if (oldPlayerDetails is null) { return false; }

        var updateCondition = Builders<Player>.Update
            .Set(u => u.FirstName, newPlayerDetails.FirstName)
            .Set(u => u.LastName, newPlayerDetails.LastName)
            .Set(u => u.Year, newPlayerDetails.Year)
            .Set(u => u.Branch, newPlayerDetails.Branch)
            .Set(u => u.Socials, newPlayerDetails.Socials)
            .Set(u => u.Email, newPlayerDetails.Email)
            .Set(u => u.Password, newPlayerDetails.Password)
            .Set(u => u.ContactNo, newPlayerDetails.ContactNo);

        var update = await _playerCollection.UpdateOneAsync(p => p.Id == newPlayerDetails.Id, updateCondition,
            null, ct);
        return update.ModifiedCount > 0;
    }

    // public async Task<Player> UpdatePlayerStats(int goals, int assists, string name, CancellationToken ct = default) {
    //     if (goals <= 0 && assists <= 0) {
    //         throw new ArgumentException("Goals and assists must be greater than zero.");
    //     }
    //     var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
    //
    //     var playerStats = await _playerCollection.Find(filter).FirstOrDefaultAsync(ct)
    //         ?? throw new NullReferenceException("PlayerStats object has null reference.");
    //     playerStats.Assists += assists;
    //     playerStats.Goals += goals;
    //     return playerStats;
    // }
}
