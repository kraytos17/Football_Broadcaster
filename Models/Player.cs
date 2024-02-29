using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gc_Broadcasting_Api.Models;

[BsonIgnoreExtraElements]
public class Player {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = string.Empty;
    
    [BsonElement("firstName")]
    [BsonRequired]
    public string FirstName { get; set; } = string.Empty;
    
    [BsonElement("lastName")]
    [BsonRequired]
    public string LastName { get; set; } = string.Empty;

    [BsonElement("email")]
    [BsonRequired]
    public string Email { get; set; } = string.Empty;

    [BsonElement("password")]
    [BsonRequired]
    public string Password { get; set; } = string.Empty;

    [BsonElement("branch")]
    [BsonRequired]
    public string Branch { get; set; } = string.Empty;

    [BsonElement("year")]
    [BsonRequired]
    public string Year { get; set; } = string.Empty;

    [BsonElement("contactNo")]
    [BsonRequired]
    public string ContactNo { get; set; } = string.Empty;

    [BsonElement("socials")]
    public IEnumerable<string> Socials { get; set; } = Enumerable.Empty<string>();

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; } = false;

    [BsonElement("deletedAt")] 
    public DateTime DeletedAt { get; set; } = DateTime.Now;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int Otp { get; set; } = 0;
}
