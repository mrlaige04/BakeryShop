FROM node:22 as builder

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:alpine

WORKDIR /usr/share/nginx/html

COPY --from=builder /usr/src/app/dist/ .

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
