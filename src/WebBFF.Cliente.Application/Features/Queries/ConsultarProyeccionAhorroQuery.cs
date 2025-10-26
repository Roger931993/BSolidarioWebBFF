using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Queries
{
    public record ConsultarProyeccionAhorroQuery(RequestBase<ConsultarProyeccionAhorroRequest> request) : IRequest<ResponseBase<ConsultarProyeccionAhorroResponse>>;

    public class ConsultarProyeccionAhorroRequest
    {
        public int? cuenta_id { get; set; }
        public int? cliente_id { get; set; }

    }

    public class ConsultarProyeccionAhorroResponse
    {
        public cuentaDto? cuenta { get; set; }
        public clienteDto? cliente { get; set; }
        public List<proyeccionDto>? proyeccion { get; set; }
    }
}
