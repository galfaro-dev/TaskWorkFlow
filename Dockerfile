FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80  

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# COPIAS: Asegúrate de que el nombre de la carpeta (izquierda) 
# sea IGUAL al que ves en Windows
COPY ["TaskWorkFlow/TaskWorkFlow.csproj", "TaskWorkFlow/"]
COPY ["TaskWorkFlow.Application/TaskWorkFlow.Application.csproj", "TaskWorkFlow.Application/"]
COPY ["TaskWorkFlow.Domain/TaskWorkFlow.Domain.csproj", "TaskWorkFlow.Domain/"]
COPY ["TaskWorkFlow.Infrastructure/TaskWorkFlow.Infrastructure.csproj", "TaskWorkFlow.Infrastructure/"]

# El restore debe apuntar al archivo exacto
RUN dotnet restore "TaskWorkFlow/TaskWorkFlow.csproj"

COPY . .

# WORKDIR: Aquí es donde entras a la carpeta para compilar
WORKDIR "/src/TaskWorkFlow"
RUN dotnet build "TaskWorkFlow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskWorkFlow.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# El nombre de la DLL debe ser el mismo que el del proyecto
ENTRYPOINT ["dotnet", "TaskWorkFlow.dll"]