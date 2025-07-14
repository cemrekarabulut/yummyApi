using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YummyApi.entities;

namespace YummyApi
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string MapLocation { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string OpenHours { get; set; }
    }
}