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
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, IResult<DomainSuccess<ResultMessageDto>, DomainError>>
    {private readonly IRepository<Message,int> _messageRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _service;

        public DeleteMessageCommandHandler(IRepository<Message, int> messageRepository, IMapper mapper, ILocalizationService service = null)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<IResult<DomainSuccess<ResultMessageDto>, DomainError>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message =await _messageRepository.GetFirstAsync(a => a.Id == request.Id);
            if (message == null)
            {
                return Result.Fail<ResultMessageDto>(DomainError.NotFound("Message not found"));
            }
            var dto=_mapper.Map<ResultMessageDto>(message);
           await _messageRepository.DeleteAsync(message);
            await _messageRepository.CommitAsync(cancellationToken);
            return Result.Success<ResultMessageDto>(DomainSuccess<ResultMessageDto>.OK(dto, _service[ResponseMessages.ExampleDeletedSuccessfully]));
        }
    }
}
