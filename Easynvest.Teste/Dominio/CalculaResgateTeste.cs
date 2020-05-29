using Easynvest.Dominio.Resgate;
using FluentAssertions;
using Xunit;

namespace Easynvest.Teste.Dominio
{
	public class CalculaResgateTeste
	{
		[Theory]
		[InlineData(1000, 700)]
		public void Calcular_Resgate_Taxa_30_Porcento(decimal valorInvestido, decimal valorEsperado)
		{
			CalcularResgate calculaResgate = new CalcularResgate(valorInvestido);
			var valorResgate = calculaResgate.CalcularResgatePadrao();

			valorResgate.Should().Be(valorEsperado);
		}

		[Theory]
		[InlineData(1000, 940)]
		public void Calcular_Resgate_Taxa_6_Porcento(decimal valorInvestido, decimal valorEsperado)
		{
			CalcularResgate calculaResgate = new CalcularResgate(valorInvestido);
			var valorResgate = calculaResgate.CalcularResgateTresMesesVencer();

			valorResgate.Should().Be(valorEsperado);
		}

		[Theory]
		[InlineData(1000, 850)]
		public void Calcular_Resgate_Taxa_15_Porcento(decimal valorInvestido, decimal valorEsperado)
		{
			CalcularResgate calculaResgate = new CalcularResgate(valorInvestido);
			var valorResgate = calculaResgate.CalcularResgateMaisMetadeTempoCustodia();

			valorResgate.Should().Be(valorEsperado);
		}
	}
}
