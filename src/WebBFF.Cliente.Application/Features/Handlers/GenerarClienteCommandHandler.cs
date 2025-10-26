using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBFF.Cliente.Application.DTOs;
using WebBFF.Cliente.Application.DTOs.Base;
using WebBFF.Cliente.Application.Features.Commands;
using WebBFF.Cliente.Application.Features.Queries;
using WebBFF.Cliente.Application.Interfaces.Base;
using WebBFF.Cliente.Application.Interfaces.Infraestructure;
using static WebBFF.Cliente.Model.Entity.EnumTypes;
using WebBFF.Cliente.Domain.Common;

namespace WebBFF.Cliente.Application.Features.Handlers
{
    public class GenerarClienteCommandHandler : BaseCommand, IDecoradorRequestHandler<GenerarClienteCommand, ResponseBase<GenerarClienteResponse>>
    {
        private readonly ICentralNegocioService _centralNegocioService;

        public GenerarClienteCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICentralNegocioService centralNegocioService) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._centralNegocioService = centralNegocioService;
        }

        public async Task<ResponseBase<GenerarClienteResponse>> Handle(GenerarClienteCommand request, CancellationToken cancellationToken)
        {
            GenerarClienteRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GenerarClienteResponse objResponse = new GenerarClienteResponse();
            try
            {
                CrearClienteRequest objDeposito = new CrearClienteRequest()
                {
                   apellido_materno = RequestData.apellido_materno,
                   apellido_paterno = RequestData.apellido_paterno,
                   identificacion = RequestData.identificacion,
                   primer_nombre = RequestData.primer_nombre,
                   segundo_nombre = RequestData.segundo_nombre,
                   user_name = RequestData.user_name    
                };

                ResponseBase<CrearClienteResponse> objCliente = await _centralNegocioService.RegistrarCliente(objDeposito, IdTraking);
                if (!objCliente.error.success)
                    return await ErrorResponse<GenerarClienteResponse>(IdTraking, objCliente.error.codeError, Status: 500);

                objResponse.cliente = objCliente.data!.cliente;

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GenerarClienteResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
