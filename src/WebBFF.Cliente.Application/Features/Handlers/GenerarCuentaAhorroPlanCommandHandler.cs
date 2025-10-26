using AutoMapper;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Features.Commands;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using WebBFF.Cliente.Domain.Common;
using static WebBFF.Cliente.Model.Entity.EnumTypes;

namespace WebBFF.Cliente.Application.Features.Handlers
{
    public class GenerarCuentaAhorroPlanCommandHandler : BaseCommand, IDecoradorRequestHandler<GenerarCuentaAhorroPlanCommand, ResponseBase<GenerarCuentaAhorroPlanResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public GenerarCuentaAhorroPlanCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<GenerarCuentaAhorroPlanResponse>> Handle(GenerarCuentaAhorroPlanCommand request, CancellationToken cancellationToken)
        {
            GenerarCuentaAhorroPlanRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GenerarCuentaAhorroPlanResponse objResponse = new GenerarCuentaAhorroPlanResponse();
            try
            {
                CrearCuentaAhorroPlanRequest objAhorroPlan = new CrearCuentaAhorroPlanRequest()
                {
                    cliente_id = RequestData.cliente_id,
                    cuenta_id = RequestData.cuenta_id,
                    moneda = RequestData.moneda,
                    monto = RequestData.monto,
                };

                ResponseBase<CrearCuentaAhorroPlanResponse> objCliente = await _centralNegocioService.CrearCuentaAhorroPlan(objAhorroPlan, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<GenerarCuentaAhorroPlanResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cuenta = objCliente.data!.cuenta;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GenerarCuentaAhorroPlanResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
