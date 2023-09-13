using AutoMapper;
using FluentValidation.Results;
using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BusinessModule, BusinessModuleDTO>();
            CreateMap<BusinessModuleDTO, BusinessModule>();

            CreateMap<ValidationFailure, ErrorResultDTO>();
            CreateMap<ErrorResultDTO, ValidationFailure>();
        }
    }
}
