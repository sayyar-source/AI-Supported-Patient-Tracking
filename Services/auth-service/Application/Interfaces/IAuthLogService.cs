using Auth.Application.Common;

namespace Auth.Application.Interfaces;
public interface IAuthLogService
{
    Task LogAsync(AuthLog log);
}
