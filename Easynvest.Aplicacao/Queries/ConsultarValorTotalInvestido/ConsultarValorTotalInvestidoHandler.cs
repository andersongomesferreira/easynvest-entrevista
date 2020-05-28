using Easynvest.Aplicacao.Options;
using Easynvest.Aplicacao.Services;
using Easynvest.Dominio.Models;
using Easynvest.Infra.Cache;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Queries.ConsultarValorTotalInvestido
{
	public class ConsultarValorTotalInvestidoHandler : IRequestHandler<ConsultarValorTotalInvestidoQuery, ListaInvestimentos>
	{
		private readonly ServicesOptions _servicesOptions;
		private readonly ILogger<ConsultarValorTotalInvestidoHandler> _logger;
		private readonly IFundosService _fundosService;
		private readonly ITesouroDiretoService _tesouroDiretoService;
		private readonly IRendaFixaService _rendaFixaService;
		
		public ConsultarValorTotalInvestidoHandler(IOptionsSnapshot<ServicesOptions> investimentosServiceOption, 
			ILogger<ConsultarValorTotalInvestidoHandler> logger, IFundosService fundosService, 
			ITesouroDiretoService tesouroDiretoService, IRendaFixaService rendaFixaService)
		{
			this._servicesOptions = investimentosServiceOption?.Value;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_fundosService = fundosService ?? throw new ArgumentNullException(nameof(fundosService));
			_tesouroDiretoService = tesouroDiretoService ?? throw new ArgumentNullException(nameof(tesouroDiretoService));
			_rendaFixaService = rendaFixaService ?? throw new ArgumentNullException(nameof(rendaFixaService));
		}

		public async Task<ListaInvestimentos> Handle(ConsultarValorTotalInvestidoQuery request, CancellationToken cancellationToken)
		{
			var fundosServiceResult = await _fundosService.ListarInvestimentosFundos(_servicesOptions.FundosId, cancellationToken);
			var rendaFixaServiceResult = await _rendaFixaService.ListarInvestimentosRendaFixa(_servicesOptions.RendaFixaId, cancellationToken);
			var tesouroDiretoServiceResult = await _tesouroDiretoService.ListarInvestimentosTesouroDireto(_servicesOptions.TesouroDiretoId, cancellationToken);
			
			var result = new ListaInvestimentos();

			List<Investimento> listaInvestimentos = new List<Investimento>();
			listaInvestimentos.AddRange(fundosServiceResult?.fundos);
			listaInvestimentos.AddRange(rendaFixaServiceResult?.lcis);
			listaInvestimentos.AddRange(tesouroDiretoServiceResult?.tds);

			result.Investimentos.AddRange(listaInvestimentos);

			result.ValorTotal = listaInvestimentos.Sum(t => t.ValorResgate);

			return result;
		}
	}
}
