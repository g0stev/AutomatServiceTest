using Xunit;
using Moq;
using AutomatServiceTest.Abstraction.IServices;
using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;

namespace AutomatServiceTest.Tests
{
    public class InventoryServiceTests
    {
        private readonly Mock<IInventoryService> _mockInventoryService;

        public InventoryServiceTests()
        {
            _mockInventoryService = new Mock<IInventoryService>();
        }

        [Fact]
        public async Task GetStorages_ReturnsListOfStorages()
        {
            // Arrange
            var storages = new List<StorageResponseDTO>
            {
                new StorageResponseDTO { Id = 1, Name = "Storage1" },
                new StorageResponseDTO { Id = 2, Name = "Storage2" }
            };

            _mockInventoryService.Setup(service => service.GetStorages())
                .ReturnsAsync(storages);

            // Act
            var result = await _mockInventoryService.Object.GetStorages();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateStorage_ReturnsTrue()
        {
            // Arrange
            var storageRequest = new CreateStorageRequestDTO { Name = "NewStorage" };

            _mockInventoryService.Setup(service => service.CreateStorage(storageRequest))
                .ReturnsAsync(true);

            // Act
            var result = await _mockInventoryService.Object.CreateStorage(storageRequest);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<ProductResponseDTO>
            {
                new ProductResponseDTO { Id = 1, Name = "Product1" },
                new ProductResponseDTO { Id = 2, Name = "Product2" }
            };

            _mockInventoryService.Setup(service => service.GetAllProducts())
                .ReturnsAsync(products);

            // Act
            var result = await _mockInventoryService.Object.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnsProductsInStorage()
        {
            // Arrange
            var storageId = 1;
            var productsInStorage = new List<ProductInStorageResponseDTO>
            {
                new ProductInStorageResponseDTO { Id = 1, Name = "Product1", Count = 10 },
                new ProductInStorageResponseDTO { Id = 2, Name = "Product2", Count = 5 }
            };

            _mockInventoryService.Setup(service => service.GetProducts(storageId))
                .ReturnsAsync(productsInStorage);

            // Act
            var result = await _mockInventoryService.Object.GetProducts(storageId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateProduct_ReturnsTrue()
        {
            // Arrange
            var productRequest = new CreateProductRequestDTO { Name = "NewProduct" };

            _mockInventoryService.Setup(service => service.CreateProduct(productRequest))
                .ReturnsAsync(true);

            // Act
            var result = await _mockInventoryService.Object.CreateProduct(productRequest);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IncProductInStorage_ReturnsTrue()
        {
            // Arrange
            var request = new AddProductToStorageRequestDTO { StorageId = 1, ProductId = 1 };

            _mockInventoryService.Setup(service => service.IncProductInStorage(request))
                .ReturnsAsync(true);

            // Act
            var result = await _mockInventoryService.Object.IncProductInStorage(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DecProductInStorage_ReturnsTrue()
        {
            // Arrange
            var request = new AddProductToStorageRequestDTO { StorageId = 1, ProductId = 1 };

            _mockInventoryService.Setup(service => service.DecProductInStorage(request))
                .ReturnsAsync(true);

            // Act
            var result = await _mockInventoryService.Object.DecProductInStorage(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddProductToStorage_ReturnsTrue()
        {
            // Arrange
            var request = new AddProductToStorageWithCountRequestDTO { StorageId = 1, ProductId = 1, Count = 5 };

            _mockInventoryService.Setup(service => service.AddProductToStorage(request))
                .ReturnsAsync(true);

            // Act
            var result = await _mockInventoryService.Object.AddProductToStorage(request);

            // Assert
            Assert.True(result);
        }
    }
}