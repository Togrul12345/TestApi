using Application.Dtos.ChatDtos;
using Application.Dtos.ChatUserDtos;
using Application.Features.Mediator.Queries.ChatQueries;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ChatHandlers
{
    public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, IResult<DomainSuccess<List<ResultChatDto>>, DomainError>>
    {
        private readonly IRepository<ChatUser, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public GetChatsByUserIdQueryHandler(IRepository<ChatUser, int> repository, IMapper mapper, ILocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<List<ResultChatDto>>, DomainError>> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userchat = await _repository.GetListAsync(a => a.ParticipantId == request.Id);
            if (userchat == null)
            {
                return Result.Fail<List<ResultChatDto>>(DomainError.NotFound("Chat not found in this UserId"));
            }
            var chats = userchat.Select(a => a.Chat);
            var dtos = _mapper.Map<List<ResultChatDto>>(chats);
            return Result.Success<List<ResultChatDto>>(DomainSuccess<List<ResultChatDto>>.OK(dtos, _localizationService[ResponseMessages.ExampleFoundSuccessfully]));
        }
    }
}
