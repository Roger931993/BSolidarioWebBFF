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
    internal class GenerarTransferenciaCommandHandler : BaseCommand, IDecoradorRequestHandler<GenerarTransferenciaCommand, ResponseBase<GenerarTransferenciaResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public GenerarTransferenciaCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<GenerarTransferenciaResponse>> Handle(GenerarTransferenciaCommand request, CancellationToken cancellationToken)
        {
            GenerarTransferenciaRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GenerarTransferenciaResponse objResponse = new GenerarTransferenciaResponse();
            try
            {
                CrearTransferenciaRequest objTransferencia = new CrearTransferenciaRequest()
                {
                   cliente_id_destino = RequestData.cliente_id_destino,
                   cuenta_id_destino= RequestData.cuenta_id_destino,
                   cliente_id_origen = RequestData.cliente_id_origen,
                   cuenta_id_origen = RequestData.cuenta_id_origen,
                   motivo = RequestData.motivo,
                    monto = RequestData.monto,
                };

                ResponseBase<CrearTransferenciaResponse> ObjData = await _centralNegocioService.GenerarTransferencia(objTransferencia, IdTraking);
                if (!ObjData.error.success)
                    return await ErrorResponse<GenerarTransferenciaResponse>(IdTraking, ObjData.error.codeError, Status: 500);

                objResponse.cuenta_destino = ObjData.data!.cuenta_destino;
                objResponse.cuenta_origen = ObjData.data!.cuenta_origen;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GenerarTransferenciaResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
