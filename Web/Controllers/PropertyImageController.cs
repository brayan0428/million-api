using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/property-image")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public PropertyImageController(IPropertyImageService propertyImageService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _propertyImageService = propertyImageService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpPost("{idProperty}")]
        public async Task<IActionResult> UploadImage(int idProperty, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is empty");
                }
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                {
                    return BadRequest("Invalid file extension");
                }
                string urlBase = $"{Request.Scheme}://{Request.Host}/images";
                string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                using (var stream = file.OpenReadStream())
                {
                    var propertyImage = await _propertyImageService.UplodadImageProperty(idProperty, stream, file.FileName, folder, urlBase);
                    return Ok(_mapper.Map<PropertyImage, PropertyImageModel>(propertyImage));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
