Установка Postgress:

1. kubectl apply -f persistent-volume.yaml
2. kubectl apply -f persistent-volume-claim.yaml
3. helm repo add bitnami https://charts.bitnami.com/bitnami
4. helm repo update
5. helm install dev-pg bitnami/postgresql -f postgres_values.yaml

Установка nginx-ingress-controller:
1. helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx/
2. helm repo update
3. helm install nginx ingress-nginx/ingress-nginx -f nginx_ingress.yaml

Установка приложения User Service через helm:
1. cd otus_micro_services\OtusMicroServices\charts\user-service
2. helm upgrade --install user-service-release . -f values.yaml

Установка приложения User Service через kubectl:
1. kubectl apply -f configmap.yaml
2. kubectl apply -f secrets.yaml
3. kubectl apply -f deployment.yaml
4. kubectl apply -f service.yaml
5. kubectl apply -f ingress.yaml

Ссылка на Api приложения http://arch.homework:8082/swagger/index.html