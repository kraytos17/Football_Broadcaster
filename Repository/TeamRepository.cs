using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class TeamRepository : ITeamRepo {
    private readonly IMongoCollection<Team> _teamCollection;
    
    public TeamRepository(DatabaseService dbService, IOptions<DatabaseSettings> dbSettings)
    {
        _teamCollection = dbService.GetCollection<Team>(dbSettings.Value.TeamCollectionName);
    }
    public async Task<bool> CreateTeam(Team? team, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (team is null) { return false; }

        await _teamCollection.InsertOneAsync(team, null, ct);
        return true;
    }

    public async Task<bool> DeleteTeam(int teamId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        if (teamId <= 0 ) { return false; }

        var filter = Builders<Team>.Filter.Eq(r => r.TeamId, teamId);
        if (filter is null) { return false; }

        var res = await _teamCollection.DeleteOneAsync(filter, null, ct);
        return res.DeletedCount > 0 && res.IsAcknowledged;
    }

    public async Task<IEnumerable<Team>> GetAllTeams(CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Empty;
        if (filter is null) { return Enumerable.Empty<Team>(); }

        var res = await _teamCollection.FindAsync(filter, null, ct);
        return await res.ToListAsync(ct);
    }

    public async Task<Team> GetTeam(string name, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq( t => t.Name, name);
        if (filter is null) { return new Team();}

        return await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<Team> GetTeam(int teamId, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq(t => t.TeamId, teamId);
        if (filter is null) { return new Team (); }

        return await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> UpdateTeam(Team team, CancellationToken ct = default) {
        ct.ThrowIfCancellationRequested();

        var filter = Builders<Team>.Filter.Eq(t => t.TeamId, team.TeamId);
        if (filter is null) { return false; }

        var oldTeam = await _teamCollection.Find(filter).FirstOrDefaultAsync(ct);
        if(oldTeam is null) { return false; }

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
        return res.ModifiedCount > 0;
    }
}
