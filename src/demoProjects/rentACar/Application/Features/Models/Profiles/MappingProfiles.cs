﻿using AutoMapper;
using Core.Persistence.Paging;
using RentACar.Application.Features.Models.Dtos;
using RentACar.Application.Features.Models.Models;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Models.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model, ModelListDto>().ForMember(x=>x.BrandName,opt=>opt.MapFrom(x=>x.Brand.Name)).ReverseMap();
            CreateMap<IPaginate<Model>,ModelListModel>().ReverseMap();
        }
    }
}
