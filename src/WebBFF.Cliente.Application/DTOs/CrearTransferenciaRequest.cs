using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class CrearTransferenciaRequest
    {
        public int? cliente_id_origen { get; set; }
        public int? cuenta_id_origen { get; set; }
        public int? cliente_id_destino { get; set; }
        public int? cuenta_id_destino { get; set; }
        public decimal? monto { get; set; }
        public string? motivo { get; set; }

    }
}
