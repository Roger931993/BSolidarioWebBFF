using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.DTOs;

namespace WebBFF.Cliente.Application.Interfaces.Infraestructure
{
    public interface ICentralNegocioService
    {
        Task<ResponseBase<BuscarClientesResponse>> BuscarClientes(Guid IdTraker);
        Task<ResponseBase<BuscarClienteResponse>> BuscarCliente(int cliente_id, Guid IdTraker);
        Task<ResponseBase<BuscarCuentaResponse>> BuscarCuenta(int cuenta_id, Guid IdTraker);        
        Task<ResponseBase<ProyeccionAhorroResponse>> ProyeccionAhorroPlan(int cliente_id, int cuenta_id, Guid IdTraker);
        Task<ResponseBase<CrearClienteResponse>> RegistrarCliente(CrearClienteRequest request, Guid IdTraker);
        Task<ResponseBase<CrearCuentaResponse>> CrearCuenta(CrearCuentaRequest request, Guid IdTraker);
        Task<ResponseBase<CrearCuentaAhorroPlanResponse>> CrearCuentaAhorroPlan(CrearCuentaAhorroPlanRequest request, Guid IdTraker);
        Task<ResponseBase<CrearDepositoResponse>> GenerarDeposito(CrearDepositoRequest request, Guid IdTraker);
        Task<ResponseBase<CrearTransferenciaResponse>> GenerarTransferencia(CrearTransferenciaRequest request, Guid IdTraker);
    }
}
