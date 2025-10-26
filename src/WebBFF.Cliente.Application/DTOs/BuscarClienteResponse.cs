namespace WebBFF.Cliente.Application.DTOs
{
    public class BuscarClienteResponse
    {
        public clienteDto? cliente { get; set; }
        public List<cuentaDto>? cuentas { get; set; }
    }
}
