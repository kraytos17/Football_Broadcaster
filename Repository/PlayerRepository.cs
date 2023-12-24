using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class PlayerRepository(Database database) : IPlayerRepo {
    private readonly Database _database = database;

    public async Task<bool> CreatePlayer(Player player, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (player is null) { return false; }

        try {
            await _database.Players.InsertOneAsync(player, null, ct);
            return true;
        }
        catch (Exception){
            throw;
        } 
    }

    public async Task<bool> DeletePlayer(string playerId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (playerId is null) { return false; }

        var filter = Builders<Player>.Filter.Eq(r => r.Id, playerId);
        if (filter is null) { return false; }

        try {
            await _database.Players.DeleteOneAsync(filter, null, ct);
            return true;
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<Player> GetPlayer(string name, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        ArgumentNullException.ThrowIfNull(name);

        var filter = Builders<Player>.Filter.Eq(x => x.Name!, name);
        if (filter is null) { return new Player { }; }

        try {
            return await _database.Players.Find(filter).FirstOrDefaultAsync(ct);
        }
        catch (Exception){
            throw;
        }
    }

    public async Task<IEnumerable<Player>> GetPlayers(int teamId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if(teamId <= 0) { return Enumerable.Empty<Player>(); }

        var filter = Builders<Player>.Filter.Eq(x => x.TeamId, teamId);
        if (filter is null) { return Enumerable.Empty<Player>(); } 

        try {
            var res = await _database.Players.FindAsync(filter, null, ct);
            return await res.ToListAsync(ct);
        }
        catch (Exception){ return Enumerable.Empty<Player>(); }
    }

    public async Task<bool> UpdatePlayer(Player newPlayerDetails, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        ArgumentNullException.ThrowIfNull(newPlayerDetails);

        var filter = Builders<Player>.Filter.Eq(p => p.Id, newPlayerDetails.Id);
        if (filter is null) { return false; }

        var oldPlayerDetails = await _database.Players.Find(filter).FirstOrDefaultAsync(ct);
        if (oldPlayerDetails is null) { return false; }

        try {
            var updateCondition = Builders<Player>.Update
                .Set(u => u.Name, newPlayerDetails.Name)
                .Set(u => u.Id, newPlayerDetails.Id)
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

            var update = await _database.Players.UpdateOneAsync (p => p.Id == newPlayerDetails.Id, updateCondition, null, ct);

            return true;
        }
        catch (Exception) {
            throw;
        }
    }
}