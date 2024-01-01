using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class TeamRepository : ITeamRepo {
    private readonly IMongoCollection<Team> _teamCollection;

    public TeamRepository(IOptions<DatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _teamCollection = mongoDb.GetCollection<Team>(dbSettings.Value.TeamCollectionName);
    }
    public async Task<bool> CreateTeam(Team team, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (team is null) { return false; }

        try {
            await _teamCollection.InsertOneAsync(team, null, ct);
            return true;
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<bool> DeleteTeam(int TeamId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (TeamId <= 0 ) { return false; }

        var filter = Builders<Team>.Filter.Eq(r => r.TeamId, TeamId);
        if (filter is null) { return false; }

        try {
            var res = await _teamCollection.DeleteOneAsync(filter, null, ct);
            if (res.DeletedCount > 0 && res.IsAcknowledged) { return true; }
            return false;
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<IEnumerable<Team>> GetAllTeams(CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Empty;
        if (filter is null) { return Enumerable.Empty<Team>(); }

        try {
            var res = await _teamCollection.FindAsync(filter, null, ct);
            return await res.ToListAsync(ct);
        }
        catch (Exception) { return Enumerable.Empty<Team>(); }
    }

    public async Task<Team> GetTeam(string name, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq( t => t.Name, name);
        if (filter is null) { return new Team { }; }

        try {
             return await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<Team> GetTeam(int TeamId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq(t => t.TeamId, TeamId);
        if (filter is null) { return new Team { }; }

        try {
            return await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task<bool> UpdateTeam(Team team, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq(t => t.TeamId, team.TeamId);
        if (filter is null) { return false; }

        var oldTeam = await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
        if(oldTeam is null) { return false; }

        try {
            var updateDef = Builders<Team>.Update
                .Set(u => u.Name, team.Name)
                .Set(u => u.LeaguePosition, team.LeaguePosition)
                .Set(u => u.MatchesLost, team.MatchesLost)
                .Set(u => u.GoalDifference, team.GoalDifference)
                .Set(u => u.GamesPlayed, team.GamesPlayed)
                .Set(u => u.Points, team.Points)
                .Set(u => u.MatchesWon, team.MatchesWon)
                .Set(u => u.TeamId, team.TeamId);

            var res = await _teamCollection.UpdateOneAsync(filter, updateDef, null, ct);
            if (res.ModifiedCount > 0) { return true; }
            return false;
        }
        catch (Exception) {
            throw;
        }
    }
}
