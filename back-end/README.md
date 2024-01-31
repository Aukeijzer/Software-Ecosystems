[Go to back-end documentation](https://aukeijzer.github.io/Software-Ecosystems/documentation/backend/index.html)

Typically a 'back-end' in a project is viewed as everything which happens behind the scenes. From this perspective other parts of the project, such as the dataprocessor and spider can both be seen as back-end. **In our project we use a different definition for back-end.** We see the back-end as the application which manages communication between applications. All applications only communicate with the back-end. As such the back-end can be seen as the glue which holds everything together.

The back-end consists of a .NET application which keeps a connection with a SQL and a Elasticsearch database.

The application runs on a postgres