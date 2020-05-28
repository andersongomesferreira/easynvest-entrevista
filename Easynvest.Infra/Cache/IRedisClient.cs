using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easynvest.Infra.Cache
{
	public interface IRedisClient<T>
	{
		Task<T> RecuperarCache(string chave);

		Task SalvarCache(string chave, T valor, DateTimeOffset dateTimeOffset);
	}
}
