from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
import json

# Retrieve taxonomy file
def get_taxonomy():
    file_path = 'assets/taxonomy.json'
    try:
        with open(file_path, 'r') as file:
            taxonomy_data = json.load(file)
            return taxonomy_data
    except FileNotFoundError:
        print(f"File '{file_path}' not found.")
    except json.JSONDecodeError as e:
        print(f"Error decoding '{file_path}': {e}")

# Flatten the taxonomy to a list of topics
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

# Map the found topics to the topics in the taxonomy
def map_topics(projects):
    # Get predefined topics
    taxonomy_data = get_taxonomy()
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

