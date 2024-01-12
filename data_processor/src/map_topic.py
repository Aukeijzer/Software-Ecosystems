"""
Module: map_topic

This module provides functionality for mapping topics in a collection of
 projects to predefined topics
from a taxonomy. It uses cosine similarity between TF-IDF vectors of project
 topics and predefined topics in the taxonomy.

Functions:
- get_taxonomy: Retrieve and return the predefined taxonomy from a JSON file.
- flatten_taxonomy_to_strings: Flatten the structure of the taxonomy into a 
list of strings.
- map_topics: Map the topics in a collection of projects to the closest
 predefined topics in the taxonomy.
"""

import json
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer


# Retrieve taxonomy file
def get_taxonomy():
    """
    Retrieve and return the predefined taxonomy from a JSON file.

    Returns
    -------
    dict
        The dictionary containing the taxonomy data.
    """
    file_path = 'assets/taxonomy.json'
    try:
        with open(file_path, 'r', encoding="utf-8") as file:
            taxonomy_data = json.load(file)
            return taxonomy_data
    except FileNotFoundError:
        print(f"File '{file_path}' not found.")
    except json.JSONDecodeError as e:
        print(f"Error decoding '{file_path}': {e}")


# Flatten the taxonomy to a list of topics
def flatten_taxonomy_to_strings(json_data):
    """
    Flatten the nested structure of the taxonomy into a list of strings.

    Parameters
    ----------
    json_data : dict
        The dictionary containing the taxonomy data.

    Returns
    -------
    list
        A list of strings representing flattened topics from the taxonomy.
    """
    def flatten(obj):
        flattened = []

        def _flatten(node):
            for key, value in node.items():
                flattened.append(key)
                if value:
                    _flatten(value)

        _flatten(obj)
        return flattened

    flattened_values = flatten(json_data)
    return flattened_values


# Map the found topics to the topics in the taxonomy
def map_topics(projects):
    """
    Map the topics in a collection of projects to the closest predefined topics
      in the taxonomy.

    Parameters
    ----------
    projects : list 
        A list of dictionaries, each representing a project with
        associated topics.

    Returns
    -------
    list: 
        A list of dictionaries, each containing mapped topics for the
        corresponding project.
    """
    # Get predefined topics
    taxonomy_data = get_taxonomy()
    predefined_topics = flatten_taxonomy_to_strings(taxonomy_data)
    results = []
    for project in projects:
        topics = project["topics"]
        result = {}
        # Combine keywords into strings for each topic
        topic_texts = [' '.join(topic["keywords"]) for topic in topics]

        # Vectorize the combined keywords
        vectorizer = TfidfVectorizer()
        topic_vectors = vectorizer.fit_transform(topic_texts)

        # Calculate cosine similarity between each topic and predefined topics
        mapped_topics = []
        for topic_vector in topic_vectors:
            similarities = cosine_similarity(
                topic_vector,
                vectorizer.transform(predefined_topics))
            most_similar_index = similarities.argmax()
            mapped_topics.append(predefined_topics[most_similar_index])

        result["topics"] = mapped_topics
        results.append(result)

    return results
