using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Queries
{
    public record ConsultaClienteCuentasQuery(RequestBase<ConsultaClienteCuentasRequest> request) : IRequest<ResponseBase<ConsultaClienteCuentasResponse>>;

    public class ConsultaClienteCuentasRequest
    {
        public int? client_id { get; set; }
    }

    public class ConsultaClienteCuentasResponse
    {
        public clienteDto? cliente { get; set; }
        public List<cuentaDto>? cuentas { get; set; }
    }
}
