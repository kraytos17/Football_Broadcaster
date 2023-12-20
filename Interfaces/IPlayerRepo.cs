using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface IPlayerRepo {
    Task<Player> GetPlayerByName(string name, CancellationToken ct = default);
    Task<IEnumerable<Player>> GetPlayersByTeam(int teamId, CancellationToken ct = default);
    Task<bool> CreateNewPlayer(Player player, CancellationToken ct = default);
    Task<bool> UpdatePlayerDetails(Player newPlayerDetails, CancellationToken ct = default);
    Task<bool> DeletePlayer(string playerId, CancellationToken ct = default);
}
