from bertopic import BERTopic
from bertopic.representation import KeyBERTInspired

# Not sure about this
def extractTopics(preprocessedDocs):
    rm = KeyBERTInspired()
    model = BERTopic(representation_model=rm, nr_topics=5, top_n_words=5, verbose=True, min_topic_size=3)
    _ = model.fit_transform(preprocessedDocs)
    topics = model.get_topics()

    # put topics and keywords in JSON file
    responseTopics = {"topics" : []}
    for key in topics:
        responseKeywords = {"keywords": []}
        for keyword, prob in topics[key]: 
            responseKeywords["keywords"].append(keyword)
        responseTopics["topics"].append(responseKeywords)    

    return responseTopics

# Saves topics into the Database - can only run once
# def saveTopics(model):
#     topics = model.get_topics()

#     conn = sqlite3.connect('topics.db')  
#     cursor = conn.cursor()
#     cursor.execute('''
#         CREATE TABLE IF NOT EXISTS topics (
#             topic_id INTEGER PRIMARY KEY,
#             words TEXT
#         )
#     ''')

#     for topic_id, words in topics.items():
#         # Check if topic has words
#         if isinstance(words, list) and words:
#             # Get words and weights from tuple
#             words_str = ', '.join([f'{word} ({weight:.4f})' for word, weight in words])
#             cursor.execute('INSERT INTO topics (topic_id, words) VALUES (?, ?)', (topic_id, words_str))
#         else:
#             # Handle the case where the topic has no words
#             words_str = 'No words available'
#             cursor.execute('INSERT INTO topics (topic_id, words) VALUES (?, ?)', (topic_id, words_str))

#     conn.commit()
#     conn.close()