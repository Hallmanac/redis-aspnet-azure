using System.Collections.Generic;

using RedisOnAzure.Web.Models;

using RedisRepo.Src;


namespace RedisOnAzure.Web.App_Common
{
    public static class AppUserCacheExtensions
    {
        public static void AddUserIndices(this CacheRepo<ApplicationUser> @this)
        {
            @this.CustomIndexFormatters.Add(arg => new KeyValuePair<string, string>(@this.UsernameIndex(), arg.UserName));
            @this.CustomIndexFormatters.Add(arg => new KeyValuePair<string, string>(@this.EmailIndex(), arg.Email));
        }


        public static string UsernameIndex(this CacheRepo<ApplicationUser> @this)
        {
            return "User:UsernameIndex";
        }


        public static string EmailIndex(this CacheRepo<ApplicationUser> @this)
        {
            return "User:EmailIndex";
        }
    }
}