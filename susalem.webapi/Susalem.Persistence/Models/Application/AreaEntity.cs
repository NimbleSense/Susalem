using System.Collections.Generic;

namespace Susalem.Infrastructure.Models.Application
{
    public class AreaEntity : DataEntityBase<int>
    {
        public string Name { get; set; }

        public virtual ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
    }
}
