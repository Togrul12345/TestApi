using Application.Dtos.ChatDtos;
using Application.Features.Mediator.Queries.ChatQueries;
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
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, IResult<DomainSuccess<ResultChatDto>, DomainError>>
    {
        private readonly IRepository<Chat, int> _chatRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public GetChatByIdQueryHandler(IRepository<Chat, int> chatRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async Task<IResult<DomainSuccess<ResultChatDto>, DomainError>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat =await _chatRepository.GetFirstAsync(a => a.Id == request.Id);
            if (chat == null)
            {
                return Result.Fail<ResultChatDto>(DomainError.NotFound(_localizationService[ResponseMessages.ExampleNotFound]));
            }
            var dto = _mapper.Map<ResultChatDto>(chat);
            return Result.Success<ResultChatDto>(DomainSuccess<ResultChatDto>.Created(dto, _localizationService[ResponseMessages.ExampleFoundSuccessfully]));
        }
    }
}
