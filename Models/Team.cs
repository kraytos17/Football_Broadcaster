using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Team {
    [BsonElement("name")] [BsonRequired]
    public string Name { get; set; } = string.Empty;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = string.Empty;

    [BsonElement("team_id")]
    [BsonRequired]
    public int TeamId { get; set; } = 0;

    [BsonElement("league_position")]
    [BsonRequired]
    public int LeaguePosition { get; set; } = 0;

    [BsonElement("matches_won")]
    [BsonRequired]
    public int MatchesWon { get; set; } = 0;

    [BsonElement("matches_lost")]
    [BsonRequired]
    public int MatchesLost { get; set; } = 0;

    [BsonElement("games_played")]
    [BsonRequired]
    public int GamesPlayed { get; set; } = 0;

    [BsonElement("goal_difference")]
    [BsonRequired]
    public int GoalDifference { get; set; } = 0;
    
    [BsonElement("points")]
    [BsonRequired]
    public int Points { get; set; } = 0;
}
