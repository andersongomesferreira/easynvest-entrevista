using Easynvest.Api.Extensions;
using Easynvest.Api.Options;
using Easynvest.Aplicacao.Options;
using Easynvest.Aplicacao.Queries.ConsultarValorTotalInvestido;
using Easynvest.Infra.Cache;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Easynvest.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//Carregar as policies da aplicação
			AplicacaoOptions appOptions = new AplicacaoOptions();
			Configuration.GetSection("Aplicacao").Bind(appOptions);

			//Injetar a classe ServicesOptions com os valores de configurações
			services.Configure<ServicesOptions>(options => Configuration.GetSection("Aplicacao:Services").Bind(options));

			services.AddMediatR(typeof(ConsultarValorTotalInvestidoQuery).Assembly);

			//Configurar a biblioteca Refit para as chamadas dos serviços
			services.ConfigurarRefit(appOptions.Policies, appOptions.Services.UrlBase);

			// Carregar o Swagger
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc(appOptions.Versao, new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = appOptions.Titulo,
					Version = appOptions.Versao,
					Description = appOptions.Descricao
				});
			});

			//Configurar o redis
			//var redis = ConnectionMultiplexer.Connect(appOptions.Cache.ConexaoRedis);
			//services.AddScoped(r => redis.GetDatabase());

			services.AddDistributedRedisCache(options =>
			{
				options.Configuration = appOptions.Cache.ConexaoRedis;
				options.InstanceName = "Easynvest_RedisCache";
			});


			services.AddTransient(typeof(IRedisClient<>), typeof(RedisClient<>));

			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["Aplicacao:Titulo"]));
		}
	}
}
