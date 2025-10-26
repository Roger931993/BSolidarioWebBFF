using WebBFF.Cliente.Application.Interfaces.Infraestructure;

namespace WebBFF.Cliente.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IMemoryCacheLocalService _memoryCacheLocalService;
        private readonly IServiceScopeFactory _scopeFactory;

        //private readonly IDbContextFactory<LoggDbContext> _dbContext;
        private readonly IConfiguration _configuration;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IMemoryCacheLocalService memoryCacheLocalService, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _memoryCacheLocalService = memoryCacheLocalService;
            this._scopeFactory = scopeFactory;
            //_dbContext = dbContext;
            this._configuration = configuration;

        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering(); // Habilita la capacidad de leer el stream varias veces

            IHeaderDictionary headers = context.Request.Headers;
            #region Validar Headers 
            if (!headers.Any(x => x.Key == "IdTraker"))
                context.Request.Headers.Add("IdTraker", Guid.NewGuid().ToString());
            #endregion            

            await _next(context);

        }

    }
}
