using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;

using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.UserEntity;
using Domain.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserQuery, IResult<DomainSuccess<LoginAppUserResult>, DomainError>>
    {
        private readonly IRepository<AppUser, int> _repository;
        private readonly IRepository<RoleUser, int> _roleUsers;
        private readonly IRepository<AppRole, int> _roles;
        private readonly JwtHelper _helper;
        private readonly ILocalizationService _localizationService;

        public LoginAppUserCommandHandler(IRepository<AppUser, int> repository, JwtHelper helper, Domain.Common.Interfaces.IRepository<RoleUser, int> roleUsers, IRepository<AppRole, int> roles, ILocalizationService localizationService)
        {
            _repository = repository;
            _helper = helper;
            _roleUsers = roleUsers;
            _roles = roles;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<LoginAppUserResult>, DomainError>> Handle(LoginAppUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetSingleIncludingAsync(a => a.UserName == request.UserName);
            if (user != null)
            {
                var roleUsers = await _roleUsers.GetListAsync(a => a.AppUserId == user.Id);

                // var roles =await _roles.GetListAsync(a => roleUsers.Any(b => b.AppRoleId == a.Id));
                var roles = await _roles.GetListAsync(a => roleUsers.Select(b => b.AppRoleId).Contains(a.Id));
                var roleNames = roles.Select(a => a.RoleName).ToList();
                var verifyResult = new PasswordHasher<AppUser>()
                    .VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    return Result.Success<LoginAppUserResult>(DomainSuccess<LoginAppUserResult>.Created(new LoginAppUserResult
                    {
                        Token = _helper.CreateTokenForUser(user, roleNames, user.Id.ToString(), new List<string>
                        {
                            "sdssds",
                            "dsdsds"
                        })
                    }, _localizationService[ResponseMessages.LoginSuccessfully]));
                }
            };
            return Result.Fail<LoginAppUserResult>(DomainError.Unauthorized(_localizationService[ResponseMessages.ExampleNotFound]));
        }
    }
}




