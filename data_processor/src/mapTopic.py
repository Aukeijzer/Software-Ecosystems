from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
import json

def get_taxonomy(ecosystem):
    file_path = 'assets/'  + ecosystem + '.json'
    try:
        with open(file_path, 'r') as file:
            taxonomy_data = json.load(file)
            return taxonomy_data
    except FileNotFoundError:
        print(f"File '{file_path}' not found.")
    except json.JSONDecodeError as e:
        print(f"Error decoding '{file_path}': {e}")

def flatten_taxonomy_to_strings(json_data):
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

def map_topics(projects, ecosystem):
    taxonomy_data = get_taxonomy(ecosystem)
    predefined_topics = flatten_taxonomy_to_strings(taxonomy_data)
    for project in projects:
        topics =  project["topics"]
        # Combine keywords into strings for each topic
        topic_texts = [' '.join(topic["keywords"]) for topic in topics]

        # Vectorize the combined keywords
        vectorizer = TfidfVectorizer()
        topic_vectors = vectorizer.fit_transform(topic_texts)

        # Calculate cosine similarity between each topic and predefined topics
        for i, topic_vector in enumerate(topic_vectors):
            similarities = cosine_similarity(topic_vector, vectorizer.transform(predefined_topics))
            most_similar_index = similarities.argmax()
            topics[i]["mapped_topic"] = predefined_topics[most_similar_index]

