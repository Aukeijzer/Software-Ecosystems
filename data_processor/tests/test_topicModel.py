import unittest
import sys
from test_data.mock_preprocessedDocs import preprocessed_docs
sys.path.append('./src')
from topicModel import extractTopicsLDA

class TestTopicExtractor(unittest.TestCase):
    def test_extractTopics(self):
        # Extract topics using LDA
        extracted_topics = extractTopicsLDA(preprocessed_docs)

        # Assert that extracted_topics is a list of projects
        self.assertIsInstance(extracted_topics, list)
        self.assertEqual(len(preprocessed_docs), len(extracted_topics))

        # Assert that each project is a dictionary with multiple topics
        for project in extracted_topics:
            for topic in project['topics']:
                self.assertIsInstance(topic, dict)
                self.assertIn('keywords', topic)
                self.assertIsInstance(topic['keywords'], list)
                self.assertIn('topicId', topic)
                self.assertIsInstance(topic['topicId'], int)
                self.assertIn('probability', topic)
                self.assertIsInstance(topic['probability'], float)

if __name__ == '__main__':
    unittest.main()
