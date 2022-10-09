#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProyectoIds4.WebApi/ProyectoIds4.WebApi.csproj", "ProyectoIds4.WebApi/"]
RUN dotnet restore "ProyectoIds4.WebApi/ProyectoIds4.WebApi.csproj"
COPY . .
WORKDIR "/src/ProyectoIds4.WebApi"
RUN dotnet build "ProyectoIds4.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProyectoIds4.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProyectoIds4.WebApi.dll"]