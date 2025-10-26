using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Queries
{
    public  record ConsultarClientesQuery(RequestBase<ConsultarClientesRequest> request) : IRequest<ResponseBase<ConsultarClientesResponse>>;

    public class ConsultarClientesRequest
    {
    }

    public class ConsultarClientesResponse
    {
        public List<clienteDto>? clientes { get; set; }
    }    
}
