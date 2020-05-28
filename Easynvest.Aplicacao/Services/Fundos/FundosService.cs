using Easynvest.Dominio.Dtos;
using Easynvest.Infra.Cache;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Services
{
	public class FundosService : IFundosService
	{
		private readonly IFundosService _fundosService;
		private readonly ILogger<FundosService> _logger;
		private readonly IRedisClient<ListadeFundos> _redisClient;

		public FundosService(IFundosService fundosService, ILogger<FundosService> logger, IRedisClient<ListadeFundos> redisClient)
		{
			_fundosService = fundosService;
			_logger = logger;
			_redisClient = redisClient;
		}

		public async Task<ListadeFundos> ListarInvestimentosFundos(string id, CancellationToken ct)
		{
			var cache = await _redisClient.RecuperarCache("Fundos-" + id);
			if (cache != null)
			{
				return cache;
			}

			var result = await _fundosService.ListarInvestimentosFundos(id, ct);
			await _redisClient.SalvarCache("Fundos-" + id, result, DateTimeOffset.Now.Add(new TimeSpan(23, 59, 59)));

			return result;
		}
	}
}
