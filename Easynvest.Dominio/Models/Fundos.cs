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
	public class Fundos : Investimento
	{
		[JsonProperty("capitalInvestido")]
		public decimal CapitalInvestido { get; set; }

		[JsonProperty("ValorAtual")]
		public decimal ValorAtual { get; set; }

		[JsonProperty("dataResgate")]
		public DateTime DataResgate { get; set; }

		[JsonProperty("dataCompra")]
		public DateTime DataCompra { get; set; }

		[JsonProperty("iof")]
		public decimal Iof { get; set; }

		[JsonProperty("nome")]
		public string Nome { get; set; }

		[JsonProperty("totalTaxas")]
		public decimal TotalTaxas { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }
		
		public override decimal CalcularValorIr()
		{
			return new CalcularIR(this.ValorAtual, this.CapitalInvestido, 0.15M).CalcularValorIR();
		}

		public override decimal CalcularValorResgate()
		{
			return new CalcularResgate(DataResgate, DataCompra, CapitalInvestido).CalcularValorResgate();
		}
	}
}
