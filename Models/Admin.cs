using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Admin {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [NotNull]
    public int Id { get; set; }
    [BsonElement("Username")]
    public string? Username {  get; set; }
    [BsonElement("Password")]
    public string? Password { get; set; }
}
