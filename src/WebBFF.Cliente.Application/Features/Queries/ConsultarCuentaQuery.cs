using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Queries
{
    public record ConsultarCuentaQuery(RequestBase<ConsultarCuentaRequest> request) : IRequest<ResponseBase<ConsultarCuentaResponse>>;

    public class ConsultarCuentaRequest
    {
        public int? cuenta_id { get; set; }

    }

    public class ConsultarCuentaResponse
    {
        public cuentaDto? cuenta { get; set; }
    }
}
