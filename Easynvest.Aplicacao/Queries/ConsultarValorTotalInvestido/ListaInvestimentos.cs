using Easynvest.Dominio.Dtos;
using Easynvest.Dominio.Interfaces;
using Easynvest.Dominio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Aplicacao.Queries.ConsultarValorTotalInvestido
{
	public class ListaInvestimentos
	{
		public ListaInvestimentos()
		{
			this.Investimentos = new List<dynamic>();
		}

		[JsonProperty("valorTotal")]
		public decimal ValorTotal { get; set; }

		[JsonProperty("investimentos")]
		public List<dynamic> Investimentos { get; private set; }		

	}
}
