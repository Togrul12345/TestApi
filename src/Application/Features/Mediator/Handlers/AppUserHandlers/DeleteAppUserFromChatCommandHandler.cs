using Application.Dtos;
using Application.Features.Mediator.Commands.AppUserCommands;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class DeleteAppUserFromChatCommandHandler : IRequestHandler<DeleteAppUserFromChatCommand, IResult<DomainSuccess<AppUserDto>, DomainError>>
    {
        private readonly IRepository<ChatUser, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public DeleteAppUserFromChatCommandHandler(IRepository<ChatUser, int> repository, IMapper mapper, ILocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<AppUserDto>, DomainError>> Handle(DeleteAppUserFromChatCommand request, CancellationToken cancellationToken)
        {
            var chatUser =await _repository.GetFirstAsync(a => a.ParticipantId == request.AppUserId && a.ChatId==request.ChatId);
            if (chatUser == null)
            {
                return Result.Fail<AppUserDto>(DomainError.NotFound("User not found"));
            }
            
            var dto = _mapper.Map<AppUserDto>(chatUser.Participant);
            await _repository.DeleteAsync(chatUser);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success<AppUserDto>(DomainSuccess<AppUserDto>.OK(dto, _localizationService[ResponseMessages.ExampleDeletedSuccessfully]));

        }
    }
}
