using System;

using Funqy.CSharp;
using RedisWithAspNet4_6.Web.Models;


namespace RedisWithAspNet4_6.Web.App_Core.Commands
{
    public interface IMinionCommands
    {
        FunqResult<Minion> CreateMinion(Minion minion);
        FunqResult DeleteMinion(Guid minionId);
        FunqResult<Minion> UpdateMinion(Minion minion);
    }
}