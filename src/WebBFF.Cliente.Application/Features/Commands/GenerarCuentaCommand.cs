using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Commands
{
    public class GenerarCuentaCommand : RequestBase<GenerarCuentaRequest>, IRequest<ResponseBase<GenerarCuentaResponse>>
    {
    }

    public class GenerarCuentaRequest
    {
        public int? cliente_id { get; set; }
        public int? producto_id { get; set; }
        public string? moneda { get; set; }
        public string? tipo_cuenta { get; set; }
    }
    public class GenerarCuentaResponse
    {
        public cuentaDto? cuenta { get; set; }
    }
}
