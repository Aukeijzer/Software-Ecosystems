import unittest
import sys
sys.path.append('./src')
from app import app

class TestApp(unittest.TestCase):

    def setUp(self):
        self.app = app.test_client()

    def test_extract_topics_endpoint(self):
        # Test with valid input
        valid_input = [
            {"id": "1", "name": "Project 1", "description": "quantum", "readme": "Readme about quantum software"},
            {"id": "2", "name": "Project 2", "description": "agriculture", "readme": "Readme about agriculture software"},
        ]
        response = self.app.post("/extract-topics", json=valid_input)
        self.assertEqual(response.status_code, 200)
        self.assertIn(b"result", response.data)

        # Test with invalid input
        invalid_input = {"id": "1", "description": "quantum", "readme":  "Readme about quantum software"}
        response = self.app.post("/extract-topics", json=invalid_input)
        self.assertEqual(response.status_code, 500)
        self.assertIn(b"error", response.data)

if __name__ == "__main__":
    unittest.main()
