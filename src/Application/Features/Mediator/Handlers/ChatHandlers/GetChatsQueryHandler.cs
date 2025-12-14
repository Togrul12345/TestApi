using Application.Dtos.ChatDtos;
using Application.Features.Mediator.Queries.ChatQueries;
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
    public class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, IResult<DomainSuccess<List<ResultChatDto>>, DomainError>>
    {
        private readonly IRepository<Chat, int> _chatRepository;
        private readonly ILocalizationService _localizationService;

        public GetChatsQueryHandler(IRepository<Chat, int> chatRepository, ILocalizationService localizationService)
        {
            _chatRepository = chatRepository;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<List<ResultChatDto>>, DomainError>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var result = await _chatRepository.GetAllListAsync();
            if (result == null)
            {
                return Result.Fail<List<ResultChatDto>>(DomainError.NotFound());
            }
            var dto = result.Select(a => new ResultChatDto
            {
                Id = a.Id,
                Avatar = a.Avatar,
                FoneImg = a.FoneImg,

            }).ToList();
            return Result.Success<List<ResultChatDto>>(DomainSuccess<List<ResultChatDto>>.OK(dto, _localizationService[ResponseMessages.ExampleFoundSuccessfully]));
        }
    }
}
