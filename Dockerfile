FROM nginx
WORKDIR /etc/nginx
COPY nginx.conf conf.d/default.conf
EXPOSE 8081