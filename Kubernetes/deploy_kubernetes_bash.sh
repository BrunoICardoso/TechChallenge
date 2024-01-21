#!/bin/bash

export PATH=$PATH:/c/Windows/system32

echo "Apply volumes e claims"
"$kubectl" apply -f mssqlserver-pv.yaml
"$kubectl" apply -f mssqlserver-pvc.yaml

echo "Apply deployments e services"
"$kubectl" apply -f mssqlserver-deployment.yaml
"$kubectl" apply -f mssqlserver-svc.yaml

"$kubectl" apply -f api-deployment.yaml
"$kubectl" apply -f api-svc.yaml

"$kubectl" apply -f apifakepayment-deployment.yaml
"$kubectl" apply -f apifakepayment-svc.yaml

"$kubectl" apply -f metricserver-deployment.yaml

echo "Apply scale objects (HPA)"
"$kubectl" apply -f api-scaleobject.yaml
"$kubectl" apply -f apifakepayment-scaleobject.yaml

echo "Os recursos do Kubernetes foram aplicados."