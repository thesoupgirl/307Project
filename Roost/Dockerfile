FROM microsoft/aspnetcore:1.1.0

RUN adduser --disabled-password deployuser
USER deployuser

WORKDIR /Roost
COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Roost.dll
