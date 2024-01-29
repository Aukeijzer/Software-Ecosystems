"""
Module: test_topic_model

This module defines tests for the topic_model.py module
"""
import unittest
import sys
from test_data.mock_preprocessed_docs import preprocessed_docs
sys.path.append('./src')
from topic_model import extract_topics_lda, extract_topics_bertopic


class TestTopicExtractor(unittest.TestCase):
    """
    A class for testing the functionality of the topic_model module.
    """
    def test_extract_topics_lda(self):
        """
        Test the extract_topics_lda function by providing preprocessed documents
        and checking the structure of the extracted topics.
        """
        # Extract topics using LDA
        extracted_topics = extract_topics_lda(preprocessed_docs, top_x_topics=3)

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

    def test_extract_topics_bertopic(self):
        """
        Test the extract_topics_bertopic function by providing preprocessed documents
        and checking the structure of the extracted topics.
        """
        # Extract topics using LDA
        extracted_topics = extract_topics_bertopic(preprocessed_docs, top_x_topics=3)

        # Assert that extracted_topics is a list of projects
        self.assertIsInstance(extracted_topics, list)
        self.assertEqual(len(preprocessed_docs), len(extracted_topics))

        # Assert that each project is a dictionary with multiple topics
        for project in extracted_topics:
            for topic in project['topics']:
                self.assertIsInstance(topic, str)

if __name__ == '__main__':
    unittest.main()
