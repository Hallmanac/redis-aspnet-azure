using System;
using System.Collections.Generic;

using RedisWithAspNet4_6.Web.Models;


namespace RedisWithAspNet4_6.Web.App_Core.ReadServices
{
    public interface IMinionsReadService
    {
        List<Minion> GetAllMinions();
        Minion GetMinion(Guid id);
    }
}