using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Role;
using Susalem.Core.Application.ReadModels.User;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IApplicationUserService
    {
        #region User
        Task<Result<UserReadModel>> CreateUserAsync(UserRequestDTO createUserRequestDto);


        Task<Result> ActivateUserAsync(string username);


        Task<Result> DeactivateUserAsync(string username);


        Task<Result<IEnumerable<UserReadModel>>> GetAllUsersAsync();

        Task<Result<UserReadModel>> GetUserAsync(string username);

        Task<Result> EditUserAsync(string userId, UserRequestDTO createUserRequestDto);


        Task<Result> ChangePasswordAsync(string username, string password);

        Task<Result> DeleteUserAsync(string username);

        ///// <summary>
        ///// Update user roles
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="roles"></param>
        ///// <returns></returns>
        //Task<Result> UpdateRolesAsync(string username, List<string> roles);

        Task<Result> ResetPasswordAsync(string username);

        Task<Result<IList<string>>> GetUserRolesAsync(string username);

        #endregion

        #region Role

        Task<Result<RoleReadModel>> CreateRoleAsync(RoleRequestDTO createUserRequestDto);

        /// <summary>
        /// Get available roles
        /// </summary>
        /// <returns></returns>
        Task<Result<List<RoleReadModel>>> GetRolesAsync();

        Task<Result> DeleteRoleAsync(string roleId);

        Task<Result> EditRoleAsync(string roleId, RoleRequestDTO createUserRequestDto);

        Task<Result<RoleReadModel>> GetRoleAsync(string roleId);

        ///// <summary>
        ///// 获取角色支持的功能
        ///// </summary>
        ///// <returns></returns>
        //Task<Result<IEnumerable<string>>> GetRoleFunctionsAsync();
        #endregion
    }
}
