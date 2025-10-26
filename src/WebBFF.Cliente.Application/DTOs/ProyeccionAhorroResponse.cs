using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class ProyeccionAhorroResponse
    {
        public clienteDto? cliente { get; set; }
        public cuentaDto? cuenta { get; set; }
        public List<proyeccionDto>? proyeccion { get; set; }
    }
}
