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
    public class GenerarDepositoCommandHandler : BaseCommand, IDecoradorRequestHandler<GenerarDepositoCommand, ResponseBase<GenerarDepositoResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public GenerarDepositoCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<GenerarDepositoResponse>> Handle(GenerarDepositoCommand request, CancellationToken cancellationToken)
        {
            GenerarDepositoRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GenerarDepositoResponse objResponse = new GenerarDepositoResponse();
            try
            {
                CrearDepositoRequest objDeposito = new CrearDepositoRequest()
                {
                    cliente_id = RequestData.cliente_id,
                    cuenta_id = RequestData.cuenta_id,
                    monto = RequestData.monto,
                };

                ResponseBase<CrearDepositoResponse> objCliente = await _centralNegocioService.GenerarDeposito(objDeposito, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<GenerarDepositoResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cuenta = objCliente.data!.cuenta;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GenerarDepositoResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
