using System.Collections.Generic;
using System.Linq;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.ReadModels.Position
{
    public class PositionQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PositionFunctionProperty> Functions { get; set; } = new List<PositionFunctionProperty>();

        public ICollection<int> BoundedAlerter { get; set; } = new List<int>();

        public bool ShowDoor { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public int AreaId { get; set; }

        public string AreaName { get; set; }
    }

    public class AreaCascadeQueryModel
    {
        public string Value { get; set; }

        public string Label { get; set; }

        public ICollection<PositionCascadeQueryModel> Children { get; set; } = new List<PositionCascadeQueryModel>();
    
        public static IList<AreaCascadeQueryModel> ConvertToCascade(IEnumerable<PositionQueryModel> positions)
        {
            var cascadeAreas= new List<AreaCascadeQueryModel>();
            foreach(var position in positions)
            {
                var area = cascadeAreas.FirstOrDefault(t => t.Value.Equals(position.AreaId.ToString()));
                if (area == null)
                {
                    var newArea = new AreaCascadeQueryModel
                    {
                        Value = position.AreaId.ToString(),
                        Label = position.AreaName
                    };
                    newArea.Children.Add(new PositionCascadeQueryModel
                    {
                        Value = position.Id.ToString(),
                        Label = position.Name,
                        Functions = position.Functions
                    });
                    cascadeAreas.Add(newArea);
                }
                else
                {
                    area.Children.Add(new PositionCascadeQueryModel
                    {
                        Value = position.Id.ToString(),
                        Label = position.Name,
                        Functions = position.Functions
                    });
                }
            }
            return cascadeAreas;
        }
    }

    public class PositionCascadeQueryModel
    {
        public string Value { get; set; }

        public string Label { get; set; }

        public ICollection<PositionFunctionProperty> Functions { get; set; } = new List<PositionFunctionProperty>();

    }
}