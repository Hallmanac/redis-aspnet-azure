using System;
using System.ComponentModel.DataAnnotations;

namespace RedisWithAspNet4_6.Web.Models
{
    public class Minion
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public MinionTraits Traits { get; set; }

        [Display(Name = "Movie Moments")]
        public string MovieMoments { get; set; }
    }
}
