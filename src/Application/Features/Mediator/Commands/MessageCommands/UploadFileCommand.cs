using Application.Dtos.MessageDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.MessageCommands
{
    public class UploadFileCommand:IRequest<IResult<DomainSuccess<ResultMessageDto>,DomainError>>
    {
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        // Ümumi Əlaqələr

        public int ChatId { get; set; }
        public IFormFile file { get; set; }
    }
}
