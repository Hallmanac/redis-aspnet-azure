using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisWithAspNet4_6.Web.Models
{
    public class MinionTraits
    {
        [Display(Name = "Eyes Quantity")]
        public short EyesQuantity { get; set; }

        [Display(Name = "Hair Style")]
        public string HairStyle { get; set; }

        [Display(Name = "Body Type")]
        public string BodyType { get; set; }

        public short Height { get; set; }

        public string Loves { get; set; }

        public string Hates { get; set; }
    }
}
