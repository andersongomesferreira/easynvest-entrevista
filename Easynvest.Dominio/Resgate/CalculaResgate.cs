using Easynvest.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easynvest.Dominio.Resgate
{
	public class CalculaResgate : ICalculaResgate
	{
		public DateTime DataVencimento { get; private set; }

		public DateTime DataCompra { get; private set; }

		public decimal ValorInvestido { get; private set; }


		public CalculaResgate(DateTime dataVencimento, DateTime dataCompra, decimal valorInvestido)
		{
			DataVencimento = dataVencimento;
			DataCompra = dataCompra;
			ValorInvestido = valorInvestido;
		}

		public decimal CalcularValorResgate()
		{
			TimeSpan totalDias = DataVencimento.Subtract(DataCompra);
			TimeSpan diferencaDiasResgate = DataVencimento.Subtract(DateTime.Now);

			if (diferencaDiasResgate >= (totalDias * 0.5))
			{
				return CalcularResgateMaisMetadeTempoCustodia();
			}
			else if (diferencaDiasResgate.Days <= 90)
			{
				return CalcularResgateTresMesesVencer();
			}
			else
			{
				return CalcularResgatePadrao();
			}
		}

		private decimal CalcularResgateMaisMetadeTempoCustodia()
		{
			return ValorInvestido - (ValorInvestido * 0.15M);
		}

		private decimal CalcularResgateTresMesesVencer()
		{
			return ValorInvestido - (ValorInvestido * 0.06M);
		}
		private decimal CalcularResgatePadrao()
		{
			return ValorInvestido - (ValorInvestido * 0.3M);
		}
	}
}
