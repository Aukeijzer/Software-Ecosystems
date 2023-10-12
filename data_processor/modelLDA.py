from gensim.models import LdaModel
from gensim.corpora import Dictionary
from preprocessing import preprocess_readme

# Load the saved LDA model
loaded_model = LdaModel.load('saved/lda_model')

# Load the dictionary used during training
dictionary = Dictionary.load('saved/dictionary')  

def extractTopic(readme):
    # Read and preprocess a new document
    new_document = preprocess_readme(readme)

    # Convert the new document to a bag-of-words representation using the same dictionary
    new_bow = dictionary.doc2bow(new_document)

    # Infer the topic distribution for the new document
    topics = loaded_model[new_bow]

    # Print the topics and their probabilities
    final_topics = []
    for topic in topics:
        topic_string = loaded_model.print_topic(topic[0], topn=5)
        keywords = topic_string.replace('"', '')
        keywords = [word.split('*')[1] for word in keywords.split('+')]
        topicJson = {"keywords" : keywords, "probability": str(topic[1])}
        final_topics.append(topicJson)    
    return final_topics