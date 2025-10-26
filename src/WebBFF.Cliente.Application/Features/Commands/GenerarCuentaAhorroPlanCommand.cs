using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Commands
{
    public class GenerarCuentaAhorroPlanCommand : RequestBase<GenerarCuentaAhorroPlanRequest>, IRequest<ResponseBase<GenerarCuentaAhorroPlanResponse>>
    {
    }

    public class GenerarCuentaAhorroPlanRequest
    {
        public int? cuenta_id { get; set; }
        public int? cliente_id { get; set; }
        public string? moneda { get; set; }
        public decimal? monto { get; set; }

    }
    public class GenerarCuentaAhorroPlanResponse
    {
        public cuentaDto? cuenta { get; set; }
    }
}
