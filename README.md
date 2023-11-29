# DocuIns

## How to Run the App

> Verify that you don't have any other container already running in docker. If a container is running, remove it.


```
docker run --cap-add SYS_PTRACE -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=SqlPassword! -p 1444:1433 --name azsql -d mcr.microsoft.com/azure-sql-edge
```