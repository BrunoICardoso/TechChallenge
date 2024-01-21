Write-Host "Apply volumes e claims"
kubectl apply -f mssqlserver-pv.yaml
kubectl apply -f mssqlserver-pvc.yaml

Write-Host "Apply deployments e services"
kubectl apply -f mssqlserver-deployment.yaml
kubectl apply -f mssqlserver-svc.yaml

kubectl apply -f api-deployment.yaml
kubectl apply -f api-svc.yaml

kubectl apply -f apifakepayment-deployment.yaml
kubectl apply -f apifakepayment-svc.yaml

kubectl apply -f metricserver-deployment.yaml

Write-Host "Apply scale objects (HPA)"
kubectl apply -f api-scaleobject.yaml
kubectl apply -f apifakepayment-scaleobject.yaml

Write-Host "Os recursos do Kubernetes foram aplicados."
