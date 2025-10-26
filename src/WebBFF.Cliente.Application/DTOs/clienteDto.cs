using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBFF.Cliente.Application.DTOs
{
    public class clienteDto
    {
        public int? cliente_id { get; set; }
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string? identificacion { get; set; }
        public string? username { get; set; }
        public List<contactoDto>? contacto { get; set; }
    }
}
