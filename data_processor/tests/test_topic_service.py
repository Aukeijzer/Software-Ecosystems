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

Module: test_topic_service

This module defines tests for the topic_service.py module
"""
import unittest
import json
import sys
from unittest.mock import MagicMock
sys.path.append('./src')
from topic_service import TopicService

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
    def test_extract_topics_bertopic_integration(self):
        """
        Test the extract_topics+bertopic method by mocking dependent functions
        and checking the interactions.
        """
        # Mock preprocessDocs function
        preprocess_docs_mock = MagicMock()
        preprocess_docs_mock.return_value = ["processed_doc_1",
                                            "processed_doc_2"]

        # Mock extract_topics_bertopic function
        extract_topics_bertopic_mock = MagicMock()
        extract_topics_bertopic_mock.return_value = [
            {"topics": ["Quantum", "Agriculture", "AI"]},
            {"topics": ["Software", "Hardware", "Mobile Application"]},
        ]

        # Patch the functions with the mock functions
        with unittest.mock.patch('topic_service.preprocess_docs',
                                 preprocess_docs_mock), \
             unittest.mock.patch('topic_service.extract_topics_bertopic',
                                 extract_topics_bertopic_mock):
            # Create an instance of TopicService with mock data
            topic_service = TopicService(mock_data)

            # Call the extractTopics method
            _ = topic_service.extract_topics_bertopic()

            # Assertions
            preprocess_docs_mock.assert_called_once()
            extract_topics_bertopic_mock.assert_called_once()

    def test_extract_topics_lda_integration(self):
        """
        Test the extract_topics method by mocking dependent functions and checking the interactions.
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
             unittest.mock.patch('topic_service.map_topics_cosine',
                                 map_topics_mock):

            # Create an instance of TopicService with mock data
            topic_service = TopicService(mock_data)

            # Call the extractTopics method
            _ = topic_service.extract_topics_lda()

            # Assertions
            preprocess_docs_mock.assert_called_once()
            extract_topics_lda_mock.assert_called_once()
            map_topics_mock.assert_called_once()

    def test_result_types(self):
        """
        Test the types and structure of the result returned by the topic service.
        """
        # Read the mock result from a file
        with open('./tests/test_data/mock_result.json', 'r', encoding="utf-8") as file:
            result_json = file.read()

        # Convert the JSON string to a dictionary
        result = json.loads(result_json)

        # Type checks on the structure
        self.assertIsInstance(result, list)

        # Check the project structure
        for item in result:
            self.assertIsInstance(item, object)
            self.assertIn('projectId', item)
            self.assertIsInstance(item['projectId'], str)
            self.assertIn('topics', item)
            self.assertIsInstance(item['topics'], list)

            # Check the topic structure
            for topic in item['topics']:
                self.assertIsInstance(topic, str)

if __name__ == '__main__':
    unittest.main()
