using System;

namespace Susalem.Core.Application.ReadModels
{
    public abstract class QueryStringParameters
    {
        private const int MaxPageSize = 500;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }

    public interface IQueryDateTimeParameters
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
