#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/demoProjects/rentACar/WebAPI/RentACar.WebAPI.csproj", "src/demoProjects/rentACar/WebAPI/"]
COPY ["src/demoProjects/rentACar/Persistence/RentACar.Persistence.csproj", "src/demoProjects/rentACar/Persistence/"]
COPY ["src/demoProjects/rentACar/Application/RentACar.Application.csproj", "src/demoProjects/rentACar/Application/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "src/corePackages/Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcers/Core.CrossCuttingConcerns.csproj", "src/corePackages/Core.CrossCuttingConcers/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "src/corePackages/Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "src/corePackages/Core.Persistence/"]
COPY ["src/demoProjects/rentACar/Domain/RentACar.Domain.csproj", "src/demoProjects/rentACar/Domain/"]
RUN dotnet restore "src/demoProjects/rentACar/WebAPI/RentACar.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/demoProjects/rentACar/WebAPI"
RUN dotnet build "RentACar.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentACar.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentACar.WebAPI.dll"]