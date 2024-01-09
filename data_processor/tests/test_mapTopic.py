import unittest
import sys
sys.path.append('./src')
from mapTopic import flatten_taxonomy_to_strings, map_topics, get_taxonomy

class TestTaxonomyFunctions(unittest.TestCase):
    def test_get_taxonomy(self):
        # Test get_taxonomy function 
        taxonomy_data = get_taxonomy()

        # Assertion on structure of taxonomy
        self.assertIsInstance(taxonomy_data, dict)

    def test_flatten_taxonomy_to_strings(self):
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
        expected_flattened = ["topic1", "subtopic1", "subtopic2", "subsubtopic1", "topic2"]
        self.assertIsInstance(flattened_topics, list)
        self.assertEqual(flattened_topics, expected_flattened)

    def test_map_topics(self):
        # Mock projects data
        sample_projects = [
            {"topics": [{"keywords": ["keyword1", "keyword2"]}]},
            {"topics": [{"keywords": ["keyword3", "keyword4"]}]},
        ]

        # Map topics to taxonomy
        map_topics(sample_projects)

        # Assertions to the modified project data
        for project in sample_projects:
            self.assertIn("topics", project)
            self.assertIsInstance(project["topics"], list)
            for topic in project["topics"]:
                self.assertIsInstance(topic, dict)
                self.assertIn("mapped_topic", topic)

if __name__ == '__main__':
    unittest.main()