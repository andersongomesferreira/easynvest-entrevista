using Easynvest.Dominio.Interfaces;
using Easynvest.Dominio.Models;
using Easynvest.Dominio.Resgate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easynvest.Dominio.Models
{
	public class TesouroDireto : Investimento
	{
		[JsonProperty("valorInvestido")]
		public decimal ValorInvestido { get; private set; }
		
		[JsonProperty("valorTotal")]
		public decimal ValorTotal { get; private set; }

		[JsonProperty("vencimento")]
		public DateTime Vencimento { get; private set; }

		[JsonProperty("dataDeCompra")]
		public DateTime DataDeCompra { get; private set; }

		[JsonProperty("iof")]
		public decimal Iof { get; private set; }

		[JsonProperty("indice")]
		public string Indice { get; private set; }

		[JsonProperty("tipo")]
		public string Tipo { get; private set; }

		[JsonProperty("nome")]
		public string Nome { get; private set; }

		public override decimal CalcularValorIr()
		{
			return (this.ValorTotal - this.ValorInvestido) * 0.15M;
		}

		public override decimal CalcularValorResgate()
		{
			return new CalculaResgate(Vencimento, DataDeCompra, ValorInvestido).CalcularValorResgate();
		}
	}
}
