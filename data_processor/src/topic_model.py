"""
Module: topic_model

This module provides functions for extracting topics from a collection of
 preprocessed documents
using Latent Dirichlet Allocation (LDA) and finding the optimal number of
 topics.

Functions:
- extract_topics_lda: Extract topics from a collection of preprocessed documents
 using LDA.
- find_optimal_num_topics: Find the optimal number of topics using model
 perplexity.
"""

import numpy as np
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.decomposition import LatentDirichletAllocation
from bertopic.representation import KeyBERTInspired
from bertopic import BERTopic
from map_topic import flatten_taxonomy_to_strings, get_taxonomy

def extract_topics_lda(documents, top_x_topics):
    """
    Extract topics from a collection of preprocessed documents using Latent
      Dirichlet Allocation (LDA).

    Parameters
    ----------
    documents : list
        A list of preprocessed documents.
    top_x_topics : int
        Number of top topics to extract for each document.

    Returns
    -------
    list
        A list of dictionaries, each containing extracted topics for the
          corresponding document.
    """

    # Initialize lda model
    lda = LatentDirichletAllocation(
        n_components=25,
        random_state=42,
    )

    # Initialize vectorizer model
    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    matrix = vectorizer.fit_transform(documents)

    # Use the pre-trained lda_model
    lda.fit(matrix)

    # Extract top N keywords for each document
    feature_names = vectorizer.get_feature_names_out()
    top_n_words = 5
    topics = []

    for doc in documents:
        # Get probabilities for each topic per document
        probabilities = lda.transform(vectorizer.transform([doc]))[0]
        top_topic_indexes = np.argsort(probabilities)[::-1][:top_x_topics]

        doc_topics = []
        for topic_idx in top_topic_indexes:
            top_feature_indexes = lda.components_[topic_idx].argsort()
            top_feature_indexes = top_feature_indexes[:-top_n_words - 1:-1]
            top_features = [feature_names[i] for i in top_feature_indexes]

            # Get the probability from the probabilities array
            topic_probability = probabilities[topic_idx]
            threshold = 0.01 # need to experiment with this value
            if topic_probability > threshold:
                topic_info = {
                    'keywords': top_features,
                    'topicId': int(topic_idx),
                    'probability': float(topic_probability)
                }

                doc_topics.append(topic_info)

        # Append the topics to the corresponding document
        topics.append({
            "topics": doc_topics
        })
    return topics

def extract_topics_bertopic(documents, top_x_topics):
    """
    Extract topics from a collection of preprocessed documents using BERTopic.

    Parameters
    ----------
    documents : list
        A list of preprocessed documents.
    top_x_topics : int
        Number of top topics to extract for each document.

    Returns
    -------
    list
        A list of dictionaries, each containing extracted topics for the corresponding document.
    """
    # Retrieve predefined topics from taxonomy
    taxonomy_data = get_taxonomy(file_path = "assets/taxonomy.json")
    predefined_topics = flatten_taxonomy_to_strings(taxonomy_data)

    # Initialize topic model
    topic_model = BERTopic(min_topic_size=2,
                           embedding_model="all-mpnet-base-v2",
                           calculate_probabilities=True,
                           vectorizer_model=CountVectorizer(),
                           representation_model=KeyBERTInspired(),
                           zeroshot_topic_list=predefined_topics,
                           zeroshot_min_similarity=0.0).fit(documents)

    # Extract topics and probability distribution
    topics , topic_distr = topic_model.transform(documents)

    # Extract topics names
    topic_names = topic_model.get_topic_info()["Name"].tolist()

    # Extract topics, probabilities and names foreach document
    extracted_topics = []
    for i, _ in enumerate(topics):
        doc_topics = []
        probabilities = topic_distr[i]
        top_indices = np.argsort(probabilities)[::-1][:top_x_topics]
        for idx in top_indices:
            if float(probabilities[idx]) > 0.5:
                doc_topic = {}
                # Uncomment if you want to see topic information in response
                # doc_topic["topic_id"] = int(idx)
                # doc_topic["probability"] = float(probabilities[idx])

                # topic_keywords = topic_model.get_topic(idx)
                # keywords = [item[0] for item in topic_keywords]
                # doc_topic["keywords"] = keywords
                doc_topic["name"] = topic_names[idx]
                doc_topics.append(topic_names[idx])

        # Append the topics to the corresponding document
        extracted_topics.append({
            "topics": list(set(doc_topics))
        })

    return extracted_topics

def find_optimal_num_topics(documents):
    """
    Find the optimal number of topics using model perplexity.

    Parameters
    ----------
    documents : list
        List of preprocessed documents.

    Returns
    -------
    int
        Optimal number of topics.
    """

    start = 2
    end = 75
    step = 10

    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    matrix = vectorizer.fit_transform(documents)

    perplexity_values = []
    for num_topics in range(start, end + 1, step):
        lda = LatentDirichletAllocation(n_components=num_topics,
                                        random_state=42)
        lda.fit(matrix)

        # Calculate perplexity
        perplexity = lda.perplexity(matrix)
        perplexity_values.append(perplexity)

    print(perplexity_values)
    # Find the index of the minimum perplexity
    optimal_num_topics = start + step * np.argmin(perplexity_values)

    return optimal_num_topics
