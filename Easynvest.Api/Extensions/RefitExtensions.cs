using Easynvest.Api.Options;
using Easynvest.Aplicacao.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Easynvest.Api.Extensions
{
	public static class RefitExtensions
	{
		public static IServiceCollection ConfigurarRefit(this IServiceCollection services, PoliciesOptions policies, string urlBase)
		{
			//Definindo policies de Retry e timeout
			AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
			.HandleTransientHttpError()
			.WaitAndRetryAsync(policies.WaitAndRetryConfig.Retry, _ => TimeSpan.FromMilliseconds(policies.WaitAndRetryConfig.Wait));

			AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy = Policy
			  .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(policies.WaitAndRetryConfig.Timeout));

			//Adicionar o client do Refit nas interfaces de serviços
			services.AddRefitClient<IRendaFixaService>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(urlBase))
				.AddPolicyHandler(retryPolicy)
				.AddPolicyHandler(timeoutPolicy);
			services.Decorate<IRendaFixaService, RendaFixaService>();

			services.AddRefitClient<IFundosService>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(urlBase))
				.AddPolicyHandler(retryPolicy)
				.AddPolicyHandler(timeoutPolicy);
			services.Decorate<IFundosService, FundosService>();

			services.AddRefitClient<ITesouroDiretoService>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(urlBase))
				.AddPolicyHandler(retryPolicy)
				.AddPolicyHandler(timeoutPolicy);
			services.Decorate<ITesouroDiretoService, TesouroDiretoService>();

			return services;
		}
	}
}
