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
    internal class ConsultarProyeccionAhorroQueryHandler : BaseCommand, IDecoradorRequestHandler<ConsultarProyeccionAhorroQuery, ResponseBase<ConsultarProyeccionAhorroResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public ConsultarProyeccionAhorroQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<ConsultarProyeccionAhorroResponse>> Handle(ConsultarProyeccionAhorroQuery request, CancellationToken cancellationToken)
        {
            ConsultarProyeccionAhorroRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            ConsultarProyeccionAhorroResponse objResponse = new ConsultarProyeccionAhorroResponse();
            try
            {
                ResponseBase<ProyeccionAhorroResponse> objCliente = await _centralNegocioService.ProyeccionAhorroPlan((int)RequestData.cliente_id!, (int)RequestData.cuenta_id!, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<ConsultarProyeccionAhorroResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cuenta = objCliente.data!.cuenta;
                objResponse.cliente = objCliente.data.cliente;
                objResponse.proyeccion = objCliente.data.proyeccion;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<ConsultarProyeccionAhorroResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
