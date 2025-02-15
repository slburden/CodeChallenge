# Use .NET 8 SDK image for development
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Install dependencies
COPY ["RapidPay.sln", "./"]
COPY ["src/Api/Api.csproj", "Api/"]
COPY ["src/Business/Business.csproj", "Business/"]
COPY ["src/DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["src/Models/Models.csproj", "Models/"]
RUN dotnet restore

# Set up the entrypoint for dotnet watch
CMD ["dotnet", "watch", "--project", "Api", "run", "--no-launch-profile", "--urls", "http://0.0.0.0:5000"]
