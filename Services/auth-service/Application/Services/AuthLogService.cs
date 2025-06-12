using Auth.Application.Common;
using Auth.Application.Interfaces;
using Serilog;

namespace Auth.Application.Services;
public class AuthLogService : IAuthLogService
{
    public Task LogAsync(AuthLog log)
    {
        Log.Information("Auth event: {@AuthLog}", log);
        return Task.CompletedTask;
    }
}
