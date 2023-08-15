Установка Postgress:
1. kubectl apply -f persistent-volume.yaml
2. kubectl apply -f persistent-volume-claim.yaml
3. helm repo add bitnami https://charts.bitnami.com/bitnami
4. helm repo update
5. helm install dev-pg bitnami/postgresql -f postgres_values_v2.yaml

Установка tls secret:
1. cd otus_micro_services/OtusMicroServices/UserService.Api/k8s
2. kubectl create secret tls arch-homework-tls --key=refcons.key.pem --cert=refcons.crt -n default

Установка стэка Prometheus:
1. cd OtusMicroServices/UserService.Api/k8s
2. helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
3. helm repo update
4. helm install -f prometeus_values.yaml prom-stack prometheus-community/kube-prometheus-stack
5. В файл host добавляем следующиезаписи 127.0.0.1 arch.homework   127.0.0.1 prometheus.arch.homework   127.0.0.1 alertmanager.arch.homework   127.0.0.1 grafana.arch.homework
6. Что бы браузер не ругался на selfsigned сертификат refcons.crt импортируем его в доверенные центры сертификации.

Установка nginx-ingress-controller:
1. helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx/
2. helm repo update
3. helm install nginx ingress-nginx/ingress-nginx -f nginx_ingress_controller_values.yaml

Установка Prometheus Postgres Exporter
1. cd OtusMicroServices/UserService.Api/k8s
2. helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
3. helm repo update
4. helm install -f prom_pg_exporter_values.yaml prom-pg-exporter prometheus-community/prometheus-postgres-exporter
5. Заходим в терминал пода Posgres
6. cd /opt/bitnami/scripts/postgresql/entrypoint.sh /bin/bash
7. psql
8. SELECT *
   FROM pg_available_extensions
   WHERE
   name = 'pg_stat_statements' and
   installed_version is not null;
9. If the table is empty, create the extension
10. CREATE EXTENSION pg_stat_statements;

Установка приложения User Service через helm:
1. Для реализации наката миграций в джобе до старта подов основного приложения используется образ k8s-wait-for (https://github.com/groundnuty/k8s-wait-for) в инит контайнере. Для корректной работы надо настроить доступы.
1. Настраиваем роль командой  kubectl create role pod-reader --verb=get --verb=list --verb=watch --resource=pods,services,deployments,jobs
2. Делаем связку роли kubectl create rolebinding default-pod-reader --role=pod-reader --serviceaccount=default:default --namespace=default
3. cd otus_micro_services\OtusMicroServices\charts\user-service
4. helm upgrade --install user-service-release . -f values.yaml

Ссылка на сваггер приложения http://arch.homework/swagger/index.html
Ссылка на метрики приложения http://arch.homework/metrics

Файл с коллекцией Postman UserService.Api.postman_collection.json
Файл с коллекцией запросов вызывающих ошибки Postman UserService.ApiErrorActions.postman_collection.json

Для подачи нагрузки на приложение запускаем в нескольких окнах консоли следующие команды (используется утилита newman для запуска коллекций Postman):
    newman run UserService.Api.postman_collection.json -n3000 --insecure    - для нагрузки с 200 кодами ответа
    newman run UserService.ApiErrorActions.postman_collection.json -n3000 --insecure    - для нагрузки с кодами 400-500 кодами ответа


docker build -f api.Dockerfile -t barikman/otus_hw_3_userservice:v2 .
docker push barikman/otus_hw_3_userservice:v2