using NUnit.Framework;
using Moq;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Services;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork = null!;
        private Mock<IPropertyRepository> _propertyRepositoryMock = null!;
        private PropertyService _propertyService = null!;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mockUnitOfWork.Setup(u => u.PropertyRepository).Returns(_propertyRepositoryMock.Object);
            _propertyService = new PropertyService(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnProperties_WithOwners()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property
                {
                    IdProperty = 1, Name = "House A", Price = 100000, IdOwner = 1,
                    Owner = new Owner { IdOwner = 1, Name = "John Doe" }
                },
                new Property
                {
                    IdProperty = 2, Name = "House B", Price = 200000, IdOwner = 2,
                    Owner = new Owner { IdOwner = 2, Name = "Jane Doe" }
                }
            };

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.GetAsync(null,null,"Owner"))
                .ReturnsAsync(properties);

            // Act
            var result = await _propertyService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("John Doe", result.First().Owner.Name);
        }

        [Test]
        public async Task CreateProperty_ShouldSaveProperty_WhenValid()
        {
            // Arrange
            var newProperty = new Property
            {
                IdProperty = 1,
                Name = "New House",
                Address = "123 Street",
                Price = 50000,
                CodeInternal = "123XYZ",
                Year = 2022,
                IdOwner = 1
            };

            var owner = new Owner { IdOwner = 1, Name = "John Doe" };

            _mockUnitOfWork.Setup(uow => uow.OwnerRepository.GetByIdAsync(newProperty.IdOwner))
                .ReturnsAsync(owner);

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.GetAsync(p => p.CodeInternal == newProperty.CodeInternal, null, ""))
                .ReturnsAsync(new List<Property>());

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.AddAsync(newProperty))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _propertyService.CreateProperty(newProperty);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newProperty.Name, result.Name);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateProperty_ShouldUpdateProperty_WhenValid()
        {
            // Arrange
            var existingProperty = new Property
            {
                IdProperty = 1,
                Name = "Old House",
                Address = "123 Street",
                Price = 100000,
                CodeInternal = "ABC123",
                Year = 2020,
                IdOwner = 1
            };

            var updatedProperty = new Property
            {
                IdProperty = 1,
                Name = "Updated House",
                Address = "456 Avenue",
                Price = 150000,
                CodeInternal = "XYZ456",
                Year = 2022,
                IdOwner = 1
            };

            var owner = new Owner { IdOwner = 1, Name = "John Doe" };

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.GetByIdAsync(existingProperty.IdProperty))
                .ReturnsAsync(existingProperty);

            _mockUnitOfWork.Setup(uow => uow.OwnerRepository.GetByIdAsync(updatedProperty.IdOwner))
                .ReturnsAsync(owner);

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.GetAsync(p => p.CodeInternal == updatedProperty.CodeInternal, null,""))
                .ReturnsAsync(new List<Property>());

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.Update(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _propertyService.UpdateProperty(existingProperty.IdProperty, updatedProperty);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated House", result.Name);
            Assert.AreEqual("456 Avenue", result.Address);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task UpdatePrice_ShouldUpdatePropertyPrice_WhenPropertyExists()
        {
            // Arrange
            var existingProperty = new Property
            {
                IdProperty = 1,
                Name = "Luxury House",
                Price = 200000
            };

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.GetByIdAsync(1))
                .ReturnsAsync(existingProperty);

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository.Update(existingProperty))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _propertyService.UpdatePrice(1, 250000);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(250000, result.Price);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }
    }
}
