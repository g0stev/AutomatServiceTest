using AutoMapper;
using AutomatServiceTest.Context;
using AutomatServiceTest.Domain.Models;
using AutomatServiceTest.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AutomatServiceTest.Abstraction.Models.Request;
using AutomatServiceTest.Abstraction.Models.Response;

namespace AutomatServiceTest.Tests
{
    public class InventoryServiceTests
    {
        private readonly Mock<ILogger<InventoryService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<AutomatServiceTestContext> _contextMock;

        public InventoryServiceTests()
        {
            _loggerMock = new Mock<ILogger<InventoryService>>();
            _mapperMock = new Mock<IMapper>();
            _contextMock = new Mock<AutomatServiceTestContext>(new DbContextOptions<AutomatServiceTestContext>());
        }

        [Fact]
        public async Task GetStorages_ReturnsListOfStorages()
        {
            // Arrange
            var storages = new List<Storage> { new Storage { Id = 1, Name = "Test Storage" } };
            var storageDbSetMock = CreateDbSetMock(storages);
            _contextMock.Setup(c => c.Storages).Returns(storageDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<List<StorageResponseDTO>>(It.IsAny<List<Storage>>()))
                .Returns(new List<StorageResponseDTO> { new StorageResponseDTO { Id = 1, Name = "Test Storage" } });

            // Act
            var result = await service.GetStorages();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Storage", result[0].Name);
        }

        [Fact]
        public async Task CreateStorage_ReturnsTrue_WhenStorageIsCreated()
        {
            // Arrange
            var storageModel = new CreateStorageRequestDTO { Name = "New Storage" };
            var storage = new Storage { Name = "New Storage" };
            _mapperMock.Setup(m => m.Map<Storage>(It.IsAny<CreateStorageRequestDTO>())).Returns(storage);
            _contextMock.Setup(c => c.Add(It.IsAny<Storage>())).Callback<Storage>(s => storage = s);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await service.CreateStorage(storageModel);

            // Assert
            Assert.True(result);
            _contextMock.Verify(c => c.Add(It.IsAny<Storage>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product" } };
            var productDbSetMock = CreateDbSetMock(products);
            _contextMock.Setup(c => c.Products).Returns(productDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<List<ProductResponseDTO>>(It.IsAny<List<Product>>()))
                .Returns(new List<ProductResponseDTO> { new ProductResponseDTO { Id = 1, Name = "Test Product" } });

            // Act
            var result = await service.GetAllProducts();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Product", result[0].Name);
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProductsInStorage()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product" };
            var storageProducts = new List<StorageProduct>
            {
                new StorageProduct { StorageId = 1, ProductId = 1, Count = 10, Product = product }
            };
            var storageProductDbSetMock = CreateDbSetMock(storageProducts);
            _contextMock.Setup(c => c.StorageProducts).Returns(storageProductDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await service.GetProducts(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Product", result[0].Name);
            Assert.Equal(10, result[0].Count);
        }

        [Fact]
        public async Task CreateProduct_ReturnsTrue_WhenProductIsCreated()
        {
            // Arrange
            var productModel = new CreateProductRequestDTO { Name = "New Product"};
            var product = new Product { Name = "New Product"};
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<CreateProductRequestDTO>())).Returns(product);
            _contextMock.Setup(c => c.Add(It.IsAny<Product>())).Callback<Product>(p => product = p);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);

            // Act
            var result = await service.CreateProduct(productModel);

            // Assert
            Assert.True(result);
            _contextMock.Verify(c => c.Add(It.IsAny<Product>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task IncProductInStorage_IncrementsProductCount()
        {
            // Arrange
            var storageProduct = new StorageProduct { StorageId = 1, ProductId = 1, Count = 5 };
            var storageProducts = new List<StorageProduct> { storageProduct };
            var storageProductDbSetMock = CreateDbSetMock(storageProducts);
            _contextMock.Setup(c => c.StorageProducts).Returns(storageProductDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);
            var storageProductModel = new AddProductToStorageRequestDTO { StorageId = 1, ProductId = 1 };

            // Act
            var result = await service.IncProductInStorage(storageProductModel);

            // Assert
            Assert.True(result);
            Assert.Equal(6, storageProduct.Count);
        }

        [Fact]
        public async Task DecProductInStorage_DecrementsProductCount()
        {
            // Arrange
            var storageProduct = new StorageProduct { StorageId = 1, ProductId = 1, Count = 5 };
            var storageProducts = new List<StorageProduct> { storageProduct };
            var storageProductDbSetMock = CreateDbSetMock(storageProducts);
            _contextMock.Setup(c => c.StorageProducts).Returns(storageProductDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);
            var storageProductModel = new AddProductToStorageRequestDTO { StorageId = 1, ProductId = 1 };

            // Act
            var result = await service.DecProductInStorage(storageProductModel);

            // Assert
            Assert.True(result);
            Assert.Equal(4, storageProduct.Count);
        }

        [Fact]
        public async Task AddProductToStorage_AddsProductCorrectly()
        {
            // Arrange
            var storageProductModel = new AddProductToStorageWithCountRequestDTO { StorageId = 1, ProductId = 1, Count = 5 };
            var storageProducts = new List<StorageProduct>();
            var storageProductDbSetMock = CreateDbSetMock(storageProducts);
            _contextMock.Setup(c => c.StorageProducts).Returns(storageProductDbSetMock.Object);

            var service = new InventoryService(_contextMock.Object, _loggerMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<StorageProduct>(It.IsAny<AddProductToStorageWithCountRequestDTO>()))
                .Returns(new StorageProduct { StorageId = 1, ProductId = 1, Count = 5 });

            // Act
            var result = await service.AddProductToStorage(storageProductModel);

            // Assert
            Assert.True(result);
            _contextMock.Verify(c => c.Add(It.IsAny<StorageProduct>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(List<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => elements.Add(s));
            return dbSetMock;
        }
    }
}