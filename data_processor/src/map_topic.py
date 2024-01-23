"""
map_topic.py
============

This module provides functionality for mapping topics in a collection of projects
to predefined topics from a taxonomy. It uses cosine similarity between TF-IDF
vectors of project topics and predefined topics in the taxonomy.
"""


import json
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer


def get_taxonomy(file_path):
    """
    Retrieve and return the predefined taxonomy from a JSON file.

    Parameters
    ----------
    file_path : str
        The string containing the name of the file path

    Returns
    -------
    dict
        The dictionary containing the taxonomy data.
    """
    try:
        with open(file_path, 'r', encoding="utf-8") as file:
            taxonomy_data = json.load(file)
            return taxonomy_data
    except FileNotFoundError as e:
        print(f"File '{file_path}' not found.")
        return e
    except json.JSONDecodeError as e:
        print(f"Error decoding '{file_path}': {e}")
        return e


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
    taxonomy_data = get_taxonomy(file_path = "assets/taxonomy.json")
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
