using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LocacaoVeiculosApi.Domain.Entities;
using LocacaoVeiculosApi.Presentation.ViewModel;
using LocacaoVeiculosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocacaoVeiculosApi.Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AgendamentoController : Controller
    {
        private readonly AgendamentoService _agendamentoService;
        private readonly IMapper _mapper;
        
        public AgendamentoController(AgendamentoService agendamentoService, IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Simular")]
        public async Task<IActionResult> Simular([FromQuery] CalcularLocacaoInput calcularLocacaoInput)
        {
            var result = await _agendamentoService.Simular(calcularLocacaoInput);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
        
            // return Ok(result.Entity);
            return StatusCode(200, result.Entity);
        }

        [HttpPost]
        [Route("Agendar")]
        public async Task<IActionResult> Alugar([FromBody] CalcularLocacaoInput input)
        {
            var result = await _agendamentoService.Alugar(input);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var agendamentoDto = _mapper.Map<Agendamento, AgendamentoDto>((Agendamento)result.Entity);
            return StatusCode(200, agendamentoDto);
            // return Ok(result.Entity);
        }
        

        [HttpPost]
        [Route(("Devolver"))]
        public async Task<IActionResult> Devolver([FromBody] DevolucaoInput input)
        {
            var result = await _agendamentoService.Devolver(input);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            return StatusCode(201, result.Entity);
        }
        
        [HttpGet]
        [Route(("Locacoes/Clientes/{cpf}"))]
        public async Task<IEnumerable<AgendamentoDto>> Consultar(string cpf)
        {
            var agendamentos = await _agendamentoService.ConsultarPorCpf(cpf);
            return _mapper.Map<IEnumerable<Agendamento>, IEnumerable<AgendamentoDto>>(agendamentos);
        }
    }
}