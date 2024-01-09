import unittest
import json
import sys
from unittest.mock import MagicMock

sys.path.append('./src')
from topicService import topicService

# Define mock data for testing
mock_data = [
    {"id": 1, "name": "Project 1", "description": "Description 1", "readme": "Readme 1"},
    {"id": 2, "name": "Project 2", "description": "Description 2", "readme": "Readme 2"},
]

class TestTopicService(unittest.TestCase):
    def test_extractTopics_integration(self):
        # Mock preprocessDocs function
        preprocessDocs_mock = MagicMock()
        preprocessDocs_mock.return_value = ["processed_doc_1", "processed_doc_2"] 

        # Mock extractTopicsLDA function
        extractTopicsLDA_mock = MagicMock()
        extractTopicsLDA_mock.return_value = [
            {"topics": [{"keywords": ["keyword1", "keyword2"], "topicId": 1, "probability": 0.5}]},
            {"topics": [{"keywords": ["keyword3", "keyword4"], "topicId": 2, "probability": 0.6}]},
        ]

        # Mock map_topics function
        map_topics_mock = MagicMock()

        # Patch the functions with the mocks
        with unittest.mock.patch('topicService.preprocessDocs', preprocessDocs_mock), \
             unittest.mock.patch('topicService.extractTopicsLDA', extractTopicsLDA_mock), \
             unittest.mock.patch('topicService.map_topics', map_topics_mock):
            
            # Create an instance of topicService with mock data
            topic_service = topicService(mock_data)

            # Call the extractTopics method
            result = topic_service.extractTopics()

            # Assertions
            preprocessDocs_mock.assert_called_once()
            extractTopicsLDA_mock.assert_called_once()
            map_topics_mock.assert_called_once()
            self.assertEqual(len(result), len(mock_data)) 

    def test_result_types(self):
        # Read the mock result from a file
        with open('./tests/test_data/mock_result.json', 'r') as file:
            result_json = file.read()

        # Convert the JSON string to a Python dictionary
        result_dict = json.loads(result_json)

        # Type checks on the structure
        self.assertIsInstance(result_dict, dict)
        self.assertIn('result', result_dict)
        self.assertIsInstance(result_dict['result'], list)

        # Check each project's structure
        for item in result_dict['result']:
            self.assertIsInstance(item, dict)
            self.assertIn('projectId', item)
            self.assertIsInstance(item['projectId'], str)
            self.assertIn('topics', item)
            self.assertIsInstance(item['topics'], list)

            # Check each topic's structure
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
