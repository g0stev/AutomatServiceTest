namespace AutomatServiceTest.Abstraction.Models.Response;

/// <summary>
/// Модель для возврата товара на складе
/// </summary>
public class ProductInStorageResponseDTO: ProductResponseDTO
{
    public int Count { get; set; }
}