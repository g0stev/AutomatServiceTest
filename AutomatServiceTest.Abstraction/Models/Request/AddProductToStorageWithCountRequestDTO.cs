namespace AutomatServiceTest.Abstraction.Models.Request;

/// <summary>
/// Модель для добавления товара на склад 
/// </summary>
public class AddProductToStorageWithCountRequestDTO : AddProductToStorageRequestDTO
{
    public int Count { get; set; }
}