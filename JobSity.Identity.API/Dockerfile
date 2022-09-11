#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["JobSity.Identity.API/JobSity.Identity.API.csproj", "JobSity.Identity.API/"]
RUN dotnet restore "JobSity.Identity.API/JobSity.Identity.API.csproj"
COPY . .
WORKDIR "/src/JobSity.Identity.API"
RUN dotnet build "JobSity.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSity.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSity.Identity.API.dll"]