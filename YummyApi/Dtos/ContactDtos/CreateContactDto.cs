using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YummyApi.Dtos.ContactDtos
{
    public class CreateContactDto
    {

        public string MapLocation { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string OpenHours { get; set; }
    }
}