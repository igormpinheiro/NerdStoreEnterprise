FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/NSE.WebApp.Blazor/NSE.WebApp.Blazor.csproj", "NSE.WebApp.Blazor/"]
RUN dotnet restore "src/NSE.WebApp.Blazor/NSE.WebApp.Blazor.csproj"
COPY . .
WORKDIR "/src/NSE.WebApp.Blazor"
RUN dotnet build "NSE.WebApp.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.WebApp.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.WebApp.Blazor.dll"]
