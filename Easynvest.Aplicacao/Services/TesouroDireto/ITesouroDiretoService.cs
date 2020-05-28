using Easynvest.Dominio.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Services
{
	public interface ITesouroDiretoService : IInvestimentosService
	{
		[Get("/{id}")]
		Task<ListadeTesouroDireto> ListarInvestimentosTesouroDireto(string id, CancellationToken ct);
	}
}
