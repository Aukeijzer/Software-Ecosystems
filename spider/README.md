## Spider

### Spider functionality
The Spiders job is to mine the repositories of of GitHub. It does this using the GitHub Rest and GraphQL apis.

### Running outside of a container
In order to run the spider you need to install [C# .net 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). 

Because the spider also needs a github personal access token to run, which docker usually provides. You also need to add a GitHub api token to a folder named secrets in the repository root. The file should be a .txt file named "spider-git-api-token.txt"

Afterwards you can open the project solution in your IDE (visual studio or rider) and run the project using the http profile.

### Documentation
[Go to spider documentation](https://aukeijzer.github.io/Software-Ecosystems/documentation/spider/index.html)