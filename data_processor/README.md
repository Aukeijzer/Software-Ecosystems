# Data Processor Readme

For more information to get started with the data processor go to [project readme](../README.md/#data%20processor). 

## Modules
- [Application](#application-module-apppy)
- [Map Topic](#map-topic-module-map_topicpy)
- [Preprocessing](#preprocessing-module-preprocessingpy)
- [Topic Model](#topic-model-module-topic_modelpy)
- [Topic Service](#topic-service-module-topic_servicepy)

## Application Module: `app.py`

This module defines a Flask application for extracting topics from preprocessed data.

### `app.extract_topics()`

Extract topics from the preprocessed data.

- **Parameters:**
  - `data` (body, required): 
    - Type: array
      - Items:
        - Type: object
          - Properties:
            - `id`:
              - Type: string
            - `name`:
              - Type: string
            - `description`:
              - Type: string
            - `readme`:
              - Type: string

- **Responses:**
  - `200`:
    - Description: Topics extracted successfully
    - Schema:
      - Type: object
        - Properties:
          - `result`:
            - Type: string
  - `500`:
    - Description: Internal Server Error
    - Schema:
      - Type: object
        - Properties:
          - `error`:
            - Type: string

- **Returns:**
  - Type: list

### `app.serve_sphinx_docs(path='index.html')`

Shows documentation when starting the application.

- **Parameters:**
  - `path` (str, optional): The path to the static file, by default ‘index.html’.

- **Returns:**
  - Type: Response

## Map Topic Module: `map_topic.py`

This module provides functionality for mapping topics in a collection of projects to predefined topics from a taxonomy. It uses cosine similarity between TF-IDF vectors of project topics and predefined topics in the taxonomy.

### `map_topic.flatten_taxonomy_to_strings(json_data)`

Flatten the nested structure of the taxonomy into a list of strings.

- **Parameters:**
  - `json_data` (dict): The dictionary containing the taxonomy data.

- **Returns:**
  - Type: list

### `map_topic.get_taxonomy(file_path)`

Retrieve and return the predefined taxonomy from a JSON file.

- **Parameters:**
  - `file_path` (str): The string containing the name of the file path

- **Returns:**
  - Type: dict

### `map_topic.map_topics(projects)`

Map the topics in a collection of projects to the closest predefined topics in the taxonomy.

- **Parameters:**
  - `projects` (list): A list of dictionaries, each representing a project with associated topics.

- **Returns:**
  - Type: list

## Preprocessing Module: `preprocessing.py`

This module provides functions for preprocessing text data, including the removal of Markdown layout, tokenization, removal of punctuation and non-alphabetic characters, and lemmatization.

### `preprocessing.preprocess_docs(docs)`

Preprocess a list of documents using the `preprocess_document` function.

- **Parameters:**
  - `docs` (list): A list of strings representing documents.

- **Returns:**
  - Type: list

### `preprocessing.preprocess_document(document)`

Preprocess a single document by removing Markdown layout, tokenizing text, removing punctuation and non-alphabetic characters, and lemmatizing words.

- **Parameters:**
  - `document` (str): The input document as a string.

- **Returns:**
  - Type: str

## Topic Model Module: `topic_model.py`

This module provides functions for extracting topics from a collection of preprocessed documents using Latent Dirichlet Allocation (LDA) and finding the optimal number of topics.

### `topic_model.extract_topics_lda(documents, top_x_topics)`

Extract topics from a collection of preprocessed documents using Latent Dirichlet Allocation (LDA).

- **Parameters:**
  - `documents` (list): A list of preprocessed documents.
  - `top_x_topics` (int): Number of top topics to extract for each document.

- **Returns:**
  - Type: list

### `topic_model.find_optimal_num_topics(documents)`

Find the optimal number of topics using model perplexity.

- **Parameters:**
  - `documents` (list): List of preprocessed documents.

- **Returns:**
  - Type: int

## Topic Service Module: `topic_service.py`

This module provides a service class for extracting and mapping topics from a list of projects’ data.

### Class: `topic_service.TopicService(data)`

A class for extracting and mapping topics from a list of projects data.

- `data`:
  - Type: list

### `topic_service.extract_topics()`

Extract topics from the project data using preprocessing, topic modeling, and topic mapping.

- **Returns:**
  - Type: list

### `topic_service.get_data(data)`

Extract document information and ids from JSON data.

- **Parameters:**
  - `data` (list): A list of dictionaries representing project data.

- **Returns:**
  - Type: tuple
    - A tuple containing a list of ids and a list of document information.
