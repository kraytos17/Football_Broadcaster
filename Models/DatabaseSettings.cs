namespace Gc_Broadcasting_Api.Models;

public class DatabaseSettings {
    public string ConnectionString { get; init; } = string.Empty;
    public string DatabaseName { get; init; } = string.Empty;
    public string PlayerCollectionName { get; init; } = string.Empty;
    public string TeamCollectionName { get; init; } = string.Empty;
    public string AdminCollectionName {  get; init; } = string.Empty;
}
