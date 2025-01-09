using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAll();
        Task<IEnumerable<Property>> Filter(FilterPropertiesDTO filter);
        Task<Property> CreateProperty(Property property);
        Task<Property> UpdateProperty(int idProperty,Property property);
    }
}
