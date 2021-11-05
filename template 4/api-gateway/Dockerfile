FROM mcr.microsoft.com/dotnet/sdk:5.0
LABEL maintainer="Eggert Mar Eggertsson"
WORKDIR /app
LABEL version="1.0.0"
# RUN dotnet publish -c Release -o /app
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS = http://*:5000;https://*:5001
COPY . .
RUN dotnet restore
RUN dotnet dev-certs https 
WORKDIR /app/JustTradeIt.Software.API
CMD ["dotnet", "run"]