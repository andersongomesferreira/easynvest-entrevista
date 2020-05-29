using Easynvest.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easynvest.Dominio.Ir
{
	public class CalcularIR : ICalculaIr
	{
		public CalcularIR(decimal valorAtual, decimal capitalInvestido, decimal taxa)
		{
			ValorAtual = valorAtual;
			CapitalInvestido = capitalInvestido;
			Taxa = taxa;
		}

		public decimal ValorAtual { get; private set; }
		public decimal CapitalInvestido { get; private set; }
		public decimal Taxa { get; private set; }

		public decimal CalcularValorIR()
		{
			return (this.ValorAtual - this.CapitalInvestido) * Taxa;
		}
	}
}
