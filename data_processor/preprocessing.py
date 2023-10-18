import markdown
from bs4 import BeautifulSoup

# Preprocessing data

def preprocessDocs(docs):
    preprocessed_documents = [preprocess_document(document) for document in docs]
    return preprocessed_documents


def preprocess_document(document):    
    html_document = markdown.markdown(document)
    processed_document = ''.join(BeautifulSoup(html_document).findAll(text=True))
    return processed_document
