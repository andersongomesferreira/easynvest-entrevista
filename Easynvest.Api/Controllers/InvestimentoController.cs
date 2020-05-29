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
        private readonly ILogger<InvestimentosController> logger;
        public InvestimentosController(ILogger<InvestimentosController> logger) 
        {
            this.logger = logger;
        }

        [HttpGet("ConsultarValorTotalInvestido")]
        public async Task<IActionResult> ConsultarValorTotalInvestido()
        {
            logger.LogInformation("ConsultarValorTotalInvestido - Consulta iniciada");
            var resultado = await Mediator.Send(new ConsultarValorTotalInvestidoQuery());
            if(resultado == null)
            {
                logger.LogWarning("ConsultarValorTotalInvestido - Nenhum valor foi retornado do serviço");
                return NotFound();
            }

            return Ok(resultado);
        }

    }
}
