import logging
import os
# logging.basicConfig(format='%(asctime)s : %(levelname)s : %(message)s', level=logging.INFO)

from gensim import corpora
from gensim.models import LdaModel
from preprocessing import preprocess_readme
from pprint import pprint

# Empty list to store all preprocessed documents
preprocessed_documents = []

# Folder containing the training documents
folder_path = 'training_docs'

# Loop through all files in the folder
for filename in os.listdir(folder_path):
    if filename.endswith('.txt'):
        file_path = os.path.join(folder_path, filename)
        with open(file_path, 'r', encoding='utf-8') as file:
            readme_content = file.read()
            preprocessed_content = preprocess_readme(readme_content)
            preprocessed_documents.append(preprocessed_content)

# print(preprocessed_documents)

# Flatten the list of lists
#preprocessed_documents = [token for doc in preprocessed_documents for token in doc]

dictionary = corpora.Dictionary(preprocessed_documents)
corpus = [dictionary.doc2bow(doc) for doc in preprocessed_documents]

#print('Number of unique tokens: %d' % len(dictionary))
#print('Number of documents: %d' % len(corpus))

# Set training parameters for LDA
num_topics = 5
chunksize = 2000
passes = 300
iterations = 600
eval_every = None       	# Don't evaluate model perplexity, takes too much time.

# # Make an index to word dictionary.
temp = dictionary[0]  # This is only to "load" the dictionary.
id2word = dictionary.id2token

model = LdaModel(
    corpus=corpus,
    id2word=id2word,
    chunksize=chunksize,
    alpha='auto',
    eta='auto',
    iterations=iterations,
    num_topics=num_topics,
    passes=passes,
    eval_every=eval_every
)

top_topics = model.top_topics(corpus)

# Save the LDA model and dictionary
model.save('saved/lda_model')
dictionary.save('saved/dictionary')

# Average topic coherence is the sum of topic coherences of all topics, divided by the number of topics.
avg_topic_coherence = sum([t[1] for t in top_topics]) / num_topics
print('Average topic coherence: %.4f.' % avg_topic_coherence)

# Print topics with a specified number of top keywords
num_keywords_per_topic = 5  

for topic_id, topic in enumerate(model.get_topics()):
    top_keywords = [id2word[word_id] for word_id in topic.argsort()[-num_keywords_per_topic:]]
    print(f"Topic {topic_id + 1}: {', '.join(top_keywords)}")
    
#pprint(top_topics)
    