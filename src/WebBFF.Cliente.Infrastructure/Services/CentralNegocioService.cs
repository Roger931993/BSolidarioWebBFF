using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using WebBFF.Cliente.Domain.Common;
using WebBFF.Cliente.Domain.Helpers;
using WebBFF.Cliente.Infrastructure.Services.Common;
using WebBFF.Cliente.Infrastructure.Services.Common.Api;
using static WebBFF.Cliente.Model.Entity.EnumTypes;

namespace WebBFF.Cliente.Infrastructure.Services
{
    public class CentralNegocioService: BaseService, ICentralNegocioService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCacheLocalService _memoryCacheLocalService;
        private readonly IApiUrl _apiUrl;
        public CentralNegocioService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IMemoryCacheLocalService memoryCacheLocalService, ApiConnectionDto apiConnectionDto)
        {
            this._httpClient = httpClient;
            this._httpContextAccessor = httpContextAccessor;
            this._memoryCacheLocalService = memoryCacheLocalService;
            this._apiUrl = apiConnectionDto.Values!["CentralNegocio"];
        }

        public async Task<ResponseBase<BuscarClientesResponse>> BuscarClientes(Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl.Url}/api/Operacion/clientes");
                ResponseBase<BuscarClientesResponse> resultService = await Util.ConvertResponse<ResponseBase<BuscarClientesResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(string.Empty, 500, ex, cachelocal);
                return new ResponseBase<BuscarClientesResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(string.Empty, 200, cachelocal);
            }
        }

        public async Task<ResponseBase<BuscarClienteResponse>> BuscarCliente(int cliente_id, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl.Url}/api/Operacion/cliente/{cliente_id}");
                ResponseBase<BuscarClienteResponse> resultService = await Util.ConvertResponse<ResponseBase<BuscarClienteResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(cliente_id.ToString(), 500, ex, cachelocal);
                return new ResponseBase<BuscarClienteResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(cliente_id.ToString(), 200, cachelocal);
            }
        }

        public async Task<ResponseBase<BuscarCuentaResponse>> BuscarCuenta(int cuenta_id, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl.Url}/api/Operacion/cuenta/{cuenta_id}");
                ResponseBase<BuscarCuentaResponse> resultService = await Util.ConvertResponse<ResponseBase<BuscarCuentaResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(cuenta_id.ToString(), 500, ex, cachelocal);
                return new ResponseBase<BuscarCuentaResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(cuenta_id.ToString(), 200, cachelocal);
            }
        }

        public async Task<ResponseBase<ProyeccionAhorroResponse>> ProyeccionAhorroPlan(int cliente_id,int cuenta_id, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl.Url}/api/Operacion/cliente/{cliente_id}/cuenta/{cuenta_id}");
                ResponseBase<ProyeccionAhorroResponse> resultService = await Util.ConvertResponse<ResponseBase<ProyeccionAhorroResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(cliente_id.ToString(), 500, ex, cachelocal);
                return new ResponseBase<ProyeccionAhorroResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(cliente_id.ToString(), 200, cachelocal);
            }
        }

        public async Task<ResponseBase<CrearClienteResponse>> RegistrarCliente(CrearClienteRequest request, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiUrl.Url}/api/Operacion/registrar-cliente", content);
                ResponseBase<CrearClienteResponse> resultService = await Util.ConvertResponse<ResponseBase<CrearClienteResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(request.ToString(), 500, ex, cachelocal);
                return new ResponseBase<CrearClienteResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(request.ToString(), 200, cachelocal);
            }
        }

       

        public async Task<ResponseBase<CrearCuentaResponse>> CrearCuenta(CrearCuentaRequest request, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiUrl.Url}/api/Operacion/crear-cuenta", content);
                ResponseBase<CrearCuentaResponse> resultService = await Util.ConvertResponse<ResponseBase<CrearCuentaResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(request.ToString(), 500, ex, cachelocal);
                return new ResponseBase<CrearCuentaResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(request.ToString(), 200, cachelocal);
            }
        }

        public async Task<ResponseBase<CrearCuentaAhorroPlanResponse>> CrearCuentaAhorroPlan(CrearCuentaAhorroPlanRequest request, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiUrl.Url}/api/Operacion/crear-cuenta-ahorro-plan", content);
                ResponseBase<CrearCuentaAhorroPlanResponse> resultService = await Util.ConvertResponse<ResponseBase<CrearCuentaAhorroPlanResponse>>(response);
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(request.ToString(), 500, ex, cachelocal);
                return new ResponseBase<CrearCuentaAhorroPlanResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(request.ToString(), 200, cachelocal);
            }
        }
    }
}
