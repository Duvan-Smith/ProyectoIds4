#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProyectoIds4.Empty.IdentityServer4/ProyectoIds4.Empty.IdentityServer4.csproj", "ProyectoIds4.Empty.IdentityServer4/"]
RUN dotnet restore "ProyectoIds4.Empty.IdentityServer4/ProyectoIds4.Empty.IdentityServer4.csproj"
COPY . .
WORKDIR "/src/ProyectoIds4.Empty.IdentityServer4"
RUN dotnet build "ProyectoIds4.Empty.IdentityServer4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProyectoIds4.Empty.IdentityServer4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProyectoIds4.Empty.IdentityServer4.dll"]