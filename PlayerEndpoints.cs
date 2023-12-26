using FluentValidation;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Gc_Broadcasting_Api;

public static class PlayerEndpoints {
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };
    public static void MapPlayerEndpoints(this IEndpointRouteBuilder app) {
        var group = app.MapGroup("api/players");
        group.MapGet("{teamId}", GetPlayersByTeamId);
        group.MapGet("{name}", GetPlayerByName);
        group.MapPost("/", CreatePlayer);
        group.MapPut("/", UpdatePlayer);
        group.MapDelete("{playerId}", DeletePlayer);
    }

    public static async Task<IResult> CreatePlayer(Player player, IPlayerRepo playerRepo, IValidator<Player> playerValidator, CancellationToken ct = default) {
        var res = await playerValidator.ValidateAsync(player, ct);
        if (!res.IsValid) {
            return TypedResults.BadRequest("Validation error, invalid player data.");
        }
        bool created = await playerRepo.CreatePlayer(player, ct);
        if (!created) {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Created();
    }

    public static async Task<IResult> GetPlayerByName(string name, IPlayerRepo playerRepo, CancellationToken ct = default) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) {
            return TypedResults.BadRequest("Name param cannot be empty or just whitespaces.");
        }
        Player player = await playerRepo.GetPlayer(name, ct)
            ?? throw new NullReferenceException("Player object reference is null");
        if (string.IsNullOrEmpty(player.Id) || string.IsNullOrWhiteSpace(player.Id)) {
            return TypedResults.NotFound(name);
        }
        string serializedData = JsonSerializer.Serialize(player, options)
            ?? throw new NullReferenceException("Serialized data reference is null");
        return TypedResults.Ok(serializedData);
    }

    public static async Task<IResult> GetPlayersByTeamId([NotNull]int teamId, IPlayerRepo playerRepo, CancellationToken ct = default) {
        if (teamId <= 0) {
            return TypedResults.BadRequest("Team Id cannot be zero or negative.");
        }
        IEnumerable<Player> players = await playerRepo.GetPlayers(teamId, ct)
            ?? throw new NullReferenceException("Players object reference is null.");
        string serializedData = JsonSerializer.Serialize(players, options)
            ?? throw new NullReferenceException("Serialized data reference is null.");
        return TypedResults.Ok(serializedData);
    }

    public static async Task<IResult> UpdatePlayer([NotNull]Player player, IPlayerRepo playerRepo, IValidator<Player> playerValidator, CancellationToken ct = default) {
        var res = await playerValidator.ValidateAsync(player, ct);
        if (!res.IsValid) {
            return TypedResults.BadRequest("Validation error, invalid player data.");
        }
        bool updated = await playerRepo.UpdatePlayer(player, ct);
        if (!updated) {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Ok();
    }

    public static async Task<IResult> DeletePlayer([NotNull]string playerId, IPlayerRepo playerRepo, CancellationToken ct = default) {
        if (string.IsNullOrEmpty(playerId) || string.IsNullOrWhiteSpace(playerId)) {
            return TypedResults.BadRequest("PlayerId cannot be empty or just whitespaces.");
        }
        bool deleted = await playerRepo.DeletePlayer(playerId, ct);
        if (!deleted) {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Ok();
    }
}
