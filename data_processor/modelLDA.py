from gensim.models import LdaModel
from gensim.corpora import Dictionary
from preprocessing import preprocess_readme

# Load the saved LDA model
loaded_model = LdaModel.load('saved/lda_model')

# Load the dictionary used during training
dictionary = Dictionary.load('saved/dictionary')  

# Read and preprocess a new document
new_document = preprocess_readme("ai_ex3.txt")

# Convert the new document to a bag-of-words representation using the same dictionary
new_bow = dictionary.doc2bow(new_document)

# Infer the topic distribution for the new document
topics = loaded_model[new_bow]

# Print the topics and their probabilities
for topic in topics:
    print(f"Topic {topic[0] + 1}: {loaded_model.print_topic(topic[0])} - Probability: {topic[1]}")
