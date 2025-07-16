using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YummyApi.Dtos.ContactDtos.MessageDto
{
    public class GetByIdMessageDto
    {
         public int MessageId { get; set; }

        public string NameSurname { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string MessageDetails { get; set; }

        public DateTime SendDate{ get; set; }

        public bool IsRead { get; set; }
    }
}