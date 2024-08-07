﻿using AutomatServiceTest.Abstraction.Models.Request;
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

        /// <summary>
        /// Получить список всех товаров
        /// </summary>
        /// <returns></returns>
        Task<List<ProductResponseDTO>> GetAllProducts();

        /// <summary>
        /// Получить список товаров на складе
        /// </summary>
        /// <param name="storageId"></param>
        /// <returns></returns>
        Task<List<ProductInStorageResponseDTO>> GetProducts(int storageId);

        /// <summary>
        /// Создать новый товар
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        Task<bool> CreateProduct(CreateProductRequestDTO productModel);

        /// <summary>
        /// Увеличить на 1 товар на складе
        /// </summary>
        /// <param name="storageProductModel"></param>
        /// <returns></returns>
        Task<bool> IncProductInStorage(AddProductToStorageRequestDTO storageProductModel);

        /// <summary>
        /// Уменьшить на 1 товар на складе
        /// </summary>
        /// <param name="storageProductModel"></param>
        /// <returns></returns>
        Task<bool> DecProductInStorage(AddProductToStorageRequestDTO storageProductModel);

        /// <summary>
        /// Добавить товар на склад
        /// </summary>
        /// <param name="storageProductModel"></param>
        /// <returns></returns>
        Task<bool> AddProductToStorage(AddProductToStorageWithCountRequestDTO storageProductModel);
    }
}
