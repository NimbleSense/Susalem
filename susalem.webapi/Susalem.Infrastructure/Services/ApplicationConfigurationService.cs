using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Common.Utilities;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Models.Application;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.Services
{
    internal class ApplicationConfigurationService : IApplicationConfigurationService
    {
        private readonly ILogger<ApplicationConfigurationService> _logger;
        private readonly IEntityRepository<ApplicationConfigurationEntity, string> _appConfigRepository;
        private readonly IMapper _mapper;
        private const string MULTIPLE_VALUE_DELIMITER = "1";
        private static List<ApplicationConfigurationEntity> _appConfigs;

        public ApplicationConfigurationService(ILogger<ApplicationConfigurationService> logger,
            IEntityRepository<ApplicationConfigurationEntity, string> appConfigRepository,
            IMapper mapper)
        {
            _logger = logger;
            _appConfigRepository = appConfigRepository;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(ApplicationConfigurationDTO applicationConfiguration)
        {
            var serviceResult = new Result<ApplicationConfigurationDTO>();
            try
            {
                if (applicationConfiguration.IsEncrypted)
                {
                    //TODO xiangzou need to encrypted.
                }

                await _appConfigRepository.AddAsync(_mapper.Map<ApplicationConfigurationEntity>(applicationConfiguration));
                await _appConfigRepository.UnitOfWork.SaveChangesAsync();
                serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger,ex,"Error while trying to create application configuration");
            }

            return serviceResult;
        }

        public async Task<Result<ApplicationConfigurationDTO>> DetailsAsync(string id, bool decrypt)
        {
            var serviceResult = new Result<ApplicationConfigurationDTO>();
            try
            {
                if (id == null)
                {
                    throw new Exception("Config not found");
                }

                var entity = await _appConfigRepository.FindAsync(id);
                if (entity == null)
                {
                    throw new Exception("Config not found");
                }

                if (decrypt && entity.IsEncrypted)
                {
                    //Todo xiangzou need to decrypt.
                }

                serviceResult.Data = _mapper.Map<ApplicationConfigurationDTO>(entity);
                serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, $"Error while trying to retrieve application configuration {id}");
            }

            return serviceResult;
        }

        public async Task<Result> EditAsync(ApplicationConfigurationDTO applicationConfiguration)
        {
            var serviceResult = new Result();
            try
            {
                var originalEntity = await _appConfigRepository.FindAsync(applicationConfiguration.Id);
                if (originalEntity == null)
                {
                    throw new Exception("Config not found");
                }
                if ((applicationConfiguration.IsEncrypted && !originalEntity.IsEncrypted) ||
                    (applicationConfiguration.IsEncrypted && applicationConfiguration.Value != originalEntity.Value))
                {
                    //TODO xiangzou add encrypt function
                }

                originalEntity.Value = applicationConfiguration.Value;

                _appConfigRepository.Update(originalEntity);
                await _appConfigRepository.UnitOfWork.SaveChangesAsync();
                _appConfigs = null;
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit application configuration");
            }

            return serviceResult;
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            var serviceResult = new Result<bool>();
            try
            {
                var entity = await _appConfigRepository.GetSingleAsync(a => a.Id == id);
                if (entity == null)
                {
                    throw new Exception("Config not found");
                }
                _appConfigRepository.Delete(entity);
                await _appConfigRepository.UnitOfWork.SaveChangesAsync();

                serviceResult.Data = true;
                return serviceResult;
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete application configuration");
                return serviceResult;
            }
        }

        public Result<string> GetValue(string key)
        {
            var serviceResult = new Result<string>();
            try
            {
                var entity = GetConfigurations().FirstOrDefault(m => m.Id == key);
                if (entity == null)
                {
                    throw new Exception($"Config: {key} not found");
                }
                string value = entity.Value;
                if (entity.IsEncrypted)
                    value = StringCipherHelper.DecryptWithRandomSalt(entity.Value);

                serviceResult.Data = value;
                return serviceResult;
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to retrieve application configuration value");
                return serviceResult;
            }
        }

        public T GetValue<T>(string key)
        {
            var result = GetValue(key);
            if (result.Failed)
            {
                return default;
            }

            if (string.IsNullOrEmpty(result.Data))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result.Data);
        }

        public Result<int> GetValueInt(string key)
        {
            throw new NotImplementedException();
        }

        public Result<bool> GetValueBool(string key)
        {
            var serviceResult = new Result<bool>();
            try
            {
                var entity = GetConfigurations().FirstOrDefault(m => m.Id == key);
                if (entity == null)
                {
                    serviceResult.Data = false;
                    return serviceResult;
                }
                string value = entity.Value;
                if (entity.IsEncrypted)
                    value = StringCipherHelper.DecryptWithRandomSalt(entity.Value);

                if (bool.TryParse(value, out bool valueBool))
                {
                    serviceResult.Data = valueBool;
                    return serviceResult;
                }

                throw new Exception("Convertion of application configuration value to bool is not successful");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to retrieve application configuration value from the database.");
                return serviceResult;
            }
        }

        public Result<DateTime> GetValueDateTime(string key)
        {
            throw new NotImplementedException();
        }

        public Result<string[]> GetMultiple(string key)
        {
            throw new NotImplementedException();
        }

        public Result<string[]> GetMultiple(string key, string delimiter)
        {
            throw new NotImplementedException();
        }

        private List<ApplicationConfigurationEntity> GetConfigurations()
        {
            if (_appConfigs == null || _appConfigs.Count == 0)
            {
                _appConfigs = _appConfigRepository.GetAll().ToList();
            }

            return _appConfigs;
        }
    }
}
