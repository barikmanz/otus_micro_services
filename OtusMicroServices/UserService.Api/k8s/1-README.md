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
1. Для реализации наката миграций в джобе до старта подов основного приложения используется образ k8s-wait-for (https://github.com/groundnuty/k8s-wait-for) в инит контайнере. Для корректной работы надо настроить доступы.
1. Настраиваем роль командой  kubectl create role pod-reader --verb=get --verb=list --verb=watch --resource=pods,services,deployments,jobs
2. Делаем связку роли kubectl create rolebinding default-pod-reader --role=pod-reader --serviceaccount=default:default --namespace=default
3. cd otus_micro_services\OtusMicroServices\charts\user-service
4. helm upgrade --install user-service-release . -f values.yaml

Ссылка на Api приложения http://arch.homework:8082/swagger/index.html
Файл с коллекцией Postman UserService.Api.postman_collection.json