using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using RentACar.Application.Features.Brands.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<UpdatedBrandDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public bool BypassCache { get; }
        //public string CacheKey => "brands-list";
        //public string[] Roles => new[] { Admin, BrandUpdate };
    }
}
