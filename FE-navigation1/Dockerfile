####################################
FROM node:20.8.1 AS dependencies

WORKDIR /app

COPY package*.json ./

RUN npm install

####################################
FROM node:20.8.1 AS build-stage

WORKDIR /app

COPY --from=dependencies /app /app

COPY ./src /app/src
COPY angular.json /app
COPY tsconfig.json /app
COPY tsconfig.app.json /app
COPY tsconfig.spec.json /app

RUN npm install -g @angular/cli
RUN ng build --configuration=docker

####################################
FROM nginx:1.24.0-alpine AS prod-stage
LABEL org.opencontainers.image.source=https://github.com/fiit-hipa/hipa-fe

COPY --from=build-stage app/dist/hipa-fe /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY ./start.sh /usr/local/bin/start.sh

RUN chmod +x /usr/local/bin/start.sh

EXPOSE 80
ENTRYPOINT ["/usr/local/bin/start.sh"]
