import unittest
import sys
sys.path.append('./src')
from preprocessing import preprocessDocs, preprocess_document

class TestTextPreprocessing(unittest.TestCase):
    def test_preprocess_document(self):
        # Test preprocess_document function
        document = "# Heading\nThis is a *test* document."
        processed = preprocess_document(document)
        
        # Define your assertions
        expected_processed = "Heading\nThis is a test document."
        self.assertEqual(processed, expected_processed)

    def test_preprocessDocs(self):
        # Test preprocessDocs function
        documents = ["# Doc 1\nThis is *document* one.", "# Doc 2\nThis is *document* two."]
        processed_documents = preprocessDocs(documents)

        # Define your assertions
        expected_processed = ["Doc 1\nThis is document one.", "Doc 2\nThis is document two."]
        self.assertEqual(processed_documents, expected_processed)

if __name__ == '__main__':
    unittest.main()
