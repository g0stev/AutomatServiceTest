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

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _inventoryService.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("GetProducts/{storageId}")]
    public async Task<IActionResult> GetProducts(int storageId)
    {
        var products = await _inventoryService.GetProducts(storageId);

        return Ok(products);
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDTO productModel)
    {
        var result = await _inventoryService.CreateProduct(productModel);

        return result
            ? Ok()
            : StatusCode(500);
    }

    [HttpPost("IncProductInStorage")]
    public async Task<IActionResult> IncProductInStorage([FromBody] AddProductToStorageRequestDTO storageProductModel)
    {
        var result = await _inventoryService.IncProductInStorage(storageProductModel);

        return result
            ? Ok()
            : StatusCode(500);
    }

    [HttpPost("DecProductInStorage")]
    public async Task<IActionResult> DecProductInStorage([FromBody] AddProductToStorageRequestDTO storageProductModel)
    {
        var result = await _inventoryService.DecProductInStorage(storageProductModel);

        return result
            ? Ok()
            : StatusCode(500);
    }

    [HttpPost("AddProductToStorage")]
    public async Task<IActionResult> AddProductToStorage([FromBody] AddProductToStorageWithCountRequestDTO storageProductModel)
    {
        var result = await _inventoryService.AddProductToStorage(storageProductModel);

        return result
            ? Ok()
            : StatusCode(500);
    }
}