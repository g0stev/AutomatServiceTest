namespace AutomatServiceTest.Domain.Models;

/// <summary>
/// Товар на складе
/// </summary>
public class StorageProduct
{
    public int StorageId { get; set; }

    public Storage Storage { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Count { get; set; }
}