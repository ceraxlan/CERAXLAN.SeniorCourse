using Application.Features.Brands.Models;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<BrandListModel>
    {
        public PageRequest PageRequest { get; set; }
    }
}
