# Dockerfile for SmartFinanceAI Blazor application

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copy Blazor project files
COPY ./src/SmartFinanceAI/SmartFinanceAI.Blazor/ ./

# Restore dependencies
RUN dotnet restore

# Publish release build to /App/out
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App

# Copy published output
COPY --from=build /App/out/ .

# Copy SQLite DB if used
COPY --from=build /App/Data/SmartFinanceAIAppContext.db ./Data/SmartFinanceAIAppContext.db

# Environment variables
ENV KBS_LOG_PATH="/etc/logs"
ENV KBS_CPU_POWER_WATTS=65

# Set ASP.NET Core to listen on all interfaces and port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Expose the port
EXPOSE 8080

# Launch the app
ENTRYPOINT ["dotnet", "SmartFinanceAI.Blazor.dll"]