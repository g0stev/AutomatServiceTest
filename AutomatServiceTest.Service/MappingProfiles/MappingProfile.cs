using AutoMapper;
using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;
using AutomatServiceTest.Domain.Models;

namespace AutomatServiceTest.Service.MappingProfiles;

public class MappingProfile : Profile
{
    /// <summary>
    /// Маппер DTO и EF объектов
    /// </summary>
    public MappingProfile()
    {
        // Склад
        CreateMap<Storage, StorageResponseDTO>();
        CreateMap<CreateStorageRequestDTO, Storage>();

        // Товар
        CreateMap<Product, ProductResponseDTO>();
        CreateMap<CreateProductRequestDTO, Product>();

        // Манипуляции товара на складе
        CreateMap<AddProductToStorageRequestDTO, StorageProduct>();
        CreateMap<AddProductToStorageWithCountRequestDTO, StorageProduct>();
        CreateMap<AddProductToStorageRequestDTO, AddProductToStorageWithCountRequestDTO>();
        CreateMap<Product, ProductInStorageResponseDTO>();
    }
}