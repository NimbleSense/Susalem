using System.Text;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Record;

namespace Susalem.Core.Application.DTOs
{
    public class RecordRequestDTO
    {
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public string AreaName { get; set; }

        public ICollection<RecordContent> Contents { get; set; } = new List<RecordContent>();

        public PositionFunctionEnum PositionFunction { get; set; }

        public DateTime CreateTime { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var content in Contents)
            {
                builder.Append(content);
            }
            return $"{PositionId} {PositionFunction} {builder}";
        }
    }
}
