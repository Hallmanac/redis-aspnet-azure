using System;

using Funqy.CSharp;
using RedisWithAspNet4_6.Web.App_Core.ReadServices;
using RedisWithAspNet4_6.Web.Models;
using System.Threading.Tasks;
using RedisWithAspNet4_6.Web.App_Core.RedisServices;

namespace RedisWithAspNet4_6.Web.App_Core.Commands
{
    public class MinionCommands : IMinionCommands
    {
        private readonly IMinionsReadService _minionReadSvc;
        private readonly IAppCache _appCache;

        public MinionCommands(IMinionsReadService minionsReadSvc, IAppCache appCache)
        {
            _appCache = appCache;
            _minionReadSvc = minionsReadSvc;
        }

        public async Task<FunqResult<Minion>> CreateMinionAsync(Minion minion)
        {
            await _appCache.AddOrUpdateAsync(minion.Id.ToString(), minion, null, typeof(Minion).Name).ConfigureAwait(false);
            return FunqFactory.KeepGroovin(minion, "Succesfully added minion");
        }

        public async Task<FunqResult<Minion>> UpdateMinionAsync(Minion minion)
        {
            var dbMinion = await _minionReadSvc.GetMinionAsync(minion.Id).ConfigureAwait(false);
            if(dbMinion == null)
            {
                return FunqFactory.Fail("Unable to find existing minion to update", (Minion)null);
            }

            dbMinion.MovieMoments = minion.MovieMoments;
            dbMinion.Name = minion.Name;
            dbMinion.Nickname = minion.Nickname;
            dbMinion.Traits = minion.Traits;

            await _appCache.AddOrUpdateAsync(dbMinion.Id.ToString(), dbMinion, null, typeof(Minion).Name).ConfigureAwait(false);
            return FunqFactory.KeepGroovin(dbMinion, "Successfully updated minion");
        }


        public async Task<FunqResult> DeleteMinionAsync(Guid minionId)
        {
            await _appCache.RemoveAsync(minionId.ToString(), typeof(Minion).Name);
            return FunqFactory.KeepGroovin("Deleted the minion");
        }
    }
}
