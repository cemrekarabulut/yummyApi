using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YummyApi.entities
{
    public class Chef
    {
        public int ChefId { get; set; }

        public string NameSurname { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}