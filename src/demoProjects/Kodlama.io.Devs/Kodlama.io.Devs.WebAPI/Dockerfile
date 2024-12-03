#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.WebAPI/Kodlama.io.Devs.WebAPI.csproj", "src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.WebAPI/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "src/corePackages/Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcers/Core.CrossCuttingConcerns.csproj", "src/corePackages/Core.CrossCuttingConcers/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "src/corePackages/Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "src/corePackages/Core.Persistence/"]
COPY ["src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Persistence/Kodlama.io.Devs.Persistence.csproj", "src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Persistence/"]
COPY ["src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Application/Kodlama.io.Devs.Application.csproj", "src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Application/"]
COPY ["src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Domain/Kodlama.io.Devs.Domain.csproj", "src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.Domain/"]
RUN dotnet restore "src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.WebAPI/Kodlama.io.Devs.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/demoProjects/Kodlama.io.Devs/Kodlama.io.Devs.WebAPI"
RUN dotnet build "Kodlama.io.Devs.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kodlama.io.Devs.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kodlama.io.Devs.WebAPI.dll"]