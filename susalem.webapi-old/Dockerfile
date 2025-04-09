# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../services/susalem/src/Susalem.HttpApi.Host/Susalem.HttpApi.Host.csproj", "../services/susalem/src/Susalem.HttpApi.Host/"]
COPY ["../shared/SusalemAbp.Shared.Hosting.AspNetCore/SusalemAbp.Shared.Hosting.AspNetCore.csproj", "../shared/SusalemAbp.Shared.Hosting.AspNetCore/"]
COPY ["../services/susalem/src/Susalem.Application/Susalem.Application.csproj", "../services/susalem/src/Susalem.Application/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.Application/Susalem.Identity.Application.csproj", "../modules/susalem.identity/src/Susalem.Identity.Application/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.Application.Contracts/Susalem.Identity.Application.Contracts.csproj", "../modules/susalem.identity/src/Susalem.Identity.Application.Contracts/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.Domain.Shared/Susalem.Identity.Domain.Shared.csproj", "../modules/susalem.identity/src/Susalem.Identity.Domain.Shared/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.Domain/Susalem.Identity.Domain.csproj", "../modules/susalem.identity/src/Susalem.Identity.Domain/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.Application/Susalem.Mes.Application.csproj", "../modules/susalem.mes/src/Susalem.Mes.Application/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.Application.Contracts/Susalem.Mes.Application.Contracts.csproj", "../modules/susalem.mes/src/Susalem.Mes.Application.Contracts/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.Domain.Shared/Susalem.Mes.Domain.Shared.csproj", "../modules/susalem.mes/src/Susalem.Mes.Domain.Shared/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.Domain/Susalem.Mes.Domain.csproj", "../modules/susalem.mes/src/Susalem.Mes.Domain/"]
COPY ["../services/susalem/src/Susalem.Domain/Susalem.Domain.csproj", "../services/susalem/src/Susalem.Domain/"]
COPY ["../services/susalem/src/Susalem.Domain.Shared/Susalem.Domain.Shared.csproj", "../services/susalem/src/Susalem.Domain.Shared/"]
COPY ["../services/susalem/src/Susalem.Application.Contracts/Susalem.Application.Contracts.csproj", "../services/susalem/src/Susalem.Application.Contracts/"]
COPY ["../services/susalem/src/Susalem.EntityFrameworkCore/Susalem.EntityFrameworkCore.csproj", "../services/susalem/src/Susalem.EntityFrameworkCore/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.EntityFrameworkCore/Susalem.Identity.EntityFrameworkCore.csproj", "../modules/susalem.identity/src/Susalem.Identity.EntityFrameworkCore/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.EntityFrameworkCore/Susalem.Mes.EntityFrameworkCore.csproj", "../modules/susalem.mes/src/Susalem.Mes.EntityFrameworkCore/"]
COPY ["../services/susalem/src/Susalem.HttpApi/Susalem.HttpApi.csproj", "../services/susalem/src/Susalem.HttpApi/"]
COPY ["../modules/susalem.identity/src/Susalem.Identity.HttpApi/Susalem.Identity.HttpApi.csproj", "../modules/susalem.identity/src/Susalem.Identity.HttpApi/"]
COPY ["../modules/susalem.mes/src/Susalem.Mes.HttpApi/Susalem.Mes.HttpApi.csproj", "../modules/susalem.mes/src/Susalem.Mes.HttpApi/"]
RUN dotnet restore "./services/susalem/src/Susalem.HttpApi.Host/Susalem.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/susalem/src/Susalem.HttpApi.Host"
RUN dotnet build "./Susalem.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Susalem.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Susalem.HttpApi.Host.dll"]