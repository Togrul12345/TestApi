using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MessageEntity
{
    public class FileMessage:Message
    {
        public string FilePath { get; set; } // Faylın serverdəki yolu və ya URL
        public string FileName { get; set; }
        public long FileSize { get; set; } // Bayt ilə ölçüsü
    }
}
