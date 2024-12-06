namespace API
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed with the request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                // Log exception to the specified folder
                var exceptionLogPath = _configuration["Logging:LogFilePaths:ExceptionLogs"];
                if (!string.IsNullOrEmpty(exceptionLogPath))
                {
                    Directory.CreateDirectory(exceptionLogPath);
                    var logFile = Path.Combine(exceptionLogPath, $"exception-{DateTime.UtcNow:yyyy-MM-dd}.log");
                    await File.AppendAllTextAsync(logFile, $"[{DateTime.UtcNow}] {ex}\n");
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                Message = "An unexpected error occurred. Please try again later."
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }

}
