using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguage.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap();
            CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<IPaginate<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap();
            CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage, ProgrammingLanguageListDto>().ReverseMap();
            CreateMap<Kodlama.io.Devs.Domain.Entities.ProgrammingLanguage, ProgrammingLanguageGetByIdDto>().ReverseMap();
        }
    }
}
