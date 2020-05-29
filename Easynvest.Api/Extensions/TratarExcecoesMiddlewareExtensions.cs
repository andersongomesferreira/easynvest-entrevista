using Easynvest.Api.Filtros;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Extensions
{
    public static class TratarExcecoesMiddlewareExtensions
    {
        public static IApplicationBuilder UseTratarExcecoes(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TratarExcecoesMiddleware>();
        }
    }
}
