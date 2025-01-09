using Core.Interfaces.Repositories;
using Core.Interfaces;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Tests
{
    [TestFixture]
    public class PropertyImageServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPropertyRepository> _mockPropertyRepository;
        private Mock<IPropertyImageRepository> _mockPropertyImageRepository;
        private PropertyImageService _propertyImageService;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPropertyRepository = new Mock<IPropertyRepository>();
            _mockPropertyImageRepository = new Mock<IPropertyImageRepository>();

            _mockUnitOfWork.Setup(uow => uow.PropertyRepository).Returns(_mockPropertyRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.PropertyImageRepository).Returns(_mockPropertyImageRepository.Object);

            _propertyImageService = new PropertyImageService(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task UploadImageProperty_ShouldThrowException_WhenPropertyDoesNotExist()
        {
            // Arrange
            int propertyId = 1;
            using var fakeStream = new MemoryStream();
            string fileName = "image.jpg";
            string uploadFolder = "uploads";
            string urlBase = "http://example.com";

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync((Property)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _propertyImageService.UplodadImageProperty(propertyId, fakeStream, fileName, uploadFolder, urlBase));

            Assert.AreEqual("Property not found", exception.Message);
        }

        [Test]
        public async Task UploadImageProperty_ShouldCreateDirectory_WhenItDoesNotExist()
        {
            // Arrange
            int propertyId = 1;
            using var fakeStream = new MemoryStream(new byte[100]); // Simula un archivo de 100 bytes
            string fileName = "image.jpg";
            string uploadFolder = "uploads";
            string urlBase = "http://example.com";

            var property = new Property { IdProperty = propertyId, Name = "Test Property" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(property);
            _mockPropertyImageRepository.Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(1);

            // Simula que el directorio no existe
            if (Directory.Exists(uploadFolder))
                Directory.Delete(uploadFolder, true);

            // Act
            await _propertyImageService.UplodadImageProperty(propertyId, fakeStream, fileName, uploadFolder, urlBase);

            // Assert
            Assert.IsTrue(Directory.Exists(uploadFolder), "El directorio de subida no fue creado.");
        }

        [Test]
        public async Task UploadImageProperty_ShouldSaveImageToRepository_WhenValid()
        {
            // Arrange
            int propertyId = 1;
            using var fakeStream = new MemoryStream(new byte[100]); // Simula un archivo de 100 bytes
            string fileName = "image.jpg";
            string uploadFolder = "uploads";
            string urlBase = "http://example.com";

            var property = new Property { IdProperty = propertyId, Name = "Test Property" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(property);
            _mockPropertyImageRepository.Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _propertyImageService.UplodadImageProperty(propertyId, fakeStream, fileName, uploadFolder, urlBase);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(propertyId, result.IdProperty);
            Assert.IsTrue(result.Url.StartsWith(urlBase), "La URL de la imagen no es correcta.");
        }

        [Test]
        public async Task UploadImageProperty_ShouldCopyFile_WhenValid()
        {
            // Arrange
            int propertyId = 1;
            using var fakeStream = new MemoryStream(new byte[100]); // Simula un archivo de 100 bytes
            string fileName = "image.jpg";
            string uploadFolder = "uploads";
            string urlBase = "http://example.com";

            var property = new Property { IdProperty = propertyId, Name = "Test Property" };

            _mockPropertyRepository.Setup(repo => repo.GetByIdAsync(propertyId)).ReturnsAsync(property);
            _mockPropertyImageRepository.Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(1);

            string newFileName = $"{Guid.NewGuid()}_{fileName}";
            string expectedFilePath = Path.Combine(uploadFolder, newFileName);

            // Mock del FileStream
            var mockFileStream = new Mock<FileStream>(expectedFilePath, FileMode.Create);

            // Act
            var result = await _propertyImageService.UplodadImageProperty(propertyId, fakeStream, fileName, uploadFolder, urlBase);

            // Assert
            _mockPropertyImageRepository.Verify(repo => repo.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

    }
}
