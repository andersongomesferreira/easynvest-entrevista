using Easynvest.Dominio.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Services
{
	public interface IRendaFixaService : IInvestimentosService
	{
		[Get("/{id}")]
		Task<ListadeRendaFixa> ListarInvestimentosRendaFixa(string id, CancellationToken ct);
	}
}
