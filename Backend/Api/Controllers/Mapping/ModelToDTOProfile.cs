﻿using Api.Controllers.DTOs.Usuario;
using AutoMapper;
using Api.Controllers.Resources;
using Api.Domain.Models;
using Api.Extensions;

namespace Api.Controllers.Mapping
{
    public class ModelToDTOProfile : Profile
    {
        public ModelToDTOProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Usuario, RegistrarDTO>();

            CreateMap<Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
        }
    }
}
