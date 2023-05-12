{{/*
Expand the name of the chart.
*/}}
{{- define "user-service-migrator.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "user-service-migrator.fullname" -}}
{{- if .Values.fullnameOverride }}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- $name := default .Chart.Name .Values.nameOverride }}
{{- if contains $name .Release.Name }}
{{- .Release.Name | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" }}
{{- end }}
{{- end }}
{{- end }}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "user-service-migrator.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Common labels
*/}}
{{- define "user-service-migrator.labels" -}}
helm.sh/chart: {{ include "user-service-migrator.chart" . }}
{{ include "user-service-migrator.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Selector labels
*/}}
{{- define "user-service-migrator.selectorLabels" -}}
app.kubernetes.io/name: {{ include "user-service-migrator.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

Generate chart secret name
*/}}
{{- define "user-service-migrator.secretName" -}}
{{ default (include "user-service-migrator.fullname" .) .Values.existingSecret }}
{{- end -}}

Generate chart configMap name
*/}}
{{- define "user-service-migrator.configMapName" -}}
{{ default (include "user-service-migrator.fullname" .) .Values.existingConfigMap }}
{{- end -}}
