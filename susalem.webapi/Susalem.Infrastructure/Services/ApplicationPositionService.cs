using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Position;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services
{
    public class ApplicationPositionService : IApplicationPositionService
    {
        private readonly ILogger<ApplicationPositionService> _logger;
        private readonly IMapper _mapper;
        private readonly IPlatformService _platformService;
        private readonly IEntityRepository<AreaEntity, int> _areaRepository;
        private readonly IEntityRepository<PositionEntity, int> _positionRepository;
        private readonly IEntityRepository<DeviceEntity, int> _deviceRepository;

        public ApplicationPositionService(ILogger<ApplicationPositionService> logger,
            IMapper mapper,
            IPlatformService platformService,
            IEntityRepository<AreaEntity, int> areaRepository,
            IEntityRepository<PositionEntity, int> positionRepository,
            IEntityRepository<DeviceEntity, int> deviceRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _platformService = platformService;
            _areaRepository = areaRepository;
            _positionRepository = positionRepository;
            _deviceRepository = deviceRepository;
        }

        #region Area
        public async Task<Result<AreaDTO>> CreateAreaAsync(AreaDTO areaDto)
        {
            var serviceResult = new Result<AreaDTO>();
            try
            {
                var areaEntity = _mapper.Map<AreaEntity>(areaDto);

                await _areaRepository.AddAsync(areaEntity);

                await _areaRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful().WithData(_mapper.Map<AreaDTO>(areaEntity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create area.");
            }
            return serviceResult;
        }

        public async Task<Result> EditAreaAsync(int areaId, AreaDTO areaDto)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _areaRepository.FindAsync(areaId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("area not found");
                }
                _mapper.Map(areaDto, entity);
                _areaRepository.Update(entity);
                await _areaRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit area");
                return serviceResult;
            }
        }

        public async Task<Result> DeleteAreaAsync(int areaId)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _areaRepository.FindAsync(areaId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("area not found");
                }
                _areaRepository.Delete(entity);
                await _areaRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete area");
                return serviceResult;
            }
        }

        public async Task<Result<IEnumerable<AreaDTO>>> GetAreasAsync()
        {
            var serviceResult = new Result<IEnumerable<AreaDTO>>();
            try
            {
                var areas = await _areaRepository.GetAll().ToListAsync();
                var areaDtos = _mapper.Map<IEnumerable<AreaDTO>>(areas);
                serviceResult.Successful().WithData(areaDtos);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get areas");
            }

            return serviceResult;
        }

        public async Task<Result<AreaDTO>> GetAreaAsync(int areaId)
        {
            var serviceResult = new Result<AreaDTO>();
            try
            {
                var area = await _areaRepository.FindAsync(areaId);
                if (area == null)
                {
                    serviceResult.Failed().WithError("area not found");
                }
                var areaDto = _mapper.Map<AreaDTO>(area);
                serviceResult.Successful().WithData(areaDto);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get area by id");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<MonitorAreaQueryModel>>> GetMonitorAreasAsync()
        {
            var serviceResult = new Result<IEnumerable<MonitorAreaQueryModel>>();
            try
            {
                var areas = await _areaRepository.GetAll().Include(t => t.Positions).ToListAsync();

                var areaDto = _mapper.Map<IEnumerable<MonitorAreaQueryModel>>(areas);
                serviceResult.Successful().WithData(areaDto);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get all areas for monitor");
            }

            return serviceResult;
        }

        #endregion

        #region Position

        public async Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsAsync()
        {
            var serviceResult = new Result<IEnumerable<PositionQueryModel>>();
            try
            {
                var positions = await _positionRepository.GetAll().Include(t=>t.Area).ToListAsync();
                var positionModels = _mapper.Map<IEnumerable<PositionQueryModel>>(positions);
                serviceResult.Successful().WithData(positionModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get positions");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsAsync(ICollection<int> positionIds)
        {
            var serviceResult = new Result<IEnumerable<PositionQueryModel>>();
            try
            {
                var positions = await _positionRepository.GetBy(t=>positionIds.Contains(t.Id)).ToListAsync();
                var positionModels = _mapper.Map<IEnumerable<PositionQueryModel>>(positions);
                serviceResult.Successful().WithData(positionModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get specific positions");
            }

            return serviceResult;
        }

        public async Task<Result<PositionQueryModel>> GetPositionAsync(int positionId)
        {
            var serviceResult = new Result<PositionQueryModel>();
            try
            {
                var position = await _positionRepository.FindAsync(positionId);
                if (position == null)
                {
                    serviceResult.Failed().WithError("position not found");
                }
                var positionModel = _mapper.Map<PositionQueryModel>(position);
                serviceResult.Successful().WithData(positionModel);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get position by area id");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsByAreaIdAsync(int areaId)
        {
            var serviceResult = new Result<IEnumerable<PositionQueryModel>>();
            try
            {
                var positions = await _positionRepository.GetBy(t => t.AreaId == areaId).ToListAsync();
                var positionModels = _mapper.Map<IEnumerable<PositionQueryModel>>(positions);
                serviceResult.Successful().WithData(positionModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get positions by area id");
            }

            return serviceResult;
        }

        public async Task<Result<PositionQueryModel>> CreatePositionAsync(PositionRequestDTO positionDto)
        {
            var serviceResult = new Result<PositionQueryModel>();
            try
            {
                var positionEntity = _mapper.Map<PositionEntity>(positionDto);

                await _positionRepository.AddAsync(positionEntity);

                await _positionRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful().WithData(_mapper.Map<PositionQueryModel>(positionEntity));
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create position.");
            }
            return serviceResult;
        }

        public async Task<Result> EditPositionAsync(int positionId, PositionRequestDTO positionDto)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _positionRepository.GetSingleAsync(a => a.Id == positionId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("position not found");
                }

                var updatedEntity = _mapper.Map(positionDto, entity);
                _positionRepository.Update(updatedEntity);
                await _positionRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit position");
                return serviceResult;
            }
        }

        public async Task<Result> DeletePositionAsync(int positionId)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _positionRepository.GetSingleAsync(a => a.Id == positionId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("position not found");
                }
                _positionRepository.Delete(entity);
                await _positionRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete position");
                return serviceResult;
            }
        }

        #endregion

        #region Device

        public async Task<Result> UpdateFunctionAsync(int positionId, IList<PositionFunctionProperty> positionFunctions)
        {
            var serviceResult = new Result();
            try
            {
                var position = await _positionRepository.FindAsync(positionId);
                if (position == null)
                {
                    return serviceResult.Failed().WithError("position not found");
                }

                position.Functions = positionFunctions;
                _positionRepository.Update(position);
                
                await _positionRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to Update position functions");
                return serviceResult;
            }
        }

        public async Task<Result> DeleteFunctionAsync(int positionId, PositionFunctionEnum positionFunction)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _positionRepository.GetSingleAsync(a => a.Id == positionId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("position not found");
                }

                var function = entity.Functions.FirstOrDefault(t=>t.Key ==positionFunction);
                if (function != null)
                {
                    entity.Functions.Remove(function);
                    _positionRepository.Update(entity);
                    await _positionRepository.UnitOfWork.SaveChangesAsync();
                }
                else
                {
                    return serviceResult.Failed().WithError("position function not found");
                }

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete position");
                return serviceResult;
            }
        }

        public async Task<Result> EditPositionFunctionAsync(int positionId, List<PositionFunctionProperty> positionFunctions)
        {
            var serviceResult = new Result();
            try
            {
                var entity = await _positionRepository.GetSingleAsync(a => a.Id == positionId);
                if (entity == null)
                {
                    return serviceResult.Failed().WithError("position not found");
                }

                entity.Functions = positionFunctions;
                _positionRepository.Update(entity);
                await _positionRepository.UnitOfWork.SaveChangesAsync();

                return serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit position function with robot");
                return serviceResult;
            }
        }

        #endregion

    }
}
