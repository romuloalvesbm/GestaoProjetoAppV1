#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GestaoProjeto.Presentation/GestaoProjeto.Presentation.csproj", "GestaoProjeto.Presentation/"]
COPY ["GestaoProjeto.Application/GestaoProjeto.Application.csproj", "GestaoProjeto.Application/"]
COPY ["GestaoProjeto.CrossCutting.Exception/GestaoProjeto.CrossCutting.Exception.csproj", "GestaoProjeto.CrossCutting.Exception/"]
COPY ["GestaoProjeto.Infra.Domain/GestaoProjeto.Infra.Domain.csproj", "GestaoProjeto.Infra.Domain/"]
COPY ["GestaoProjeto.Presentation.Contracts/GestaoProjeto.Presentation.Contracts.csproj", "GestaoProjeto.Presentation.Contracts/"]
COPY ["GestaoProjeto.Infra.Database/GestaoProjeto.Infra.Database.csproj", "GestaoProjeto.Infra.Database/"]
RUN dotnet restore "./GestaoProjeto.Presentation/GestaoProjeto.Presentation.csproj"
COPY . .
WORKDIR "/src/GestaoProjeto.Presentation"
RUN dotnet build "./GestaoProjeto.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GestaoProjeto.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestaoProjeto.Presentation.dll"]