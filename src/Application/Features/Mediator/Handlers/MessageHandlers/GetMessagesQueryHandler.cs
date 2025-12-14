using Application.Dtos.MessageDtos;
using Application.Features.Mediator.Queries.MessageQueries;
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
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IResult<DomainSuccess<List<ResultMessageDto>>, DomainError>>
    {
        private readonly IRepository<Message, int> _repository;
        private readonly ILocalizationService _service;

        public GetMessagesQueryHandler(IRepository<Message, int> repository, ILocalizationService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<IResult<DomainSuccess<List<ResultMessageDto>>, DomainError>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var result =await _repository.GetAllListIncludingAsync(a=>a.ChatId==request.ChatId);
            var dtos= result.Select(a => new ResultMessageDto
            {
                ChatId = a.ChatId,
                Content=a.Content,
                FileName = a.FileName,
                FilePath = a.FilePath,
                FileSize = a.FileSize,
                MessageReplyId = a.MessageReplyId,
                SenderId = a.SenderId,
                Reaction = a.Reaction,
                Status = a.Status,
                SentDate = a.SentDate,
                ReceiverId = a.ReceiverId

            }).ToList();
            return Result.Success<List<ResultMessageDto>>(DomainSuccess<List<ResultMessageDto>>.OK(dtos, _service[ResponseMessages.ExampleFoundSuccessfully]));
        }
    }
}
