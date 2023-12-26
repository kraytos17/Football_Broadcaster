using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Team {
    [BsonElement("name")]
    [BsonRequired]
    public string? Name { get; set; }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [NotNull]
    public string? Id { get; set; }
    [BsonElement("team_id")]
    [BsonRequired]
    public int TeamId { get; set; }
    [BsonElement("league_position")]
    [BsonRequired]
    public int? LeaguePosition { get; set; }
    [BsonElement("matches_won")]
    [BsonRequired]
    public int MatchesWon { get; set; }
    [BsonElement("matches_lost")]
    [BsonRequired]
    public int MatchesLost { get; set; }
    [BsonElement("games_played")]
    [BsonRequired]
    public int GamesPlayed { get; set; }
    [BsonElement("goal_difference")]
    [BsonRequired]
    public int GoalDifference { get; set; }
    [BsonElement("points")]
    [BsonRequired]
    public int Points { get; set; }
}
