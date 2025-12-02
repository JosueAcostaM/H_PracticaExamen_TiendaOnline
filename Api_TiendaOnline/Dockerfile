FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api_TiendaOnline/Api_TiendaOnline.csproj", "Api_TiendaOnline/"]
RUN dotnet restore "Api_TiendaOnline/Api_TiendaOnline.csproj"
COPY . .


WORKDIR "/src/Api_TiendaOnline"

RUN dotnet build "Api_TiendaOnline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api_TiendaOnline.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api_TiendaOnline.dll"]