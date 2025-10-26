namespace WebBFF.Cliente.Application.DTOs
{
    public class CrearClienteRequest
    {
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string? identificacion { get; set; }
        public string? user_name { get; set; }
    }
}
