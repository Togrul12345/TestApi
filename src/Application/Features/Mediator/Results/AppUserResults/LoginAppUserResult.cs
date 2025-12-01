using Domain.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.AppUserResults
{
    public class LoginAppUserResult
    {
        public AccessToken Token { get; set; }
    }
}
