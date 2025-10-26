using AutoMapper;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Features.Queries;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using WebBFF.Cliente.Domain.Common;
using static WebBFF.Cliente.Model.Entity.EnumTypes;

namespace WebBFF.Cliente.Application.Features.Handlers
{
    internal class ConsultaClienteCuentasQueryHandler : BaseCommand, IDecoradorRequestHandler<ConsultaClienteCuentasQuery, ResponseBase<ConsultaClienteCuentasResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public ConsultaClienteCuentasQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<ConsultaClienteCuentasResponse>> Handle(ConsultaClienteCuentasQuery request, CancellationToken cancellationToken)
        {
            ConsultaClienteCuentasRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            ConsultaClienteCuentasResponse objResponse = new ConsultaClienteCuentasResponse();
            try
            {
                ResponseBase<BuscarClienteResponse> objCliente = await _centralNegocioService.BuscarCliente((int)RequestData.client_id!, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<ConsultaClienteCuentasResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cliente = objCliente.data!.cliente;
                objResponse.cuentas = objCliente.data.cuentas;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<ConsultaClienteCuentasResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
