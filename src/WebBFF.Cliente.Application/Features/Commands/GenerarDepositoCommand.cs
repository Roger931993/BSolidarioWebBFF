using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Commands
{
    public class GenerarDepositoCommand : RequestBase<GenerarDepositoRequest>, IRequest<ResponseBase<GenerarDepositoResponse>>
    {
    }

    public class GenerarDepositoRequest
    {
        public int? cliente_id { get; set; }
        public int? cuenta_id { get; set; }
        public decimal? monto { get; set; }
    }
    public class GenerarDepositoResponse
    {
        public cuentaDto? cuenta { get; set; }
    }
}
