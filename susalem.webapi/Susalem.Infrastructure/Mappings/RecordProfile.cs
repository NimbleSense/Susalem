using AutoMapper;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.ReadModels.Record;
using Susalem.Infrastructure.Models.Record;

namespace Susalem.Infrastructure.Mappings
{
    internal class RecordProfile:Profile
    {
        public RecordProfile()
        {
            CreateMap<PositionRecordEntity, RecordRequestDTO>().ReverseMap();

            CreateMap<PositionRecordEntity, RecordReadModel>();

        }

    }
}
