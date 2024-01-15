namespace Gc_Broadcasting_Api.Models;

public class DatabaseSettings {
    public string ConnectionString { get; init; } = null!;
    public string DatabaseName { get; init; } = null!;
    public string PlayerCollectionName { get; init; } = null!;
    public string TeamCollectionName { get; init; } = null!;
    public string AdminCollectionName {  get; init; } = null!;
}
