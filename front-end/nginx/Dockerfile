# nginx/Dockerfile

FROM nginx:1.23.3-alpine

# possibly the file should be copied to /etc/nginx/etc/ instead of the root.
# However NGINX somehow won't be able to find them that way.
COPY localhost.crt /localhost.crt 
COPY localhost.key /localhost.key 

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
EXPOSE 443