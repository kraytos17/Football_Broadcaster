using FluentValidation;
using FluentValidation.Results;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Gc_Broadcasting_Api;

public static class PlayerEndpoints {
    private static readonly IPlayerRepo? _playerRepo;
    private static readonly IValidator<Player>? _playerValidator;
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };
    private static readonly IDictionary<string, string[]>? errors;

    public static void MapPlayerEndpoints(this WebApplication app) {
        app.MapGet("/players/{teamId}", GetPlayersByTeamId);
        app.MapGet("/player/{name}", GetPlayerByName);
        //app.MapPost("/player", CreateCustomer).WithValidator<Customer>();
        //app.MapPut("/player/{id}", UpdateCustomer);
        //app.MapDelete("/player/{id}", DeleteCustomerById);
    }

    public static async Task<IResult> GetPlayerByName ([NotNull] string name, CancellationToken ct = default) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) {
            return TypedResults.BadRequest("Name param cannot be empty or just whitespaces.");
        }      
        Player player = await _playerRepo!.GetPlayer(name, ct) 
            ?? throw new NullReferenceException("Player object reference is null");
        if (string.IsNullOrEmpty(player.Id) || string.IsNullOrWhiteSpace(player.Id)){
            return TypedResults.NotFound(name);
        }
        string serializedData = JsonSerializer.Serialize(player, options)
            ?? throw new NullReferenceException("Serialized data reference is null");
        return TypedResults.Ok(serializedData);
    }

    public static async Task<IResult> GetPlayersByTeamId([NotNull]int teamId, CancellationToken ct = default) {
        if (teamId <= 0) {
            return TypedResults.BadRequest("Team Id cannot be zero or negative.");
        }
        IEnumerable<Player> players = await _playerRepo!.GetPlayers(teamId, ct)
            ?? throw new NullReferenceException("Players object reference is null.");
        string serializedData = JsonSerializer.Serialize(players, options)
            ?? throw new NullReferenceException("Serialized data reference is null.");
        return TypedResults.Ok(serializedData);
    }

    public static async Task<IResult> CreatePlayer([NotNull] Player player, CancellationToken ct = default) {
        ValidationResult isValidPlayer = await _playerValidator!.ValidateAsync(player, ct);
        if(!isValidPlayer.IsValid) {
            return TypedResults.ValidationProblem(errors!);
        }
        bool created = await _playerRepo!.CreatePlayer(player, ct);
        if(!created) {
            return TypedResults.StatusCode(500);
        }
        return TypedResults.Created();
    }
}
