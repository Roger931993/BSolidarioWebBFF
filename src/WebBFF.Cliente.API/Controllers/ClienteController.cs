using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Features.Commands;
using WebBFF.Cliente.Application.Features.Queries;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;

namespace WebBFF.Cliente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : CommonController
    {
        public ClienteController(IMediator mediator, IMemoryCacheLocalService memoryCacheLocalService, IRedisCache redisCache) : base(mediator, memoryCacheLocalService, redisCache)
        {
        }

        [HttpGet("clientes")]        
        [ProducesResponseType(typeof(ConsultarClientesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ConsultarClientesResponse>> BuscarClientes()
        {
            RequestBase<ConsultarClientesRequest> request = new RequestBase<ConsultarClientesRequest>()
            {
                Request = new ConsultarClientesRequest()
                {
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<ConsultarClientesResponse> objResponse = await _mediator.Send(new ConsultarClientesQuery(request));
            return OkUrban(objResponse);
        }

        [HttpGet("cliente/{id}/cuentas")]
        [ProducesResponseType(typeof(ConsultaClienteCuentasResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ConsultaClienteCuentasResponse>> BuscarClienteCuentas(int id)
        {
            RequestBase<ConsultaClienteCuentasRequest> request = new RequestBase<ConsultaClienteCuentasRequest>()
            {
                Request = new ConsultaClienteCuentasRequest()
                {
                    client_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<ConsultaClienteCuentasResponse> objResponse = await _mediator.Send(new ConsultaClienteCuentasQuery(request));
            return OkUrban(objResponse);
        }

        [HttpGet("cuenta/{id}")]
        [ProducesResponseType(typeof(ConsultarCuentaResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ConsultarCuentaResponse>> BuscarCuenta(int id)
        {
            RequestBase<ConsultarCuentaRequest> request = new RequestBase<ConsultarCuentaRequest>()
            {
                Request = new ConsultarCuentaRequest()
                {
                    cuenta_id = id                   
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<ConsultarCuentaResponse> objResponse = await _mediator.Send(new ConsultarCuentaQuery(request));
            return OkUrban(objResponse);
        }

        [HttpGet("cliente/{id}/cuenta/{id1}/proyeccion-ahorro-plan")]
        [ProducesResponseType(typeof(ConsultarProyeccionAhorroResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ConsultarProyeccionAhorroResponse>> BuscarProyeccionAhorroPlan(int id, int id1)
        {
            RequestBase<ConsultarProyeccionAhorroRequest> request = new RequestBase<ConsultarProyeccionAhorroRequest>()
            {
                Request = new ConsultarProyeccionAhorroRequest()
                {
                    cliente_id = id,
                    cuenta_id=id1,
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<ConsultarProyeccionAhorroResponse> objResponse = await _mediator.Send(new ConsultarProyeccionAhorroQuery(request));
            return OkUrban(objResponse);
        }
        
        [HttpPost("cuenta")]
        [ProducesResponseType(typeof(GenerarCuentaResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenerarCuentaResponse>> CrearCuentaAhorroPlan([FromBody] GenerarCuentaRequest data)
        {
            GenerarCuentaCommand command = new GenerarCuentaCommand()
            {
                Request = data
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<GenerarCuentaResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }

        [HttpPost("cuenta-ahorro-plan")]        
        [ProducesResponseType(typeof(GenerarCuentaAhorroPlanResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenerarCuentaAhorroPlanResponse>> CrearCuentaAhorroPlan([FromBody] GenerarCuentaAhorroPlanRequest data)
        {
            GenerarCuentaAhorroPlanCommand command = new GenerarCuentaAhorroPlanCommand()
            {
                Request = data
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<GenerarCuentaAhorroPlanResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }
    }
}
