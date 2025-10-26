using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class CrearTransferenciaResponse
    {
        public cuentaDto? cuenta_origen { get; set; }
        public cuentaDto? cuenta_destino { get; set; }
    }
}
