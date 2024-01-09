import markdown
from bs4 import BeautifulSoup
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer

# Preprocessing data

def preprocessDocs(docs):
    preprocessed_documents = [preprocess_document(document) for document in docs]
    return preprocessed_documents

def preprocess_document(document):
    # Renove markdown layout
    html_document = markdown.markdown(document)
    processed_document = ''.join(BeautifulSoup(html_document, features="html.parser").findAll(string=True))

    # Tokenize text
    tokens = word_tokenize(processed_document)

    # Remove punctuation and non-alphabetic characters
    tokens = [word for word in tokens if word.isalpha()]

    # Lemmatize words
    lemmatizer = WordNetLemmatizer()
    tokens = [lemmatizer.lemmatize(word) for word in tokens]

    return " ".join(tokens)
