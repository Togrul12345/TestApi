using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatDtos
{
    public class CreateChatDto
    {
        public string Avatar { get; set; }
        public string FoneImg { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
