using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
        var stopWatch = Stopwatch.StartNew();
        _logger.LogCommandsBefore(GetType().FullName!);

        var response = await next();

        stopWatch.Stop();

        var elapsed = stopWatch.ElapsedMilliseconds;
        if (elapsed > 3000)
        {
        _logger.LogCommandsAfterCritical(GetType().FullName!, elapsed);
        }

        _logger.LogCommandsAfter(GetType().FullName!, elapsed);
        return response;
    }
}
