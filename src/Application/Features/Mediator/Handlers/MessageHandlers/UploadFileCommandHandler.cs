using Application.Dtos.MessageDtos;
using Application.Features.Mediator.Commands.MessageCommands;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Entities.MessageEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.MessageHandlers
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, IResult<DomainSuccess<ResultMessageDto>, DomainError>>
    {
        private readonly IRepository<Message, int> _repository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _service;

        public UploadFileCommandHandler(ILocalizationService service, IMapper mapper, IRepository<Message, int> repository)
        {
            _service = service;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResult<DomainSuccess<ResultMessageDto>, DomainError>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request.file == null || request.file.Length == 0)
                return Result.Fail<ResultMessageDto>(DomainError.BadRequest("No file uploaded"));

            // Faylın serverdə saxlanacağı qovluq
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", request.file.FileName);

            // Qovluq yoxdursa yaradılır
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));
            }

            // Faylı serverdə saxlamaq
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.file.CopyToAsync(stream);
            }
          
            var message = new Message
            {
                ChatId = request.ChatId,
                SentDate = DateTime.UtcNow,
                Content=request.Content,
                FilePath = path,
                FileName = request.file.FileName,
                FileSize = request.file.Length,
            };
            var dto = _mapper.Map<ResultMessageDto>(message);
            await _repository.AddAsync(message);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success<ResultMessageDto>(DomainSuccess<ResultMessageDto>.OK(dto, _service["File uploaded successfully"]));
        }
    }
}
