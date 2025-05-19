FROM mcr.microsoft.com/dotnet/sdk:latest AS build

WORKDIR /app

COPY *.sln .

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.BLL.Interfaces/*.csproj ./Base.BLL.Interfaces/
COPY Base.Dal.EF/*.csproj ./Base.Dal.EF/
COPY Base.DAL.Interfaces/*.csproj ./Base.DAL.Interfaces/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Helpers/*.csproj ./Base.Helpers/
COPY Base.Interfaces/*.csproj ./Base.Interfaces/
COPY Base.Resources/*.csproj ./Base.Resources/

COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.BLL.Interfaces/*.csproj ./App.BLL.Interfaces/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.DAL.Interfaces/*.csproj ./App.DAL.Interfaces/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Resources/*.csproj ./App.Resources/
COPY App.Test/*.csproj ./App.Test/

COPY WebApp/*.csproj ./WebApp/


RUN dotnet restore

COPY Base.BLL/. ./Base.BLL/
COPY Base.BLL.Interfaces/. ./Base.BLL.Interfaces/
COPY Base.Dal.EF/. ./Base.Dal.EF/
COPY Base.DAL.Interfaces/. ./Base.DAL.Interfaces/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Helpers/. ./Base.Helpers/
COPY Base.Interfaces/. ./Base.Interfaces/
COPY Base.Resources/. ./Base.Resources/

COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.BLL.Interfaces/. ./App.BLL.Interfaces/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.DAL.Interfaces/. ./App.DAL.Interfaces/
COPY App.Domain/. ./App.Domain/
COPY App.DTO/. ./App.DTO/
COPY App.Resources/. ./App.Resources/
COPY App.Test/. ./App.Test/

COPY WebApp/. ./WebApp/

RUN dotnet test App.Test/App.Test.csproj --configuration Release --logger "trx"

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app
EXPOSE 80
EXPOSE 8080

COPY --from=build /app/out ./
ENV ConnectionStrings:DefaultConnection="Host=host.docker.internal;Port=5432;Database=appdatabase;Username=postgres;Password=postgres"
ENTRYPOINT ["dotnet", "WebApp.dll"]
