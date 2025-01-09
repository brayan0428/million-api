using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;
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

        public async Task<Property> CreateProperty(Property property)
        {
            PropertyValidator propertyValidator = new PropertyValidator();
            var validationResult = await propertyValidator.ValidateAsync(property);
            if (validationResult.IsValid)
            {
                var existOwner = await _unitOfWork.OwnerRepository.GetByIdAsync(property.IdOwner);
                if (existOwner == null)
                {
                    throw new Exception("Owner is not valid");
                }
                var existCodeInternal = await _unitOfWork.PropertyRepository.GetAsync(p => p.CodeInternal == property.CodeInternal);
                if (existCodeInternal.Any())
                {
                    throw new Exception("CodeInternal already exist");
                }
                await _unitOfWork.PropertyRepository.AddAsync(property);
                await _unitOfWork.CommitAsync();
                return property;
            }
            else
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<Property> UpdateProperty(int idProperty,Property property)
        {

            PropertyValidator propertyValidator = new PropertyValidator();
            var validationResult = await propertyValidator.ValidateAsync(property);
            if (validationResult.IsValid)
            {
                var propertyToUpdate = await _unitOfWork.PropertyRepository.GetByIdAsync(idProperty);
                if (propertyToUpdate == null)
                {
                    throw new Exception("Property not found");
                }
                var existOwner = await _unitOfWork.OwnerRepository.GetByIdAsync(property.IdOwner);
                if (existOwner == null)
                {
                    throw new Exception("Owner is not valid");
                }
                if(propertyToUpdate.CodeInternal != property.CodeInternal)
                {
                    var existCodeInternal = await _unitOfWork.PropertyRepository.GetAsync(p => p.CodeInternal == property.CodeInternal);
                    if (existCodeInternal.Any())
                    {
                        throw new Exception("CodeInternal already exist");
                    }
                }
                propertyToUpdate.Name = property.Name;
                propertyToUpdate.Address = property.Address;
                propertyToUpdate.Price = property.Price;
                propertyToUpdate.CodeInternal = property.CodeInternal;
                propertyToUpdate.Year = property.Year;
                propertyToUpdate.IdOwner = property.IdOwner;
                await _unitOfWork.PropertyRepository.Update(propertyToUpdate);
                await _unitOfWork.CommitAsync();
                return property;
            }
            else
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<Property> UpdatePrice(int idProperty, decimal price)
        {
            var propertyToUpdate = await _unitOfWork.PropertyRepository.GetByIdAsync(idProperty);
            if (propertyToUpdate == null)
            {
                throw new Exception("Property not found");
            }
            propertyToUpdate.Price = price;
            await _unitOfWork.PropertyRepository.Update(propertyToUpdate);
            await _unitOfWork.CommitAsync();
            return propertyToUpdate;
        }
    }
}
