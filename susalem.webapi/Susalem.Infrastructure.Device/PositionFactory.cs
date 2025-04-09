using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Position;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Device
{
    internal class PositionFactory : IPositionFactory
    {
        private readonly ILogger<PositionFactory> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEngineFactory _engineFactory;

        public IEnumerable<PositionQueryModel> Positions { get; private set; } = new List<PositionQueryModel>();

        public ConcurrentDictionary<int, MonitorContext> MonitoringPositions { get; private set; }

        public PositionFactory(ILogger<PositionFactory> logger, 
            IServiceProvider serviceProvider,
            IEngineFactory engineFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _engineFactory = engineFactory;
            MonitoringPositions = new ConcurrentDictionary<int, MonitorContext>();
        }

        public IList<IAlerter> GetBoundAlerter(int positionId)
        {
            var relatedAlerter = new List<IAlerter>();
            var position = Positions.FirstOrDefault(t => t.Id == positionId);
            if (position == null) return relatedAlerter;

            return _engineFactory.Alerts.Where(t => position.BoundedAlerter.Contains(t.Id)).ToList();
        }

        public async Task InitializeAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var applicationPositionService = scope.ServiceProvider.GetService<IApplicationPositionService>();
            if (applicationPositionService == null) return;

            var positionsResult = await applicationPositionService.GetPositionsAsync();
            if (positionsResult.Failed) return;

            Positions = positionsResult.Data;
        }

        void IPositionFactory.MonitorPositions(ICollection<int> positionIds)
        {
            foreach (var positionId in positionIds)
            {
                var position = Positions.FirstOrDefault(t => t.Id == positionId);
                if (position == null) continue;

                var monitorContext = new MonitorContext(position);
                if (!MonitoringPositions.TryAdd(positionId, monitorContext))
                {
                    _logger.LogError($"Monitor position: {position.Name} register failed");
                    continue;
                }

                _logger.LogInformation($"Monitor position: {position.Name}");
            }
        }

        public void CancelMonitorPositions(ICollection<int> positionIds)
        {
            foreach (var positionId in positionIds)
            {
                if (!MonitoringPositions.ContainsKey(positionId))
                {
                    _logger.LogWarning($"Position: {positionId} is not in monitoring");
                }
                else
                {
                    if (!MonitoringPositions.TryRemove(positionId, out var monitorContext)) continue;
                    _logger.LogInformation($"Cancel monitor position: {monitorContext.PositionModel.Name}");
                }
            }
        }

        public PositionQueryModel GetPositionIdByName(string positionName)
        {
            return Positions.FirstOrDefault(t => t.Name.Equals(positionName));
        }
    }
}
