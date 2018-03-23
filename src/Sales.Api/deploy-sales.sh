dotnet publish
scp bin/Debug/netcoreapp2.0/publish/* ubuntu@$1.compute-1.amazonaws.com:~/Sales.Api
