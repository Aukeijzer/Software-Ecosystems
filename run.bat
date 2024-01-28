docker network create secodash-network
docker compose -f elasticsearch/docker-compose.yml up --build
docker compose -f docker-compose.yml up --build
PAUSE