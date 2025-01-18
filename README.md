# SaBooBo

A project for food store app with multiple Merchant join free. A simple microservice architecture.

# Getting Started
This version of eShop is based on .NET 9.
## Run project
Just running command:
``` bash
dotnet run --project src/AppHost/AppHost.csproj
```

Create az group
``` bash
az group create \
    -l southeastasia \
    --name saboobo-rg
```

Create aks
``` bash
az aks create \
    --resource-group saboobo-rg \
    --name saboobo-aks-cluster \
    --node-count 1 \
    --generate-ssh-keys
```

Connect todoapp-cluster node from local

``` bash
az aks get-credentials --resource-group saboobo-rg --name saboobo-aks-cluster
```

Apply deloy postgre sql product service
``` bash
kubectl apply -f ./k8s/postgredb.yaml
```