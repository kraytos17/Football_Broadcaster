using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;
namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Player
{
    [BsonElement("name")]
    [BsonRequired]
    public string? Name { get; set; }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [NotNull]
    public string? Id { get; set; }
    [BsonElement("image_link")]
    [BsonRequired]
    public string? Imagelink { get; set; }
    [BsonElement("position")]
    [BsonRequired]
    public string? Position { get; set; }
    [BsonElement("branch")]
    [BsonRequired]
    public string? Branch { get; set; }
    [BsonElement("age")]
    [BsonRequired]
    public int Age { get; set; }
    [BsonElement("year")]
    [BsonRequired]
    public int Year { get; set; }
    [BsonElement("instagram")]
    public string? Instagram { get; set; }
    [BsonElement("goals")]
    public int Goals { get; set; }
    [BsonElement("assists")]
    public int Assists { get; set; }
    [BsonElement("college_id")]
    [BsonRequired]
    public string? CollegeId { get; set; }
    [BsonElement("team_id")]
    public int TeamId { get; set; }
}
