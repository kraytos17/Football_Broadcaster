using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface ITeamRepo {
    Task<IEnumerable<Team>> GetAllTeams(CancellationToken ct);
    Task<Team> GetTeam(string name, CancellationToken ct);
    Task<Team> GetTeam(int teamId, CancellationToken ct);
    Task<bool> CreateTeam(Team team, CancellationToken ct);
    Task<bool> UpdateTeam(Team team, CancellationToken ct);
    Task<bool> DeleteTeam(int teamId, CancellationToken ct);
}
