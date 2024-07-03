namespace AutomatServiceTest.Domain.Models;

/// <summary>
/// Товар
/// </summary>
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<StorageProduct> StorageProducts { get; set; }
}