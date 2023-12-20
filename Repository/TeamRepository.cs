using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using MongoDB.Driver;

namespace Gc_Broadcasting_Api.Repository;

public sealed class TeamRepository(Database database) : ITeamRepo {
    private readonly Database _database = database;

    public Task<bool> CreateTeam(Team team) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTeam(int TeamId) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Team>> GetAllTeams() {
        throw new NotImplementedException();
    }

    public Task<Team> GetTeam(string name) {
        throw new NotImplementedException();
    }

    public Task<Team> GetTeam(int TeamId) {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTeam(Team team) {
        throw new NotImplementedException();
    }
}
