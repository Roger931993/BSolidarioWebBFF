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
    public class GenerarCuentaCommandHandler : BaseCommand, IDecoradorRequestHandler<GenerarCuentaCommand, ResponseBase<GenerarCuentaResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public GenerarCuentaCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<GenerarCuentaResponse>> Handle(GenerarCuentaCommand request, CancellationToken cancellationToken)
        {
            GenerarCuentaRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GenerarCuentaResponse objResponse = new GenerarCuentaResponse();
            try
            {
                CrearCuentaRequest objAhorroPlan = new CrearCuentaRequest()
                {
                    cliente_id = RequestData.cliente_id,
                    moneda = RequestData.moneda,
                    producto_id = RequestData.producto_id,
                    tipo_cuenta = RequestData.tipo_cuenta  
                };

                ResponseBase<CrearCuentaResponse> objCliente = await _centralNegocioService.CrearCuenta(objAhorroPlan, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<GenerarCuentaResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cuenta = objCliente.data!.cuenta;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GenerarCuentaResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
