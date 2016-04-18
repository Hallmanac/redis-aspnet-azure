using System;
using System.Collections.Generic;
using System.Linq;

using RedisOnAzure.Web.Models;

using RedisRepo.Src;


namespace RedisOnAzure.Web.App_Cache
{
    public class UserCache : CacheRepo<ApplicationUser>
    {
        public UserCache(IAppCache appCache) : base(appCache){ }


        public string UsernameIndex => "User:UsernameIndex";

        public string EmailIndex => "User:EmailIndex";


        public override void SetCustomCacheIndices(ApplicationUser entity)
        {
            //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // -- The method on the base class does nothing                  \\
            // -- This method gets called from inside the CacheRepo<T> class \\
            //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            
            CustomIndexFormatters.Add(arg => new KeyValuePair<string, string>(UsernameIndex, arg.UserName));
            CustomIndexFormatters.Add(arg => new KeyValuePair<string, string>(EmailIndex, arg.Email));
            CustomIndexFormatters.AddRange(LoginsIndexDefinition(entity));
        }


        private static string LoginsIndex(ApplicationUser user)
        {
            return $"User:LoginsIndex:{user.UserName}";
        }


        private static IEnumerable<Func<ApplicationUser, KeyValuePair<string, string>>> LoginsIndexDefinition(ApplicationUser user)
        {
            // Iterate through the logins list and generate an index for each item
            var indexDefinitions = user
                .Logins
                .Select(login => new Func<ApplicationUser, KeyValuePair<string, string>>(
                    x => new KeyValuePair<string, string>(LoginsIndex(user), login.UserId))
                )
                .ToList();
            return indexDefinitions;
        }
    }
}