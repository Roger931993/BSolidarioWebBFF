using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class CrearCuentaAhorroPlanRequest
    {
        public int? cuenta_id { get; set; }
        public int? cliente_id { get; set; }
        public string? moneda { get; set; }
        public decimal? monto { get; set; }

    }
}
