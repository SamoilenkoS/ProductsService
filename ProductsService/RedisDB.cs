using StackExchange.Redis;
using System.Threading.Tasks;

namespace ProductsPresentationLayer
{
    public class RedisDB
    {
        public static async Task<string> GetValue(string key)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:5002");

            IDatabase db = redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.ToString();
        }

        public static async Task SetValue(string key, string value)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:5002");

            IDatabase db = redis.GetDatabase();
            await db.StringSetAsync(key, value);
        }
    }
}
