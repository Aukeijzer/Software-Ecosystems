"""
preprocessing
=============

This module provides functions for preprocessing text data, including the removal
of Markdown layout, tokenization, removal of punctuation and non-alphabetic
characters, and lemmatization.
"""


import markdown
import langid
import nltk
from bs4 import BeautifulSoup
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords
from nltk.stem import WordNetLemmatizer

nltk.download('punkt')
nltk.download('stopwords')
nltk.download('wordnet')

def preprocess_docs(docs):
    """
    Preprocess a list of documents using the preprocess_document function.

    Parameters
    ----------
    docs : list
        A list of strings representing documents.

    Returns
    -------
    list
        A list of preprocessed documents.
    """
    preprocessed_documents = [preprocess_document(document)
                              for document in docs]

    return preprocessed_documents

def preprocess_document(document):
    """
    Preprocess a single document by removing Markdown layout, tokenizing text,
    removing punctuation and non-alphabetic characters, and lemmatizing words.

    Parameters
    ----------
    document : str
        The input document as a string.

    Returns
    -------
    str
        The preprocessed document.
    """
    # Detect language
    lang, _ = langid.classify(document)

    # Check if the detected language is English
    if lang != 'en':
        return ''  # Return an empty string for non-English documents

    # Remove markdown layout
    html_document = markdown.markdown(document)
    processed_document = ''.join(BeautifulSoup(
        html_document,
        features="html.parser").findAll(string=True))

    # Tokenize text
    tokens = word_tokenize(processed_document)

    # Remove punctuation and non-alphabetic characters
    tokens = [word for word in tokens if word.isalpha()]

    # Lemmatize words
    lemmatizer = WordNetLemmatizer()
    tokens = [lemmatizer.lemmatize(word) for word in tokens]

    # Remove NLTK stopwords
    stop_words = set(stopwords.words("english"))
    tokens = [word for word in tokens if word.lower() not in stop_words]

    preprocessed_document = " ".join(tokens)

    return preprocessed_document
