using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IUserAuthenticationService
    {
        Task<Result<AuthenticationModel>> Login(LoginRequestDTO loginRequest);

        Task<Result<string>> Logout();
    }
}
