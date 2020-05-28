using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easynvest.Api.Controllers;
using Easynvest.Aplicacao.Queries.ConsultarValorTotalInvestido;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Easynvest.Api.Controllers
{
    public class InvestimentosController : BaseController
    {
        [HttpGet("ConsultarValorTotalInvestido")]
        public async Task<IActionResult> ConsultarValorTotalInvestido()
        {
            var resultado = await Mediator.Send(new ConsultarValorTotalInvestidoQuery());
            return Ok(resultado);
        }

    }
}
