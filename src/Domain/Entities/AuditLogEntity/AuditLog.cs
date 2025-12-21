using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuditLogEntity
{
    public class AuditLog:BaseAuditedEntity<int>
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Target { get; set; }
        [NotMapped]
        public object Detail { get; set; }
        public string IpAdress { get; set; }
    }
}
