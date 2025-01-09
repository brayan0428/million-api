﻿using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Entity to Model
            CreateMap<Property, PropertyModel>();
            CreateMap<Owner, OwnerModel>();

            //Model to Entity
            CreateMap<PropertyModel, Property>();
        }
    }
}
