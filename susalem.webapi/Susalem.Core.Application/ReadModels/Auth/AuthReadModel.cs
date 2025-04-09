using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.Auth;

public record AuthReadModel(string token,string userName, List<string> permissions, AuthConfigReadModel config);

public record AuthConfigReadModel(bool exportExcel, string customerName);
