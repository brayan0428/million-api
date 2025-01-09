using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Core.DTOs;

namespace Web.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyModel>>> GetAll()
        {
            var properties = await _propertyService.GetAll();
            return Ok(_mapper.Map<IEnumerable<Property>,IEnumerable<PropertyModel>>(properties));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PropertyModel>>> Filter([FromQuery] FilterPropertiesDTO filter)
        {
            var properties = await _propertyService.Filter(filter);
            return Ok(_mapper.Map<IEnumerable<Property>, IEnumerable<PropertyModel>>(properties));
        }
    }
}
