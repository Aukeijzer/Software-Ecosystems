"""
Module: test_map_topic

This module defines tests for the map_topic.py module
"""

import unittest
import sys
import json
sys.path.append('./src')
from map_topic import flatten_taxonomy_to_strings, map_topics_cosine, get_taxonomy


class TestTaxonomyFunctions(unittest.TestCase):
    """
    A class for testing the functionality of the map_topic module.
    """
    def test_get_valid_taxonomy(self):
        """Test the get_taxonomy function by checking the structure of the returned taxonomy data.
        """
        # Test get_taxonomy function
        taxonomy_data = get_taxonomy(file_path = "assets/taxonomy.json")

        # Assertion on structure of taxonomy
        self.assertIsInstance(taxonomy_data, dict)

    def test_get_invalid_taxonomy(self):
        """Test the get_taxonomy function by checking the error of the invalid taxonomy
        """
        # Test get_taxonomy function
        taxonomy_data = get_taxonomy(file_path = "tests/test_data/invalid_taxonomy.json")

        # Assertion
        self.assertIsInstance(taxonomy_data, json.JSONDecodeError)

    def test_get_non_existing_taxonomy(self):
        """Test the get_taxonomy function by checking the error of the non-existing taxonomy
        """
        # Test get_taxonomy function
        taxonomy_data = get_taxonomy(file_path = "assets/non_existing.json")

        # Assertion
        self.assertIsInstance(taxonomy_data, FileNotFoundError)

    def test_flatten_taxonomy_to_strings(self):
        """Test the flatten_taxonomy_to_strings function by providing a sample taxonomy and checking 
        the flattened topics.
        """
        # Mock JSON data representing a taxonomy
        sample_taxonomy = {
            "topic1": {
                "subtopic1": None,
                "subtopic2": {
                    "subsubtopic1": None
                }
            },
            "topic2": None
        }

        flattened_topics = flatten_taxonomy_to_strings(sample_taxonomy)

        # Expected flattened topics
        expected_flattened = ["topic1",
                              "subtopic1",
                              "subtopic2",
                              "subsubtopic1",
                              "topic2"]
        self.assertIsInstance(flattened_topics, list)
        self.assertEqual(flattened_topics, expected_flattened)

    def test_map_topics_cosine(self):
        """Test the map_topics function by providing sample projects data and checking the modified 
        project data.
        """
        # Mock projects data
        sample_projects = [
            {"topics": [{"keywords": ["keyword1", "keyword2"]}]},
            {"topics": [{"keywords": ["keyword3", "keyword4"]}]},
        ]

        # Map topics to taxonomy
        mapped_topics = map_topics_cosine(sample_projects)

        # Assertions to the modified project data
        for project in mapped_topics:
            self.assertIn("topics", project)
            self.assertIsInstance(project["topics"], list)
            for topic in project["topics"]:
                self.assertIsInstance(topic, str)


if __name__ == '__main__':
    unittest.main()
