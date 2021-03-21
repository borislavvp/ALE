# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app/client
COPY ["client/package.json", "./"]
COPY ["client/tsconfig.json", "./"]
RUN npm install

COPY ["client", "./"]
RUN ls -al
RUN npm run build

FROM nginx:stable-alpine 
COPY --from=build-stage /app/client/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]