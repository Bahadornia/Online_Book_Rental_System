using MediatR;
using Microsoft.Extensions.Logging;

namespace Framework.CQRS;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public  async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        _logger.LogInformation("before saving {Title}, {azx}, {ElapsedTime}", "Book1", 456, TimeSpan.FromDays(1));
        var response = await next();
        _logger.LogInformation($"after saving {response}");
        return response;
    }
}
