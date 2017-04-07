using System;
using System.Collections.Generic;

using RedisWithAspNet4_6.Web.Models;
using System.Threading.Tasks;

namespace RedisWithAspNet4_6.Web.App_Core.ReadServices
{
    public interface IMinionsReadService
    {
        Task<List<Minion>> GetAllMinionsAsync();
        Task<Minion> GetMinionAsync(Guid id);
    }
}