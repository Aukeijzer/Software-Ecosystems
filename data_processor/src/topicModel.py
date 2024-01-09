from sklearn.feature_extraction.text import CountVectorizer
from sklearn.decomposition import LatentDirichletAllocation
from sklearn.feature_extraction.text import CountVectorizer
import numpy as np
import math 

def extractTopicsLDA(preprocessedDocs):
    # Define top X 
    top_X_topics = math.ceil(len(preprocessedDocs)/5)

    # Initialize vectorizer model 
    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    X = vectorizer.fit_transform(preprocessedDocs)
    
    # Initialize lda model 
    lda = LatentDirichletAllocation(n_components=5, random_state=42, doc_topic_prior=1, topic_word_prior=1)
    lda.fit(X)

    # Extract top N keywords for each document
    feature_names = vectorizer.get_feature_names_out()
    top_n_words = 10
    topics = []

    for doc in preprocessedDocs:
         # Get probabilities for each topic per document
        probabilities = lda.transform(vectorizer.transform([doc]))[0]
        top_topic_indexes = np.argsort(probabilities)[::-1][:top_X_topics]
        
        doc_topics = []
        for topic_idx in top_topic_indexes:
            top_feature_indexes = lda.components_[topic_idx].argsort()[:-top_n_words - 1:-1]
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