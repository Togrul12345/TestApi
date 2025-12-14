using Application.Dtos.ChatAdminDtos;
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
    public class DeleteAdminFromChatCommandHandler : IRequestHandler<DeleteAdminFromChatCommand, IResult<DomainSuccess<ResultChatAdminDto>, DomainError>>
    {
        private readonly IRepository<ChatAdmin, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _service;

        public DeleteAdminFromChatCommandHandler(IRepository<ChatAdmin, int> repository, IMapper mapper, ILocalizationService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<IResult<DomainSuccess<ResultChatAdminDto>, DomainError>> Handle(DeleteAdminFromChatCommand request, CancellationToken cancellationToken)
        {
            var chatUser =await _repository.GetFirstAsync(a => a.ChatId == request.ChatId && a.AdminId == request.UserId);
            if (chatUser == null)
            {
                return Result.Fail<ResultChatAdminDto>(DomainError.NotFound("ChatId or AdminId not found"));
            }
            var dto = _mapper.Map<ResultChatAdminDto>(chatUser);
           await _repository.DeleteAsync(chatUser);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success <ResultChatAdminDto>(DomainSuccess<ResultChatAdminDto>.OK(dto, _service[ResponseMessages.ExampleDeletedSuccessfully]));

        }
    }
}
