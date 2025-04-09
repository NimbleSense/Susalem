using System;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IApplicationConfigurationService
    {
        Task<Result> CreateAsync(ApplicationConfigurationDTO applicationConfiguration);

        Task<Result<ApplicationConfigurationDTO>> DetailsAsync(string id, bool decrypt);

        Task<Result> EditAsync(ApplicationConfigurationDTO applicationConfiguration);

        Task<Result<bool>> DeleteAsync(string id);

        Result<string> GetValue(string key);

        T GetValue<T>(string key);

        Result<int> GetValueInt(string key);

        Result<bool> GetValueBool(string key);

        Result<DateTime> GetValueDateTime(string key);

        /// <summary>
        /// default '|' character
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Result<string[]> GetMultiple(string key);

        Result<string[]> GetMultiple(string key, string delimiter);
    }
}
