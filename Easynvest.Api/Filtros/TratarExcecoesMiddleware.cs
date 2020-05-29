using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Easynvest.Api.Filtros
{
    public class TratarExcecoesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public TratarExcecoesMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context, ILogger<TratarExcecoesMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, logger, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, ILogger<TratarExcecoesMiddleware> logger, Exception exception)
        {
            logger.LogError(exception, "Ocorreu um erro na chamada: ", context.Request.Method, context.Request.Path);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionInfo = new Dictionary<string, object>
            {
                ["Mensagem"] = "Ocorreu um erro na sua requisição. Tente novamente mais tarde."
            };

            if (env.IsDevelopment())
            {
                exceptionInfo["Mensagem"] = exception.Message;
                if (exception.InnerException != null)
                {
                    exceptionInfo["DetalhesErro"] = new Dictionary<string, object>
                    {
                        ["Erro"] = exception.InnerException.GetType().Name,
                        ["Detalhes"] = exception.InnerException.Message
                    };
                }
            }

            var result = JsonConvert.SerializeObject(exceptionInfo, JsonSerializerSettingsProvider.CreateSerializerSettings());

            return context.Response.WriteAsync(result);
        }
    }

   
}
