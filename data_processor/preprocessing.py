# Preprocessing data

def preprocessDocs(docs):
    preprocessed_documents = [preprocess_document(document) for document in docs]
    return preprocessed_documents

def preprocess_document(document):
    processed_document = document
    return processed_document
