using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface IPlayerRepo {
    Task<Player> GetPlayer(string name, CancellationToken ct);
    Task<IEnumerable<Player>> GetPlayers(int teamId, CancellationToken ct);
    Task<bool> CreatePlayer(Player player, CancellationToken ct);
    Task<bool> UpdatePlayer(Player newPlayerDetails, CancellationToken ct);
    //Task<Player> UpdatePlayerStats(int goals, int assists, string name, CancellationToken ct);
    Task<bool> DeletePlayer(string playerId, CancellationToken ct);
}
