using Core.Security.Dtos;
using MediatR;
using RentACar.Application.Features.Auths.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IdAddress { get; set; }
    }
}
