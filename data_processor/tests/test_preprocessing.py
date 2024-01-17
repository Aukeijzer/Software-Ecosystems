"""
Module: test_preprocessing

This module defines tests for the preprocessing.py module
"""

import unittest
import sys
from preprocessing import preprocess_docs, preprocess_document
sys.path.append('./src')


class TestTextPreprocessing(unittest.TestCase):
    """
    A class for testing the functionality of the preprocessing module.
    """
    def test_markdown_removed(self):
        """
        Test whether markdown is removed from the document.
        """
        # Test whether markdown is removed
        document = "# Heading\nThis is a *test* document."
        processed = preprocess_document(document)
        expected_processed = "Heading This is a test document"
        self.assertEqual(processed, expected_processed)

    def test_lemmatization(self):
        """
        Test lemmatization of words in the document.
        """
        # Test lemmatization
        document = "Lemmatize these words."
        processed = preprocess_document(document)
        expected_processed = "Lemmatize these word"
        self.assertEqual(processed, expected_processed)

    def test_preprocess_docs(self):
        """
        Test the preprocess_docs function by providing a list of documents.
        """
        # Test preprocessDocs function
        documents = [
            "# Doc 1\nThis is *document* one.",
            "# Doc 2\nThis is *document* two."
        ]
        processed_documents = preprocess_docs(documents)
        expected_processed = ["Doc This is document one",
                              "Doc This is document two"]
        self.assertEqual(processed_documents, expected_processed)


if __name__ == '__main__':
    unittest.main()
