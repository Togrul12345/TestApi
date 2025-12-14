using Application.Dtos.ChatAdminDtos;
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
    public class AssignAdminToChatCommandHandler : IRequestHandler<AssignAdminToChatCommand, IResult<DomainSuccess<ResultChatAdminDto>, DomainError>>
    {
        private readonly IRepository<ChatAdmin, int> _repository;
        private readonly ILocalizationService _service;
        private readonly IMapper _mapper;

        public AssignAdminToChatCommandHandler(IRepository<ChatAdmin, int> repository, ILocalizationService service, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IResult<DomainSuccess<ResultChatAdminDto>, DomainError>> Handle(AssignAdminToChatCommand request, CancellationToken cancellationToken)
        {

            var chatAdmin = new ChatAdmin
            {
                AdminId = request.UserId,
                ChatId=request.UserId
            };
            var dto = _mapper.Map<ResultChatAdminDto>(chatAdmin);
            await _repository.AddAsync(chatAdmin);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success<ResultChatAdminDto>(DomainSuccess<ResultChatAdminDto>.OK(dto, _service[ResponseMessages.ExampleUpdatedSuccessfully]));

        }
    }
}
