using System;
using System.Collections.Generic;
using System.Linq;

using RedisWithAspNet4_6.Web.Models;


namespace RedisWithAspNet4_6.Web.App_Core.ReadServices
{
    public class MinionsReadService : IMinionsReadService
    {
        private List<Minion> _allMinions;

        public List<Minion> GetAllMinions()
        {
            return _allMinions = GenerateMinions();
        }

        public Minion GetMinion(Guid id)
        {
            if(_allMinions == null)
            {
                _allMinions = GenerateMinions();
            }
            return _allMinions.FirstOrDefault(m => m.Id == id);
        }

        private List<Minion> GenerateMinions()
        {
            var minions = new List<Minion>
            {
                new Minion
                {
                    Id = Guid.NewGuid(),
                    Name = "Dave",
                    Nickname = "Bazooka Dave",
                    MovieMoments = "When Gru was about to share his lates nefarious scheme, Dave got over-excited and fired his bazooka. The end result: an exploding fountain of minions.",
                    Traits = new MinionTraits
                    {
                        EyesQuantity = 2,
                        HairStyle = "Flat, Center-parted",
                        BodyType = "Slim",
                        Height = 105,
                        Loves = "Rockets & Missiles",
                        Hates = "Waiting"
                    }
                },
                new Minion
                {
                    Id = Guid.NewGuid(),
                    Name = "Stuart",
                    Nickname = "Glow Stick",
                    MovieMoments = "While trying to escape from Vector's house, they ended up inside a dark ventilation shaft. As they needed light, Jerry picked Stuart up and cracked him, turning him into a over-sized glow-stick!",
                    Traits = new MinionTraits
                    {
                        EyesQuantity = 1,
                        HairStyle = "Flat, center-parted",
                        BodyType = "Slim",
                        Height = 94,
                        Loves = "Playing & Laughing",
                        Hates = "Being bullied by other minions"
                    }
                }
            };

            return minions;
        }
    }
}
