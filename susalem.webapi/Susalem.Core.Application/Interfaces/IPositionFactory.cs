using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Position;

namespace Susalem.Core.Application.Interfaces
{
    /// <summary>
    /// 监控点位管理功能
    /// </summary>
    public interface IPositionFactory
    {
        IEnumerable<PositionQueryModel> Positions { get; }
        
        /// <summary>
        /// 初始化点位资源
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// 监控点位
        /// </summary>
        /// <param name="positionIds"></param>
        void MonitorPositions(ICollection<int> positionIds);

        /// <summary>
        /// 取消监控点位
        /// </summary>
        /// <param name="positionIds"></param>
        void CancelMonitorPositions(ICollection<int> positionIds);

        /// <summary>
        /// 正在监控中的点位
        /// </summary>
        ConcurrentDictionary<int, MonitorContext> MonitoringPositions { get; }

        /// <summary>
        /// 获取点位所绑定的报警器
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        IList<IAlerter> GetBoundAlerter(int positionId);

        /// <summary>
        /// 根据点位名字获取点位Id
        /// </summary>
        /// <param name="positionName"></param>
        /// <returns></returns>
        PositionQueryModel GetPositionIdByName(string positionName);
    }


    /// <summary>
    /// 点位监控详细信息
    /// </summary>
    public class MonitorContext
    {

        public PositionQueryModel PositionModel { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime GatherTime { get; set; }

        /// <summary>
        /// 正在响应的告警规则
        /// </summary>
        public List<int> TriggeredAlarmRules { get; }

        /// <summary>
        /// 最新的点位数据记录
        /// </summary>
        public IList<RecordRequestDTO> LatestPositionRecords { get; set; }

        public MonitorContext(PositionQueryModel positionModel)
        {
            PositionModel = positionModel;
            GatherTime = DateTime.Now;
            TriggeredAlarmRules = new List<int>();
            LatestPositionRecords = new List<RecordRequestDTO>();
        }
    }
}
