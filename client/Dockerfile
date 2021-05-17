# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app/client

ARG ENV                   

COPY ["client/package.json", "./"]
COPY ["client/tsconfig.json", "./"]
RUN npm install

COPY ["client", "./"]
RUN ls -al
RUN npm run build:$ENV

FROM nginx:stable-alpine 
COPY --from=build-stage /app/client/dist /usr/share/nginx/html
EXPOSE 8081
CMD ["/bin/sh", "-c", "sed -i 's/listen  .*/listen 8081;/g' /etc/nginx/conf.d/default.conf && exec nginx -g 'daemon off;'"]