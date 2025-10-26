using MediatR;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Features.Commands
{
    public class GenerarTransferenciaCommand : RequestBase<GenerarTransferenciaRequest>, IRequest<ResponseBase<GenerarTransferenciaResponse>>
    {
    }

    public class GenerarTransferenciaRequest
    {
        public int? cliente_id_origen { get; set; }
        public int? cuenta_id_origen { get; set; }
        public int? cliente_id_destino { get; set; }
        public int? cuenta_id_destino { get; set; }
        public decimal? monto { get; set; }
        public string? motivo { get; set; }
    }
    public class GenerarTransferenciaResponse
    {
        public cuentaDto? cuenta_origen { get; set; }
        public cuentaDto? cuenta_destino { get; set; }
    }
}
