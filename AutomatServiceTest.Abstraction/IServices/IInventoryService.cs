using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;

namespace AutomatServiceTest.Abstraction.IServices
{
    public interface IInventoryService
    {
        /// <summary>
        /// Получить список складов
        /// </summary>
        /// <returns></returns>
        Task<List<StorageResponseDTO>> GetStorages();

        /// <summary>
        /// Создать новый склад
        /// </summary>
        /// <param name="storageModel"></param>
        /// <returns></returns>
        Task<bool> CreateStorage(CreateStorageRequestDTO storageModel);
    }
}
