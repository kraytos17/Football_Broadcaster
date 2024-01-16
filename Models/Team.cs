using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Team {
    [BsonElement("name")]
    [BsonRequired]
    public string? Name { get; }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; }
    [BsonElement("team_id")]
    [BsonRequired]
    public int TeamId { get; }
    [BsonElement("league_position")]
    [BsonRequired]
    public int? LeaguePosition { get; }
    [BsonElement("matches_won")]
    [BsonRequired]
    public int MatchesWon { get; }
    [BsonElement("matches_lost")]
    [BsonRequired]
    public int MatchesLost { get; }
    [BsonElement("games_played")]
    [BsonRequired]
    public int GamesPlayed { get; }
    [BsonElement("goal_difference")]
    [BsonRequired]
    public int GoalDifference { get; }
    [BsonElement("points")]
    [BsonRequired]
    public int Points { get; }
}
