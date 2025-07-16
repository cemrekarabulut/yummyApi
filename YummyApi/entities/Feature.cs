using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YummyApi.entities
{
    public class Feature
    {
        
        public int FeatureId { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; } 

        public string Image { get; set; }        
    }
}