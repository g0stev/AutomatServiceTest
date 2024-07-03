using AutoMapper;
using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;
using AutomatServiceTest.Domain.Models;

namespace AutomatServiceTest.Service.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Storage, StorageResponseDTO>();
        CreateMap<CreateStorageRequestDTO, Storage>();
    }
}