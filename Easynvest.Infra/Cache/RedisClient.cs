using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easynvest.Infra.Cache
{
	public class RedisClient<T> : IRedisClient<T> where T : class
	{
		private readonly IDistributedCache _distributedCache;

		public RedisClient(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task<T> RecuperarCache(string chave)
		{
			var resultadoCache = await _distributedCache.GetStringAsync(chave);
			if (!string.IsNullOrEmpty(resultadoCache))
				return JsonConvert.DeserializeObject<T>(resultadoCache);
			return  null;
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
