using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class CrearCuentaRequest
    {
        public int? cliente_id { get; set; }
        public int? producto_id { get; set; }
        public string? moneda { get; set; }
        public string? tipo_cuenta { get; set; }

    }
}
