using Application.Dtos;
using Application.Features.Mediator.Commands.AppUserCommands;
using AutoMapper;
using CSharpFunctionalExtensions;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Entities.UserEntity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers;

public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, Domain.Common.Results.IResult<DomainSuccess<AppUserDto>, DomainError>>
{
    private readonly IRepository<AppUser, int> _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<AppUser> _passwordHasher;



    public CreateAppUserCommandHandler(IRepository<AppUser, int> repository, IMapper mapper, IPasswordHasher<AppUser> passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<Domain.Common.Results.IResult<DomainSuccess<AppUserDto>, DomainError>> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = new AppUser
        {
            
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Gmail = request.Gmail,
           
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.PasswordHash);
        await _repository.AddAsync(user);
        await _repository.CommitAsync(cancellationToken);
        var dto=_mapper.Map<AppUserDto>(user);
        return Domain.Common.Results.Result.Success<AppUserDto>(DomainSuccess<AppUserDto>.Created(dto,"Created"));
    }
}
