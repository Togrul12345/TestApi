using Application.Dtos.ChatDtos;
using Application.Features.Mediator.Commands.ChatCommands;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.ChatEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ChatHandlers
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, IResult<DomainSuccess<ResultChatDto>, DomainError>>
    {
        private readonly IRepository<Chat,int> _chatRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public CreateChatCommandHandler(IRepository<Chat, int> chatRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<ResultChatDto>, DomainError>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Avatar = request.Avatar,
                FoneImg=request.FoneImg,
                CreatedDate = request.CreateAt
            };
            await _chatRepository.AddAsync(chat);
           await _chatRepository.CommitAsync(cancellationToken);
            var dto = _mapper.Map<ResultChatDto>(chat);
            return Result.Success<ResultChatDto>(DomainSuccess<ResultChatDto>.Created(dto, _localizationService[ResponseMessages.ExampleCreatedSuccessfully]));
        }
    }
}
