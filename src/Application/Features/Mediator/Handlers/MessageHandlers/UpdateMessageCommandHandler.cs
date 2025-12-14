using Application.Dtos.MessageDtos;
using Application.Features.Mediator.Commands.MessageCommands;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.MessageEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.MessageHandlers
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, IResult<DomainSuccess<ResultMessageDto>, DomainError>>
    {
        private readonly IRepository<Domain.Entities.MessageEntity.Message, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public UpdateMessageCommandHandler(IRepository<Message, int> repository, IMapper mapper, ILocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<ResultMessageDto>, DomainError>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _repository.GetFirstAsync(a => a.Id == request.Id);
            if (message == null)
            {
                return Result.Fail<ResultMessageDto>(DomainError.NotFound("Message not found"));
            }
           
            message.Content = request.Content;
            message.ChatId = request.ChatId;
            await _repository.UpdateAsync(message);
            await _repository.CommitAsync(cancellationToken);
            var dto = _mapper.Map<ResultMessageDto>(message);
            return Result.Success<ResultMessageDto>(DomainSuccess<ResultMessageDto>.OK(dto, _localizationService[ResponseMessages.ExampleUpdatedSuccessfully]));
        }
    }
}
