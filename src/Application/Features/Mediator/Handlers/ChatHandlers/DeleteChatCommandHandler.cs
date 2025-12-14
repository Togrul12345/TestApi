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
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, IResult<DomainSuccess<ResultChatDto>, DomainError>>
    {
        private readonly IRepository<Chat, int> _repository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public DeleteChatCommandHandler(IRepository<Chat, int> repository, ILocalizationService localizationService, IMapper mapper)
        {
            _repository = repository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async Task<IResult<DomainSuccess<ResultChatDto>, DomainError>> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetFirstAsync(request.Id);
            if (entity == null)
            {
                return Result.Fail<ResultChatDto>(DomainError.NotFound(_localizationService[ResponseMessages.ExampleNotFound]));
            }
            var dto = _mapper.Map<ResultChatDto>(entity);
            await _repository.DeleteAsync(entity);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success<ResultChatDto>(DomainSuccess<ResultChatDto>.OK(dto, _localizationService[ResponseMessages.ExampleDeletedSuccessfully]));
        }
    }
}
