using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Services
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<PropertyImage> UplodadImageProperty(int idProperty, Stream fileStream, string fileName, string uploadFolder, string urlBase)
        {
            try
            {
                var property = await _unitOfWork.PropertyRepository.GetByIdAsync(idProperty);
                if (property == null)
                {
                    throw new Exception("Property not found");
                }
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);
                string newFileName = $"{Guid.NewGuid()}_{fileName}";
                string filePath = Path.Combine(uploadFolder, newFileName);
                using (var fileStreamCopy = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileStreamCopy);
                }
                PropertyImage propertyImage = new PropertyImage
                {
                    IdProperty = idProperty,
                    Url = $"{urlBase}/{newFileName}",
                    Enabled = true
                };
                await _unitOfWork.PropertyImageRepository.AddAsync(propertyImage);
                await _unitOfWork.CommitAsync();
                return propertyImage;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
