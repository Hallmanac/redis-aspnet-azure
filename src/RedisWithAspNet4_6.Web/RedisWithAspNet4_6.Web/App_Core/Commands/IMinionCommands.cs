using System;

using Funqy.CSharp;
using RedisWithAspNet4_6.Web.Models;
using System.Threading.Tasks;

namespace RedisWithAspNet4_6.Web.App_Core.Commands
{
    public interface IMinionCommands
    {
        Task<FunqResult<Minion>> CreateMinionAsync(Minion minion);

        Task<FunqResult> DeleteMinionAsync(Guid minionId);

        Task<FunqResult<Minion>> UpdateMinionAsync(Minion minion);
    }
}