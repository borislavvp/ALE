# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app/client

ARG ENV                   

COPY ["package.json", "./"]
COPY ["tsconfig.json", "./"]
RUN npm install

COPY ["./", "./"]
RUN ls -al
RUN npm run build:$ENV

FROM nginx:stable-alpine 
COPY --from=build-stage /app/client/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 8081
CMD ["nginx", "-g", "daemon off;"]