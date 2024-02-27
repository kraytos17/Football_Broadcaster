using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Player
{
    [BsonElement("name")]
    [BsonRequired]
    public string Name { get; private set; } = string.Empty;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = string.Empty;

    [BsonElement("image_link")]
    [BsonRequired]
    public string ImageLink { get; private set; } = string.Empty;

    [BsonElement("position")]
    [BsonRequired]
    public string Position { get; private set; } = string.Empty;

    [BsonElement("branch")]
    [BsonRequired]
    public string Branch { get; private set; } = string.Empty;

    [BsonElement("age")]
    [BsonRequired]
    public int Age { get; private set; } = 0;

    [BsonElement("year")]
    [BsonRequired]
    public int Year { get; private set; } = 0;

    [BsonElement("instagram")]
    public string Instagram { get; private set; } = string.Empty;

    [BsonElement("goals")] 
    public int Goals { get; set; }
    [BsonElement("assists")]
    public int Assists { get; set; }

    [BsonElement("college_id")]
    [BsonRequired]
    public string CollegeId { get; private set; } = string.Empty;

    [BsonElement("team_id")]
    public int TeamId { get; private set; } = 0;
}
