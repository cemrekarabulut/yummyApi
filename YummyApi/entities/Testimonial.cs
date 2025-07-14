using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YummyApi.entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }

        public string NameSurname { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public int ImageUrl { get; set; }
    }
}