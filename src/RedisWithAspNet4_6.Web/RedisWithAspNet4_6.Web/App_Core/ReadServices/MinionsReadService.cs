using System;
using System.Collections.Generic;
using System.Linq;

using RedisWithAspNet4_6.Web.Models;
using RedisWithAspNet4_6.Web.App_Core.RedisServices;
using System.Threading.Tasks;

namespace RedisWithAspNet4_6.Web.App_Core.ReadServices
{
    public class MinionsReadService : IMinionsReadService
    {
        private List<Minion> _allMinions;
        private readonly IAppCache _appCache;

        public MinionsReadService(IAppCache appCache)
        {
            _appCache = appCache;
        }

        public async Task<List<Minion>> GetAllMinionsAsync()
        {
            _allMinions = await _appCache.GetAllItemsInPartitionAsync<Minion>(typeof(Minion).Name).ConfigureAwait(false);
            if (_allMinions != null && _allMinions.Count > 0)
                return _allMinions.OrderBy(m => m.Name).ToList();
            _allMinions = await GenerateMinionsAsync().ConfigureAwait(false);
            return _allMinions.OrderBy(m => m.Name).ToList();
        }

        public async Task<Minion> GetMinionAsync(Guid id)
        {
            if(_allMinions == null)
            {
                _allMinions = await GetAllMinionsAsync().ConfigureAwait(false);
            }
            return _allMinions.FirstOrDefault(m => m.Id == id);
        }

        private async Task<List<Minion>> GenerateMinionsAsync()
        {
            var cachedMinions = await _appCache.GetAllItemsInPartitionAsync<Minion>(typeof(Minion).Name).ConfigureAwait(false);
            if (cachedMinions != null && cachedMinions.Count > 0)
                return cachedMinions;

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

            // Add the minions to the cache
            foreach (var minion in minions)
            {
                await _appCache.AddOrUpdateAsync(minion.Id.ToString(), minion, null, typeof(Minion).Name).ConfigureAwait(false);
            }

            return minions;
        }
    }
}
