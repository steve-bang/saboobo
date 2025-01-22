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

# Work with Azure Storage class, PV, PVC

## Setup

Create a storage account  azure
``` bash
az storage account create -n sabboboac -g saboobo-rg -l southeastasia --sku Standard_LRS
```

Export the connection string as an environment variable using the following command, which you use to create the file share.
``` bash
export AZURE_STORAGE_CONNECTION_STRING=$(az storage account show-connection-string -n sabboboac -g saboobo-rg -o tsv)
```

Create the file share. Make sure to replace shareName with your share name.
``` bash
az storage share create -n saboobo-azfile --connection-string $AZURE_STORAGE_CONNECTION_STRING
```

Export the storage account key as an environment variable using the following command.
``` bash
STORAGE_KEY=$(az storage account keys list --resource-group saboobo-rg --account-name sabboboac --query "[0].value" -o tsv)
```

Echo the storage account name and key using the following command. Copy this information, as you need these values when creating the Kubernetes volume.
``` bash
echo Storage account key: $STORAGE_KEY
```

Output like: Storage account key: aoWxIoVqN95uKysanGGrz7z*****************

## Create a Kubernetes secret az

Create the secret in `k8s\azure\az-file-secret.yaml`

 ``` bash
kubectl apply -f ./k8s/azure/az-file-secret.yaml
```

Apply PV

 ``` bash
kubectl apply -f ./k8s/azure/az-file-sc.yaml
```

 ``` bash
kubectl apply -f ./k8s/azure/az-file-pv.yaml
```

 ``` bash
kubectl apply -f ./k8s/azure/az-file-pvc.yaml
```


# Deploy db postgres sql
``` bash
kubectl apply -f ./k8s/pg-product.yaml
```

``` bash
kubectl apply -f ./k8s/pg-admin-product.yaml
```

