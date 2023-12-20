using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface ITeamRepo {
    Task<IEnumerable<Team>> GetAllTeams();
    Task<Team> GetTeam(string name);
    Task<Team> GetTeam(int TeamId);
    Task<bool> CreateTeam(Team team);
    Task<bool> UpdateTeam(Team team);
    Task<bool> DeleteTeam(int TeamId);
}
