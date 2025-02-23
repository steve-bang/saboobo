# Apply ingress

# Config nginx controller

``` bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/cloud/deploy.yaml
```

## Apply config ingress

``` bash
kubectl apply -f api.steve-bang.com.igress.yaml
```

## Config cloud flare

Log in to your Cloudflare Dashboard.
Select your domain from the dashboard.
Go to the DNS tab.
 - Add a new A record:
 - Type: A
 - Name: subdomain (e.g., app for app.yourdomain.com)
 - IPv4 Address: Your ingress controller's public IP - kubectl describe ingress <your-ingress> and see IP Address
 - Set Proxy Status to Proxied