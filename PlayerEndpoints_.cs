//using Gc_Broadcasting_Api.Interfaces;
//using Gc_Broadcasting_Api.Models;
//using Gc_Broadcasting_Api.Repository;
//using Gc_Broadcasting_Api.Validator;
//using MongoDB.Driver;
//using System.Diagnostics.CodeAnalysis;
//using System.Text.Json;

//namespace Gc_Broadcasting_Api;

//public static class PlayerEndpoints_ {
//    private static readonly IPlayerRepo playerRepo;
//    private static readonly IConfiguration configuration;
//    private static readonly MongoClient mongoClient = new();
//    private static readonly Database _db = new(configuration, mongoClient);
//    private static readonly PlayerRepository _playerRepo = new(_db);
//    private static readonly PlayerValidator _playerValidator = new(playerRepo, configuration);
//    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

//    public static void MapPlayerEndpoints(this WebApplication app) {
        
//    }

//    public static async Task<IResult> GetPlayerByName([NotNull]string name, CancellationToken ct = default) {
//        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) {
//            return TypedResults.BadRequest("Name param cannot be empty or just whitespaces.");
//        }
//        Player player = await _playerRepo.GetPlayer(name, ct) 
//            ?? throw new NullReferenceException("Player object reference is null");
//        if (string.IsNullOrEmpty(player.Id) || string.IsNullOrWhiteSpace(player.Id)){
//            return TypedResults.NotFound(name);
//        }
//        string serializedData = JsonSerializer.Serialize(player, options)
//            ?? throw new NullReferenceException("Serialized data reference is null");
//        return TypedResults.Ok(serializedData);
//    }

//    public static async Task<IResult> GetPlayersByTeamId([NotNull]int teamId, CancellationToken ct = default) {
//        if (teamId <= 0) {
//            return TypedResults.BadRequest("Team Id cannot be zero or negative.");
//        }
//        IEnumerable<Player> players = await _playerRepo.GetPlayers(teamId, ct)
//            ?? throw new NullReferenceException("Players object reference is null.");
//        string serializedData = JsonSerializer.Serialize(players, options)
//            ?? throw new NullReferenceException("Serialized data reference is null.");
//        return TypedResults.Ok(serializedData);
//    }

//    public static async Task<IResult> CreatePlayer([NotNull]Player player, CancellationToken ct = default) {
//        var res = await _playerValidator.ValidateAsync(player, ct);
//        if (!res.IsValid) {
//            return TypedResults.BadRequest("Validation error, invalid player data.");
//        }
//        bool created = await _playerRepo.CreatePlayer(player, ct);
//        if(!created) {
//            return TypedResults.StatusCode(500);
//        }
//        return TypedResults.Created();
//    }

//    public static async Task<IResult> UpdatePlayer([NotNull]Player player, CancellationToken ct = default) {
//        var res = await _playerValidator.ValidateAsync(player, ct);
//        if (!res.IsValid) {
//            return TypedResults.BadRequest("Validation error, invalid player data.");
//        }
//        bool updated = await _playerRepo.UpdatePlayer(player, ct);
//        if(!updated) {
//            return TypedResults.StatusCode(500);
//        }
//        return TypedResults.Ok();
//    }

//    public static async Task<IResult> DeletePlayer([NotNull]string playerId, CancellationToken ct = default) {
//        if (string.IsNullOrEmpty(playerId) || string.IsNullOrWhiteSpace(playerId)) {
//            return TypedResults.BadRequest("PlayerId cannot be empty or just whitespaces.");
//        }
//        bool deleted = await _playerRepo.DeletePlayer(playerId, ct);
//        if (!deleted) {
//            return TypedResults.StatusCode(500);
//        }
//        return TypedResults.Ok();
//    }
//}
