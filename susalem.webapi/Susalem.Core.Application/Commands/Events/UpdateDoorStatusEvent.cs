using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events;

public record UpdateDoorStatusEvent(int PositionId, bool Door1, bool Door2, bool Door3, bool Door4) : INotification;