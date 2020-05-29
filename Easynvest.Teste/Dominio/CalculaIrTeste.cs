
using Easynvest.Dominio.Ir;
using FluentAssertions;
using Xunit;


namespace Easynvest.Teste.Dominio
{
	public class CalculaIrTeste
	{
		[Theory]
		[InlineData(1000, 900, 0.15, 15)]
		[InlineData(1000, 900, 0.05, 5)]
		[InlineData(1000, 900, 0.10, 10)]
		public void Calcular_Valor_IR(decimal valoratual, decimal capitalInvestido, decimal taxa, decimal valorEsperado)
		{
			CalcularIR calcularIR = new CalcularIR(valoratual, capitalInvestido, taxa);

			var valorIR = calcularIR.CalcularValorIR();

			valorIR.Should().Be(valorEsperado);
		}
	}
}
