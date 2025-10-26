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
    public class ConsultarClientesQueryHandler : BaseCommand, IDecoradorRequestHandler<ConsultarClientesQuery, ResponseBase<ConsultarClientesResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public ConsultarClientesQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<ConsultarClientesResponse>> Handle(ConsultarClientesQuery request, CancellationToken cancellationToken)
        {
            ConsultarClientesRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            ConsultarClientesResponse objResponse = new ConsultarClientesResponse();
            try
            {
                ResponseBase<BuscarClientesResponse> objCliente = await _centralNegocioService.BuscarClientes(IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<ConsultarClientesResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.clientes = objCliente.data!.clientes;
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<ConsultarClientesResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
