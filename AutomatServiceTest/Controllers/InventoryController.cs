using AutomatServiceTest.Abstraction.IServices;
using AutomatServiceTest.Abstraction.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace AutomatServiceTest.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("GetStorages")]
    public async Task<IActionResult> GetStorages()
    {
        var storages = await _inventoryService.GetStorages();

        return Ok(storages);
    }

    [HttpPost("CreateStorage")]
    public async Task<IActionResult> CreateStorage([FromBody] CreateStorageRequestDTO storageModel)
    {
        var result = await _inventoryService.CreateStorage(storageModel);

        return result
            ? Ok()
            : StatusCode(500);
    }
}