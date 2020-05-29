using Easynvest.Dominio.Interfaces;
using Easynvest.Dominio.Ir;
using Easynvest.Dominio.Models;
using Easynvest.Dominio.Resgate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easynvest.Dominio.Models
{
	public class RendaFixa : Investimento
	{
		[JsonProperty("capitalInvestido")]
		public decimal CapitalInvestido { get; set; }

		[JsonProperty("capitalAtual")]
		public decimal CapitalAtual { get; set; }

		[JsonProperty("quantidade")]
		public	decimal Quantidade { get; set; }

		[JsonProperty("vencimento")]
		public DateTime Vencimento { get; set; }

		[JsonProperty("iof")]
		public decimal Iof { get; set; }

		[JsonProperty("outrasTaxas")]
		public decimal OutrasTaxas { get; set; }

		[JsonProperty("taxas")]
		public decimal Taxas { get; set; }

		[JsonProperty("indice")]
		public string Indice { get; set; }

		[JsonProperty("tipo")]
		public string Tipo { get; set; }

		[JsonProperty("nome")]
		public string Nome { get; set; }

		[JsonProperty("guarantidoFGC")]
		public string GuarantidoFGC { get; set; }

		[JsonProperty("dataOperacao")]
		public DateTime DataOperacao { get; set; }

		[JsonProperty("precoUnitario")]
		public decimal PrecoUnitario { get; set; }

		[JsonProperty("primario")]
		public bool Primario { get; set; }

		public override decimal CalcularValorIr()
		{
			return new CalcularIR(this.CapitalAtual, this.CapitalInvestido, 0.05M).CalcularValorIR();
		}

		public override decimal CalcularValorResgate()
		{
			return new CalcularResgate(Vencimento, DataOperacao, CapitalInvestido).CalcularValorResgate();
		}
	}
}
