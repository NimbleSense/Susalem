using System.Collections.Generic;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.ReadModels.Record;

namespace Susalem.Core.Application.Commands.Events
{
    public record PositionDataEvent(int PositionId, string Function, ICollection<RecordContent> RecordContents) : IRequest<Result>;
}
