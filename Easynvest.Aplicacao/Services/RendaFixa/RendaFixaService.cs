using Easynvest.Dominio.Dtos;
using Easynvest.Dominio.Models;
using Easynvest.Infra.Cache;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Services
{
	public class RendaFixaService : IRendaFixaService
	{
		private readonly IRendaFixaService _rendaFixaService;
		private readonly ILogger<RendaFixaService> _logger;
		private readonly IRedisClient<ListadeRendaFixa> _redisClient;

		public RendaFixaService(IRendaFixaService RendaFixaService, ILogger<RendaFixaService> logger, IRedisClient<ListadeRendaFixa> redisClient)
		{
			_rendaFixaService = RendaFixaService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
		}

		public async Task<ListadeRendaFixa> ListarInvestimentosRendaFixa(string id, CancellationToken ct)
		{
			var cache = await _redisClient.RecuperarCache("RendaFixa-" + id);
			if (cache != null)
			{
				return cache;
			}

			var result = await _rendaFixaService.ListarInvestimentosRendaFixa(id, ct);
			await _redisClient.SalvarCache("RendaFixa-" + id, result, DateTimeOffset.Now.Add(new TimeSpan(23, 59, 59)));

			return result;
		}
	}
}
