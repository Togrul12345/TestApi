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
    public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand, IResult<DomainSuccess<ResultChatDto>, DomainError>>
    {
        private readonly IRepository<Chat, int> _chatRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public UpdateChatCommandHandler(IRepository<Chat, int> chatRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async Task<IResult<DomainSuccess<ResultChatDto>, DomainError>> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat =await _chatRepository.GetFirstAsync(request.Id);
            if (chat == null)
            {
                return Result.Fail<ResultChatDto>(DomainError.NotFound(_localizationService[ResponseMessages.ExampleNotFound]));
            }
            chat.Avatar = request.Avatar;
            chat.FoneImg = request.FoneImg;
            var dto = _mapper.Map<ResultChatDto>(chat);
           await _chatRepository.UpdateAsync(chat);
            await _chatRepository.CommitAsync(cancellationToken);
            return Result.Success<ResultChatDto>(DomainSuccess<ResultChatDto>.OK(dto, _localizationService[ResponseMessages.ExampleUpdatedSuccessfully]));
        }
    }
}
