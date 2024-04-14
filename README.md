# SECODash documentation
<p align="center"><img width="133" height="169" src="assets/SECODash_logo.png"></p>

## SECODash
SECODash (Software-Ecosystems) is a web-application which gathers, analyses and displays ecosystems. It was create as a Computer Science Bachelor Thesis at Utrecht University by the project group ODINDash.

### ODINDash Members
- Akdimi, Y. (Youssra)
- Boucher, L.C. (Lucas)
- Hendrix, M.T.A.M. (Matean)
- Hillebrand, D. (Daan)
- Hol, S. (Simon)
- Kopp, Q.F. (Quentin)
- Ning, V.Y. (Vivian)
- Roest, A. (Auke)
- Uunen, I.R. van (Ian)


## Client
The SECODash project has been commissioned by [Dr. R.L. (Slinger) Jansen](https://www.uu.nl/medewerkers/RLJansen) and [Dr. S. (Siamak) Farshidi](https://www.uu.nl/medewerkers/SFarshidi) from the Department of Information and Computer Science at Utrecht University.

# Open-Source 
<p align="center"><img width="155" height="51" src="assets/agplv3.png"></p>

SECODash is an Open-Source project and is released under the [GNU Affero General Public License](LICENSE). For more information on the The GNU Affero General Public License visit [www.gnu.org/licenses/agpl-3.0.en.html](https://www.gnu.org/licenses/agpl-3.0.en.html).

# Running SECODash
SECODash is fully containerized with Docker. As such it is portable and easy to run. For installation you only need to download [Docker](https://docs.docker.com/get-docker/) to be able to run the project. 

## Setup and Secrets
This project has some private information which is not included "out-of-the-box". As such we need to add this information manually, namely.

- elasticsearch .env file
- back-end connection strings
- SSL certificate and private key
- github personal access token
- front-end .env files

Most of this information is in a directory called `secrets`. The enviroment files must be placed in their respective project folders.

Furthermore in order to run the project we need to setup connection with a elasticsearch database. Either locally or remote we have included instructions to do this correctly below.

### Setup elasticsearch database connection
We can either use a remote database or set one up locally. For our project we have use a local elasticsearch database for production and a remote elasticsearch database for development. 

If you use a remote database connection you can connect to it by using the **cloud id** of the deployment and by generating a **api-key**. Fill both of these values in the [backend-secrets.json](/README.md#backend-connectionstringsjson-outside-docker) secrets file and you are done.

Running the elasticsearch database locally uses the template given by the [documentation](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html) of elasticsearch. First need to make sure you have set [vm_max_map to atleast 262144](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html#_set_vm_max_map_count_to_at_least_262144). After that you make a file `.env` in the `elasticsearch` directory and paste the following contents in the file.
``` conf
# Password for the 'elastic' user (at least 6 characters)
ELASTIC_PASSWORD=<elastic_password>

# Password for the 'kibana_system' user (at least 6 characters)
KIBANA_PASSWORD=<kibana_password>

# Version of Elastic products
STACK_VERSION=8.13.0

# Set the cluster name
CLUSTER_NAME=docker-cluster

# Set to 'basic' or 'trial' to automatically start the 30-day trial
LICENSE=basic
#LICENSE=trial

# Port to expose Elasticsearch HTTP API to the host
#ES_PORT=9200
ES_PORT=127.0.0.1:9200

# Port to expose Kibana to the host
KIBANA_PORT=5601
#KIBANA_PORT=80

# Increase or decrease based on the available host memory (in bytes)
MEM_LIMIT=1073741824

# Project namespace (defaults to the current folder name if not set)
#COMPOSE_PROJECT_NAME=myproject
```
Be sure to set the elastic_search password and kibana_password to something else.

After that you can start the elasticsearch database. To do this run the batch file in the `elasticsearch` directory. This starts up the elasticsearch database. If you can't run batch files in your machine you can also just copy paste all commands in the terminal. The last command will display the ssl sha256-thumbprint needed to connect to the database. Be sure to also enter the thumbprint and elastic_password in the [backend-secrets.json](README.md#backend-secretsjson-in-docker).

### Backend connection-strings
In order to keep the database passwords private the connection strings the back-end uses have been put in the secrets file. This normally would be the `appsettings.json` file. We use 2 versions of this file. `backend-secrets.json` for running the back-end in a docker container and `backend-secrets.json` for running it in a docker container.

#### backend-secrets.json (In docker)
``` json
{
  "ConnectionStrings": {
    "DevelopmentDb": "Server=0.0.0.0;Port=5432;Host=db;Database=postgres;Username=postgres;Password=<postgress-password>",
    "Spider": "http://spider-app:5205/Spider",
    "DataProcessor" : "http://data-processor-app:5000",
    "Hangfire" : "Server=0.0.0.0;Port=5432;Host=db; Database=hangfire; Username=postgres; Password=<postgress-password>"
   },
  "Elasticsearch": {
    "Password": "<elastic-password>",
    "Fingerprint": "<fingerprint>",
    "Nodes": [
      "https://<elastic-container-name-1>:9200"
      "https://<elastic-container-name-2>:9200"
      "https://<elastic-container-name-3>:9200"
    ]
  }
}
```
#### backend-connectionstrings.json (Outside docker)
``` json
{
  "ConnectionStrings": {
    "DevelopmentDb" : "Server=localhost; Database=develop; Port=5432; User Id=postgres; Password=<postgress-password>",
    "Spider": "http://localhost:5205/Spider",
    "DataProcessor": "http://localhost:5000",
    "Hangfire" : "Server=localhost; Database=hangfire; Port=5432; User Id=postgres; Password=<postgress-password>"
  },
  "Elasticsearch": {
    "CloudId": "<cloudid>",
    "ApiKey": "<apikey>"
  }
}
```

Some notes:
- \<postgress-password\> is the password used for the postgres sql database. You also need to make a file called `postgres-password.txt` with the same value so the database container knows which password it has to use.
- These files assume you use a remote elasticsearch connection for running the project outside of docker and a local elasticsearch for running the project inside docker. If you want to use a local database for running inside a docker container or vice versa just use the *"Elasticsearch"* attribute of the other file. [Click here to see how to setup a elasticsearch database](Readme.md#setup-elasticsearch-database-connection)

### SSL Certificates
Creating personal certificates isn't nescessary, but it allows the application to run in https in the browser. Create a folder called `certs` and put the certificate and private key into it with the names `fullchain.pem` and `privkey.pem`. If you make a personal certificale make sure it is trusted by your machine by installing it.

### Personal access token
Get a [personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens) from github and paste it into a file named `spider-git-api-token.txt`. Do note that github does limit how many requests you can make to its api. So it is not recommended to do this with any active account. Also note that using multiple api token's to exceed github's rate limit is against [Terms of Service](https://docs.github.com/en/site-policy/github-terms/github-terms-of-service#h-api-terms).

### Front-end enviroment files
The front-end has 2 enviroment files. `.env.development` is used for running the project outside of Docker and `.env.production` is used for running the project in Docker. The templates for both files have been given below.
#### .env.development
``` conf
NEXT_PUBLIC_LOCAL_ADRESS=http://localhost
NEXT_PUBLIC_BACKEND_ADRESS=http://localhost:5003

NEXTAUTH_URL=http://localhost:3000

NEXTAUTH_SECRET=

GITHUB_CLIENT_ID=
GITHUB_CLIENT_SECRET=

GOOGLE_CLIENT_SECRET=
GOOGLE_CLIENT_ID=
```

#### .env.production
``` conf
NEXT_PUBLIC_LOCAL_ADRESS=http://localhost
NEXT_PUBLIC_BACKEND_ADRESS=http://backend-app:5003

NEXTAUTH_URL=http://localhost:3000

NEXTAUTH_SECRET=

GITHUB_CLIENT_ID=
GITHUB_CLIENT_SECRET=

GOOGLE_CLIENT_SECRET=
GOOGLE_CLIENT_ID=
```

## Running locally

Once all setup is done (if you are using a local elasticsearch database make sure its running) the project can be run by running batch file run.bat in the project folder. Below we have outlined how it works.

When in Docker applications use the docker network to communicate instead of ports on localhost. So before we start up our project this the network needs to be created first.

`$ docker network create secodash-network`

Next we compose the project.

`$ docker compose up --build`

This builds all our applications into images which will then be ran as containers ([Docker documentation](https://docs.docker.com/guides/get-started/)). Building the images can take a while (10 minutes), however this only needs to be done once. Running the images as containers will only take a few seconds. In this command the `--build` flag is optional. However it will make Docker rebuild an image if you change the source code.

After this you have the project running. You can find the application at localhost and use Docker Desktop to inspect the containers.

## Running in production

If you want to run docker on a remote server you need to install the Docker deamon there first (you do **not** need docker desktop). Afterwards you need to create a Docker network and compose the project like you do locally.

The docker compose file we use locally has some extra development features which aren't needed in a production enviroment. As such we use a different compose file for running the project in a production enviroment.

`$ docker compose -f server-compose.yml up --build`

If you intend to continue developping this project it is recommended to be able to run everything outside of docker.

# Overview of All Applications

The structure of the project is made up of 4 different branches.
- Front-end
- Back-end
- Spider
- Data processor

## Back-end (service)

[Go to back-end documentation](https://aukeijzer.github.io/Software-Ecosystems/documentation/backend/index.html)

![UML diagram of the back-end](assets/backendUML.png)

Typically the 'back-end' in a project is viewed as everything which happens server-side. With this definition other parts of the project, such as the dataprocessor and spider are technically both back-end. **In our project we use a different definition for back-end.** We see the back-end as the *application* which manages communication between applications all other applications. Most applications send and recieve information soley from the back-end application. Furthermore the back-end is responsible for managing the databases.

The back-end consists of a .NET application which keeps a connection with a SQL and a Elasticsearch database.

### Elasticsearch database
TODO
### Postgres database
TODO

### Running outside of an container
In order to run the spider you need to install [C# .net 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) and [PostgreSQL](https://www.postgresql.org/download/). While installing postgress it is recommended to also install the database-browser pgadmin4 via the installer. Be sure you have set up the [connection string](README.md#backend-connection-strings) properly.

Afterwards you can open the project solution in your IDE (visual studio or rider) and run it using the http profile. 

## Front-end

[Go to front-end documentation](front-end/README.md)

The front-end uses

### Nginx
Nginx is used as a reverse proxy to handle ssl certification so the application can run on https. It just handles ssl certification and passes all requests on to the nodejs server. We have a config file to run Nginx in a docker container located at front-end/nginx,

### Next.js

Next.js is a React framework that simplifies the process of building server-side rendered (SSR) and statically generated web applications. It is designed to enhance the developer experience by providing a set of conventions and tools for quickly creating robust and scalable web applications.


### Running outside of a container
Outside of a container we just run the Next.js on http so we do not need Nginx. In order to use Next.js we first need to download [Node.js and npm](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm). 

1. Open the project folder to frontend/ecodash
2. Open a new terminal in the folder
3. Run the following command "npm ci". This chain installs all required packages
4. to run the front-end in development mode excecute: `npm run dev`
5. To run the front-end in production mode execute: `npm run build` followed by `npm start`

Also be sure you have added the [eviroment files](README.md#front-end-enviroment-files).

### Testing
Front-end testing is doen with Cypress and Jest. Cypress is used for component testing and e2e tests. Jest is used for all seperate functions.

#### Cypress testing
To start testing with cypress follow these steps:
1. Open the projet folder to frontend/ecodash
2. Open a new terminal
3. Run the following command: `npm run dev`
4. Open a second new terminal
5. Run the following command: `npx cypress open`
6. This opens up a new window with two options: e2e testing / component
7. Click on the chosen option
8. Click specs and click on the green start icon to run the selected tests

#### Jest testing
To start testing with jest follow these steps:
1. Open the project folder to `frontend/ecodash`
2. Open a new terminal
3. Run the following command: `npm run test`
4. Supply the command with the following flag `--verbose` to see additional test information

## Spider

### Spider functionality
The Spiders job is to mine the repositories of of GitHub. It does this using the GitHub Rest and GraphQL apis.

### Running outside of a container
In order to run the spider you need to install [C# .net 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). 

Afterwards you can open the project solution in your IDE (visual studio or rider) and run the project using the http profile.

### Documentation
[Go to spider documentation](https://aukeijzer.github.io/Software-Ecosystems/documentation/spider/index.html)

## Data Processor
[Go to data processor documentation](data_processor/README.md)

### Running outside of a container
#### Step 1: Create and Activate a Virtual Environment (please make sure python==3.9 is installed)*

Once you have installed python you need to open the data_processor directory in the commandline and install all dependencies

`$ python -m venv .venv`

`$ .venv\Scripts\activate`

#### Step 2: Install Dependencies

After activating the virtual environment, install project dependencies from `requirements.txt`:

`$ pip install -r requirements.txt`

#### Step 3: Run the Application

Once dependencies are installed run the `app.py` file in src directory:

`$ python src/app.py`

In order to run the dataprocessor you need to [install python](https://www.python.org/downloads/release/python-390/). Make sure you install version 3.9. **Other versions are not supported.** 

### Running tests

#### Step 1: Set Up Environment
Follow all previous steps to run data processor outside of a containter

#### Step 2: Run Tests
Run all tests using the following command:

`$ python -m unittest discover -s tests -p 'test_*.py'`
