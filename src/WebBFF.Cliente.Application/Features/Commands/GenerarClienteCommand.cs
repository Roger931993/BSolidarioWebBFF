using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.DTOs;

namespace WebBFF.Cliente.Application.Features.Commands
{
    public class GenerarClienteCommand : RequestBase<GenerarClienteRequest>, IRequest<ResponseBase<GenerarClienteResponse>>
    {
    }

    public class GenerarClienteRequest
    {
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string? identificacion { get; set; }
        public string? user_name { get; set; }
    }
    public class GenerarClienteResponse
    {
        public clienteDto? cliente { get; set; }
    }
}
