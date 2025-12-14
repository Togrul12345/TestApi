using Application.Dtos;
using Application.Dtos.ChatDtos;
using Application.Dtos.ChatUserDtos;
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
    public class AddAppUserToChatCommandHandler : IRequestHandler<AddAppUserToChatCommand, IResult<DomainSuccess<ResultChatUserDto>, DomainError>>
    {
        private readonly IRepository<ChatUser, int> _repository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public AddAppUserToChatCommandHandler(IRepository<ChatUser, int> repository, ILocalizationService localizationService, IMapper mapper)
        {
            _repository = repository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async Task<IResult<DomainSuccess<ResultChatUserDto>, DomainError>> Handle(AddAppUserToChatCommand request, CancellationToken cancellationToken)
        {
            var chatUser = new ChatUser
            {
                ChatId = request.ChatId,
                ParticipantId = request.AppUserId
            };
           await _repository.AddAsync(chatUser);
            await _repository.CommitAsync(cancellationToken);
            var dto = _mapper.Map<ResultChatUserDto>(chatUser);
           
           
            return Result.Success<ResultChatUserDto>(DomainSuccess<ResultChatUserDto>.OK(dto, _localizationService[ResponseMessages.ExampleUpdatedSuccessfully]));
        }
    }
}
