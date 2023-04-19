FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 6000
ENV ASPNETCORE_URLS=http://+:6000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY Service2 .
RUN dotnet restore  

RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Service2.dll"]