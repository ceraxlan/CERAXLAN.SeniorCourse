﻿using RentACar.Application.Features.Brands.Models;
using RentACar.Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            var mappedBrandListModel = _mapper.Map<BrandListModel>(brands);

            return mappedBrandListModel;
        }
    }
}
