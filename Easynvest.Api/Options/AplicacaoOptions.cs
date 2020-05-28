using Easynvest.Aplicacao.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Options
{
	public class AplicacaoOptions
	{
		public string Titulo { get; set; }
		public string Versao { get; set; }
		public string Descricao { get; set; }
		public ServicesOptions Services { get; set; }
		public PoliciesOptions Policies { get; set; }
		public CacheOptions Cache { get; set; }

	}
}
