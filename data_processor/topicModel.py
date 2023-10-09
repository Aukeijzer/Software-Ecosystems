from bertopic import BERTopic
from bertopic.representation import KeyBERTInspired
from sklearn.feature_extraction.text import CountVectorizer

# Extracting the topics from a list of preprocessed readmes
def extractTopics(preprocessedDocs):
    # Fit model on readmes
    rm = KeyBERTInspired()
    vectorizer_model = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    model = BERTopic(representation_model=rm, nr_topics=5, top_n_words=5, verbose=True, min_topic_size=3, vectorizer_model=vectorizer_model)
    _ = model.fit_transform(preprocessedDocs)

    # Extract topics from the model
    info = model.get_document_info(preprocessedDocs)
    topic_info = info[['Top_n_words', 'Topic', 'Probability']].copy()
    keyword_info = topic_info.rename(columns={'Top_n_words': 'Keywords'})
    json_str = keyword_info.to_dict(orient='records')

    return json_str
