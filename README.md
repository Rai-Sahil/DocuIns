# DocuIns

## How to Run the App

> Verify that you don't have any other container already running in docker. If a container is running, remove it.

Publish the App to local machine, to get the .dll file of the project. Keep in mind to name it as 'dist'.
```
dotnet publish -o dist
```

Now turn on your docker desktop.
At last run the docker-compose.yml, using
```
docker-compose up
```
