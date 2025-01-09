using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            return await _unitOfWork.PropertyRepository.GetAsync(includeProperties: "Owner");
        }

        public async Task<IEnumerable<Property>> Filter(FilterPropertiesDTO filter)
        {
            var properties = await _unitOfWork.PropertyRepository.GetAsync(includeProperties: "Owner");
            if (filter.PriceMin.HasValue)
            {
                properties = properties.Where(p => p.Price >= filter.PriceMin.Value);
            }
            if (filter.PriceMax.HasValue)
            {
                properties = properties.Where(p => p.Price <= filter.PriceMax.Value);
            }
            if (filter.YearMin.HasValue)
            {
                properties = properties.Where(p => p.Year >= filter.YearMin.Value);
            }
            if (filter.YearMax.HasValue)
            {
                properties = properties.Where(p => p.Year <= filter.YearMax.Value);
            }
            if (filter.IdOwner.HasValue)
            {
                properties = properties.Where(p => p.IdOwner == filter.IdOwner.Value);
            }
            return properties;
        }
    }
}
