using Funqy.CSharp;
using RedisWithAspNet4_6.Web.Models;
using RedisWithAspNet4_6.Web.ReadServices;
using System;

namespace RedisWithAspNet4_6.Web.Commands
{
    public class MinionCommands : IMinionCommands
    {
        public readonly IMinionsReadService _minionReadSvc;

        public MinionCommands(IMinionsReadService minionsReadSvc)
        {
            _minionReadSvc = minionsReadSvc;
        }

        public FunqResult<Minion> CreateMinion(Minion minion)
        {
            return FunqFactory.KeepGroovin(minion, "Succesfully added minion");
        }

        public FunqResult<Minion> UpdateMinion(Minion minion)
        {
            var dbMinion = _minionReadSvc.GetMinion(minion.Id);
            if(dbMinion == null)
            {
                return FunqFactory.Fail("Unable to find existing minion to update", (Minion)null);
            }

            dbMinion.MovieMoments = minion.MovieMoments;
            dbMinion.Name = minion.Name;
            dbMinion.Nickname = minion.Nickname;
            dbMinion.Traits = minion.Traits;

            //TODO: Save updated minion to a data store
            return FunqFactory.KeepGroovin<Minion>(dbMinion, "Successfully updated minion");
        }


        public FunqResult DeleteMinion(Guid minionId)
        {
            //TODO: Delete the minion from the datastore
            return FunqFactory.KeepGroovin("Deleted the minion");
        }
    }
}
