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
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, IResult<DomainSuccess<ResultMessageDto>, DomainError>>
    {
        private readonly IRepository<Message, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _service;

        public CreateMessageCommandHandler(IRepository<Message, int> repository, IMapper mapper, ILocalizationService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<IResult<DomainSuccess<ResultMessageDto>, DomainError>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Content = request.Content,
                SentDate = DateTime.UtcNow,
                ChatId = request.ChatId,
            };
            var dto = _mapper.Map<ResultMessageDto>(message);
            await _repository.AddAsync(message);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success<ResultMessageDto>(DomainSuccess<ResultMessageDto>.OK(dto, _service[ResponseMessages.ExampleCreatedSuccessfully]));
        }
    }
}
