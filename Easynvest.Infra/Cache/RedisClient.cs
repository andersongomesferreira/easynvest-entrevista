using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easynvest.Infra.Cache
{	
	public class RedisClient<T> : IRedisClient<T>
	{
		private readonly IDistributedCache _distributedCache;

		public RedisClient(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task<T> RecuperarCache(string chave)
		{
			return JsonConvert.DeserializeObject<T>(await _distributedCache.GetStringAsync(chave));
		}

		public async Task SalvarCache(string chave, T valor, DateTimeOffset dateTimeOffset)
		{
			await _distributedCache.SetStringAsync(chave, JsonConvert.SerializeObject(valor), new DistributedCacheEntryOptions()
			{
				AbsoluteExpiration = dateTimeOffset
			});
		}
	}
}
