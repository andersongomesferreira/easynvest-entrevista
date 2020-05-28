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
	public class TesouroDiretoService : ITesouroDiretoService
	{
		private readonly ITesouroDiretoService _tesouroDiretoService;
		private readonly ILogger<TesouroDiretoService> _logger;
		private readonly IRedisClient<ListadeTesouroDireto> _redisClient;

		public TesouroDiretoService(ITesouroDiretoService tesouroDiretoService, ILogger<TesouroDiretoService> logger, IRedisClient<ListadeTesouroDireto> redisClient)
		{
			_tesouroDiretoService = tesouroDiretoService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_redisClient = redisClient;
		}
		
		public async Task<ListadeTesouroDireto> ListarInvestimentosTesouroDireto(string id, CancellationToken ct)
		{
			var cache = await _redisClient.RecuperarCache("TesouroDireto-" + id);
			if (cache != null)
			{
				return cache;
			}

			var result = await _tesouroDiretoService.ListarInvestimentosTesouroDireto(id, ct);
			await _redisClient.SalvarCache("TesouroDireto-" + id, result, DateTimeOffset.Now.Add(new TimeSpan(23, 59, 59)));

			return result;
		}
	}
}
