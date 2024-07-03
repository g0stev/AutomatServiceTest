namespace AutomatServiceTest.Abstraction.Models.Request;

/// <summary>
/// Модель для добавления товара на склад
/// </summary>
public class AddProductToStorageRequestDTO
{
    public int StorageId { get; set; }

    public int ProductId { get; set; }
}