using AutoMapper;
using AutomatServiceTest.Abstraction.IServices;
using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;
using AutomatServiceTest.Context;
using AutomatServiceTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutomatServiceTest.Service.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AutomatServiceTestContext _context;
        private readonly ILogger<InventoryService> _logger;
        private readonly IMapper _mapper;

        public InventoryService(AutomatServiceTestContext context, ILogger<InventoryService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список складов
        /// </summary>
        /// <returns></returns>
        public async Task<List<StorageResponseDTO>> GetStorages()
        {
            var storages = await _context.Storages
                .AsNoTracking()
            .ToListAsync();

            var result = _mapper.Map<List<StorageResponseDTO>>(storages);

            return result;
        }

        /// <summary>
        /// Создать новый склад
        /// </summary>
        /// <param name="storageModel"></param>
        /// <returns></returns>
        public async Task<bool> CreateStorage(CreateStorageRequestDTO storageModel)
        {
            try
            {
                var storage = _mapper.Map<Storage>(storageModel);

                _context.Add(storage);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.ToString());
                return false;
            }
        }
    }
}
