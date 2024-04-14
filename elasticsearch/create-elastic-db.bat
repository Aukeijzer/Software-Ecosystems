docker network create secodash-network
docker compose up -d --build
docker network connect secodash-network elasticsearch-es01-1
docker network connect secodash-network elasticsearch-es02-1
docker network connect secodash-network elasticsearch-es03-1
openssl x509 -noout -fingerprint -sha256 -inform pem -in ./certs/es01/es01.crt
pause