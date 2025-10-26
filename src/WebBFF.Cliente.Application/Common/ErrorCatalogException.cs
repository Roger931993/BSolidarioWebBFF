using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;

namespace WebBFF.Cliente.Application.Common
{
    public class ErrorCatalogException : ApplicationException, IErrorCatalogException
    {
        private readonly IRedisCache _redisCache;

        public ErrorCatalogException(IRedisCache redisCache)
        {
            this._redisCache = redisCache;
        }

        public int CodeError { get; private set; }
        public int StatusCodeError { get; private set; }
        public string? MessageError { get; private set; }
        public Guid? IdTraking { get; set; }
        public string Exception { get; set; }

        public async Task SetCodeError(int code, Guid? idTraking)
        {
            List<catalog_errorDto> objErrorCache = await _redisCache.GetAsync<List<catalog_errorDto>>($"catalog_error");
            objErrorCache = objErrorCache != null ? objErrorCache : new List<catalog_errorDto>();

            IdTraking = idTraking;
            catalog_errorDto objError = objErrorCache.FirstOrDefault(x => x.catalog_error_id == code)!;
            CodeError = code;
            if (objError != null)
            {
                MessageError = objError!.error_description;
                StatusCodeError = objError!.error_status_code;
            }
            else
            {

                MessageError = "Error no encontrado en la base de datos";
                StatusCodeError = 500;

            }
        }

        public async Task SetCodeError(int code, Guid? idTraking, Exception ex)
        {
            List<catalog_errorDto> objErrorCache = await _redisCache.GetAsync<List<catalog_errorDto>>($"catalog_error");
            objErrorCache = objErrorCache != null ? objErrorCache : new List<catalog_errorDto>();

            IdTraking = idTraking;
            catalog_errorDto objError = objErrorCache.FirstOrDefault(x => x.catalog_error_id == code)!;
            CodeError = code;
            Exception = ex.Message;
            if (objError != null)
            {
                MessageError = objError!.error_description;
                StatusCodeError = objError!.error_status_code;
            }
            else
            {

                MessageError = "Error no encontrado en la base de datos";
                StatusCodeError = 500;

            }
        }

        public void SetCodeErrorMessage(int code, string? message, Guid? idTraking, int StatusCode = 500)
        {
            IdTraking = idTraking;
            CodeError = code;
            MessageError = message;
            StatusCodeError = StatusCode;
        }
        public void SetCodeErrorMessage(int code, Exception ex, string? message, Guid? idTraking, int StatusCode = 500)
        {
            IdTraking = idTraking;
            CodeError = code;
            MessageError = message;
            StatusCodeError = StatusCode;
            Exception = ex.Message;
        }
    }
}
