using AutoMapper;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Features.Queries;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using static WebBFF.Cliente.Model.Entity.EnumTypes;
using WebBFF.Cliente.Domain.Common;

namespace WebBFF.Cliente.Application.Features.Handlers
{
    public class ConsultarCuentaQueryHandler : BaseCommand, IDecoradorRequestHandler<ConsultarCuentaQuery, ResponseBase<ConsultarCuentaResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public ConsultarCuentaQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<ConsultarCuentaResponse>> Handle(ConsultarCuentaQuery request, CancellationToken cancellationToken)
        {
            ConsultarCuentaRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            ConsultarCuentaResponse objResponse = new ConsultarCuentaResponse();
            try
            {
                ResponseBase<BuscarCuentaResponse> objCliente = await _centralNegocioService.BuscarCuenta((int)RequestData.cuenta_id!, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<ConsultarCuentaResponse>(IdTraking, objCliente.error.codeError, Status: 500);
                
                objResponse.cuenta = objCliente.data!.cuenta;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<ConsultarCuentaResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
