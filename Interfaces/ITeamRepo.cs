using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface ITeamRepo {
    Task<IEnumerable<Team>> GetAllTeams(CancellationToken ct = default);
    Task<Team> GetTeam(string name, CancellationToken ct = default);
    Task<Team> GetTeam(int teamId, CancellationToken ct = default);
    Task<bool> CreateTeam(Team team, CancellationToken ct = default);
    Task<bool> UpdateTeam(Team team, CancellationToken ct = default);
    Task<bool> DeleteTeam(int teamId, CancellationToken ct = default);
}
