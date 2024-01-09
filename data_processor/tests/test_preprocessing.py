import unittest
import sys
sys.path.append('./src')
from preprocessing import preprocessDocs, preprocess_document

class TestTextPreprocessing(unittest.TestCase):
    def test_markdown_removed(self):
        # Test whether markdown is removed
        document = "# Heading\nThis is a *test* document."
        processed = preprocess_document(document)
        expected_processed = "Heading This is a test document"
        self.assertEqual(processed, expected_processed)

    def test_lemmatization(self):
        # Test lemmatization
        document = "Lemmatize these words."
        processed = preprocess_document(document)
        expected_processed = "Lemmatize these word"
        self.assertEqual(processed, expected_processed)

    def test_preprocessDocs(self):
        # Test preprocessDocs function
        documents = [
            "# Doc 1\nThis is *document* one.",
            "# Doc 2\nThis is *document* two."
        ]
        processed_documents = preprocessDocs(documents)
        expected_processed = ["Doc This is document one", "Doc This is document two"]
        self.assertEqual(processed_documents, expected_processed)

if __name__ == '__main__':
    unittest.main()