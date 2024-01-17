"""
Module: test_topic_service

This module defines tests for the topic_service.py module
"""
import unittest
import json
import sys
from unittest.mock import MagicMock
from topic_service import TopicService
sys.path.append('./src')
# Define mock data for testing
mock_data = [
    {"id": 1, "name": "Project 1",
     "description": "Description 1",
     "readme": "Readme 1"},
    {"id": 2,
     "name": "Project 2",
     "description": "Description 2",
     "readme": "Readme 2"},
]


class TestTopicService(unittest.TestCase):
    """
     A class for testing the functionality of the topic_service module.
    """
    def test_extract_topics_integration(self):
        """
        Test the extract_topics method by mocking dependent functions and
        checking the interactions.
        """
        # Mock preprocessDocs function
        preprocess_docs_mock = MagicMock()
        preprocess_docs_mock.return_value = ["processed_doc_1",
                                             "processed_doc_2"]

        # Mock extractTopicsLDA function
        extract_topics_lda_mock = MagicMock()
        extract_topics_lda_mock.return_value = [
            {"topics": [{"keywords":
                         ["keyword1", "keyword2"],
                         "topicId": 1,
                         "probability": 0.5}]},
            {"topics": [{"keywords":
                         ["keyword3", "keyword4"],
                         "topicId": 2,
                         "probability": 0.6}]},
        ]

        # Mock map_topics function
        map_topics_mock = MagicMock()

        # Patch the functions with the mock functions
        with unittest.mock.patch('topic_service.preprocess_docs',
                                 preprocess_docs_mock), \
             unittest.mock.patch('topic_service.extract_topics_lda',
                                 extract_topics_lda_mock), \
             unittest.mock.patch('topic_service.map_topics',
                                 map_topics_mock):

            # Create an instance of TopicService with mock data
            topic_service = TopicService(mock_data)

            # Call the extractTopics method
            _ = topic_service.extract_topics()

            # Assertions
            preprocess_docs_mock.assert_called_once()
            extract_topics_lda_mock.assert_called_once()
            map_topics_mock.assert_called_once()

    def test_result_types(self):
        """
        Test the types and structure of the result returned by
        the topic service.
        """
        # Read the mock result from a file
        with open(
            './tests/test_data/mock_result.json', 'r', encoding="utf-8"
                ) as file:
            result_json = file.read()

        # Convert the JSON string to a dictionary
        result_dict = json.loads(result_json)

        # Type checks on the structure
        self.assertIsInstance(result_dict, dict)
        self.assertIn('result', result_dict)
        self.assertIsInstance(result_dict['result'], list)

        # Check the project structure
        for item in result_dict['result']:
            self.assertIsInstance(item, dict)
            self.assertIn('projectId', item)
            self.assertIsInstance(item['projectId'], str)
            self.assertIn('topics', item)
            self.assertIsInstance(item['topics'], list)

            # Check the topic structure
            for topic in item['topics']:
                self.assertIsInstance(topic, dict)
                self.assertIn('keywords', topic)
                self.assertIsInstance(topic['keywords'], list)
                self.assertIn('mapped_topic', topic)
                self.assertIsInstance(topic['mapped_topic'], str)
                self.assertIn('probability', topic)
                self.assertIsInstance(topic['probability'], float)
                self.assertIn('topicId', topic)
                self.assertIsInstance(topic['topicId'], int)


if __name__ == '__main__':
    unittest.main()
