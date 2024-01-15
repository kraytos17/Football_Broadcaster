using FluentValidation;
using FluentValidation.Results;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Endpoints;

public static class PlayerEndpoints
{
    public static void MapPlayerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/player");
        group.MapGet("{name}", GetPlayerByName);
        group.MapPost("/", CreatePlayer);
        group.MapPut("/", UpdatePlayer);
        //group.MapPatch("{name}", UpdateStats);
        group.MapDelete("{playerId}", DeletePlayer);
        app.MapGet("api/players/{teamId:int}", GetPlayersByTeamId);
    }

    public static async Task<IResult> CreatePlayer(Player player, IPlayerRepo playerRepo, IValidator<Player> playerRequestValidator, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var res = await playerRequestValidator.ValidateAsync(player, ct);
        if (!res.IsValid)
        {
            return TypedResults.BadRequest("Validation error, invalid player data.");
        }
        var created = await playerRepo.CreatePlayer(player, ct);
        if (!created)
        {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Created();
    }

    public static async Task<IResult> GetPlayerByName(string name, IPlayerRepo playerRepo, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return TypedResults.BadRequest("Name param cannot be empty or just whitespaces.");
        }
        var player = await playerRepo.GetPlayer(name, ct)
                     ?? throw new NullReferenceException("Player object reference is null");
        if (string.IsNullOrEmpty(player.Id) || string.IsNullOrWhiteSpace(player.Id))
        {
            return TypedResults.NotFound(name);
        }
        return TypedResults.Ok(player);
    }

    public static async Task<IResult> GetPlayersByTeamId(int teamId, IPlayerRepo playerRepo, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (teamId <= 0)
        {
            return TypedResults.BadRequest("Team Id cannot be zero or negative.");
        }
        IEnumerable<Player> players = await playerRepo.GetPlayers(teamId, ct)
            ?? throw new NullReferenceException("Players object reference is null.");
        return TypedResults.Ok(players);
    }

    public static async Task<IResult> UpdatePlayer(Player player, IPlayerRepo playerRepo, IValidator<Player> playerRequestValidator, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var res = await playerRequestValidator.ValidateAsync(player, ct);
        if (!res.IsValid)
        {
            return TypedResults.BadRequest("Validation error, invalid player data.");
        }
        var updated = await playerRepo.UpdatePlayer(player, ct);
        if (!updated)
        {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Ok(player);
    }

    public static async Task<IResult> DeletePlayer(string playerId, IPlayerRepo playerRepo, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (string.IsNullOrEmpty(playerId) || string.IsNullOrWhiteSpace(playerId))
        {
            return TypedResults.BadRequest("PlayerId cannot be empty or just whitespaces.");
        }
        var deleted = await playerRepo.DeletePlayer(playerId, ct);
        if (!deleted)
        {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Ok();
    }
}
