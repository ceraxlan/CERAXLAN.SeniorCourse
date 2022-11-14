using RentACar.Application.Features.Brands.Commands.CreateBrand;
using RentACar.Application.Features.Brands.Dtos;
using RentACar.Application.Features.Brands.Queries.GetByIdBrand;
using RentACar.Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using RentACar.Application.Features.Brands.Models;
using RentACar.Application.Features.Brands.Commands.UpdateBrand;
using RentACar.Application.Features.Brands.Commands.DeleteBrand;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {      
        [HttpGet]
        [ProducesResponseType(typeof(BrandListModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var getListBrandQuery = new GetListBrandQuery { PageRequest = pageRequest };
            var result = await Mediator.Send(getListBrandQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(BrandGetByIdDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
        {
            var result = await Mediator.Send(getByIdBrandQuery);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedBrandDto),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdatedBrandDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
        {
            UpdatedBrandDto result = await Mediator.Send(updateBrandCommand);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(DeletedBrandDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] DeleteBrandCommand deleteBrandCommand)
        {
            DeletedBrandDto result = await Mediator.Send(deleteBrandCommand);
            return Ok(result);
        }
    }
}
