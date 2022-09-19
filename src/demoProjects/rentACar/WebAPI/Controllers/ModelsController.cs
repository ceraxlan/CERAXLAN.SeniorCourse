using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Models.Models;
using RentACar.Application.Features.Models.Queries.GetListModel;
using RentACar.Application.Features.Models.Queries.GetListModelByDynamic;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getlistModelQuery = new GetListModelQuery { PageRequest = pageRequest};
            ModelListModel result = await Mediator.Send(getlistModelQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,[FromBody] Dynamic dynamic)
        {
            GetListModelByDynamicQuery getlistByDynamicModelQuery = new GetListModelByDynamicQuery { PageRequest = pageRequest , Dynamic = dynamic };
            ModelListModel result = await Mediator.Send(getlistByDynamicModelQuery);
            return Ok(result);
        }
    }
}
