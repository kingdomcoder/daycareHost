# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Daycare.WebAPIHost/Daycare.WebAPIHost.csproj"
RUN dotnet publish "Daycare.WebAPIHost/Daycare.WebAPIHost.csproj" -c Release -o /app /p:UseAppHost=false

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Daycare.WebAPIHost.dll"]
