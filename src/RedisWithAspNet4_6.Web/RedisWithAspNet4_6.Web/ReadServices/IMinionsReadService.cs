using System;
using System.Collections.Generic;
using RedisWithAspNet4_6.Web.Models;

namespace RedisWithAspNet4_6.Web.ReadServices
{
    public interface IMinionsReadService
    {
        List<Minion> GetAllMinions();
        Minion GetMinion(Guid id);
    }
}