FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AWS_ECS_CoreApi.csproj", "./"]
RUN dotnet restore "AWS_ECS_CoreApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AWS_ECS_CoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AWS_ECS_CoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWS_ECS_CoreApi.dll"]
