"""
Module: preprocessing

This module provides functions for preprocessing text data, including the
 removal of Markdown layout, tokenization, removal of punctuation and
 non-alphabetic characters and lemmatization.

Functions:
- preprocess_docs: Preprocess a list of documents using the preprocess_document
 function.
- preprocess_document: Preprocess a single document by removing Markdown
 layout, tokenizing text, removing punctuation and non-alphabetic characters
 and lemmatizing words.
"""

import markdown
from bs4 import BeautifulSoup
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer


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
    # Renove markdown layout
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

    return " ".join(tokens)
