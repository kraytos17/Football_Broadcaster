using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface IPlayerRepo {
    Task<Player> GetPlayer(string name, CancellationToken ct = default);
    Task<IEnumerable<Player>> GetPlayers(int teamId, CancellationToken ct = default);
    Task<bool> CreatePlayer(Player player, CancellationToken ct = default);
    Task<bool> UpdatePlayer(Player newPlayerDetails, CancellationToken ct = default);
    Task<bool> DeletePlayer(string playerId, CancellationToken ct = default);
}
