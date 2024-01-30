"""
Copyright (C) <2024> <OdinDash>
 
This file is part of SECODash
 
SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
 
SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.
 
You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

Module: test_preprocessing

This module defines tests for the preprocessing.py module
"""

import unittest
import sys
sys.path.append('./src')
from preprocessing import preprocess_docs, preprocess_document


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
        expected_processed = "Heading test document"
        self.assertEqual(processed, expected_processed)

    def test_lemmatization(self):
        """
        Test lemmatization of words in the document.
        """
        # Test lemmatization
        document = "Lemmatize these words."
        processed = preprocess_document(document)
        expected_processed = "Lemmatize word"
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
        expected_processed = ["Doc document one",
                              "Doc document two"]
        self.assertEqual(processed_documents, expected_processed)


if __name__ == '__main__':
    unittest.main()
