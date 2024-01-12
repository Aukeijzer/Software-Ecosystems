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

from sklearn.feature_extraction.text import CountVectorizer
from sklearn.decomposition import LatentDirichletAllocation
import numpy as np


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
    # Initialize vectorizer model
    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    matrix = vectorizer.fit_transform(documents)

    # Initialize lda model
    lda = LatentDirichletAllocation(
        n_components=find_optimal_num_topics(documents),
        random_state=42,
        doc_topic_prior=1,
        topic_word_prior=1)

    lda.fit(matrix)

    # Extract top N keywords for each document
    feature_names = vectorizer.get_feature_names_out()
    top_n_words = 10
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
    end = len(documents)
    step = max(1, int(end/10))

    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    X = vectorizer.fit_transform(documents)

    perplexity_values = []
    for num_topics in range(start, end + 1, step):
        lda = LatentDirichletAllocation(n_components=num_topics,
                                        random_state=42)
        lda.fit(X)

        # Calculate perplexity
        perplexity = lda.perplexity(X)
        perplexity_values.append(perplexity)

    # Find the index of the minimum perplexity
    optimal_num_topics = start + step * np.argmin(perplexity_values)

    return optimal_num_topics
