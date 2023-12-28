namespace Gc_Broadcasting_Api.Models;

public class DatabaseSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PlayerCollectionName { get; set; } = null!;
    public string TeamCollectionName { get; set; } = null!;
    public string AdminCollectionName {  get; set; } = null!;
}
