using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easynvest.Dominio.Models
{
	public abstract class Investimento
	{
		[JsonProperty("ir")]
		public decimal Ir => CalcularValorIr();

		[JsonProperty("valorResgate")]
		public decimal ValorResgate => CalcularValorResgate();

		public abstract decimal CalcularValorIr();
		public abstract decimal CalcularValorResgate();

	}
}
