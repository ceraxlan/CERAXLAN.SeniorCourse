using Core.Application.Pipelines.Authorization;
using MediatR;
using RentACar.Application.Features.Brands.Dtos;
using static RentACar.Application.Features.Brands.Constants.OperationClaims;
using static RentACar.Domain.Constants.OperationClaims;


namespace RentACar.Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<DeletedBrandDto>, ISecuredRequest
    {
        public int Id { get; set; }

        //public bool BypassCache { get; }
        //public string CacheKey => "brands-list";
        public string[] Roles => new[] { Admin, BrandDelete };
    }
}
