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

Module: test_app

This module defines tests for the app.py module
"""


import unittest
import sys
sys.path.append('./src')
from app import app


class TestApp(unittest.TestCase):
    """
    A class for testing the functionality of the app module.

    Attributes
    ----------
    app : FlaskClient
        An instance of the Flask test client for making requests to the app.

    Methods
    -------
    setUp()
        Set up the test environment by creating an instance of the Flask test client.

    test_extract_topics_endpoint()
        Test the '/extract-topics' endpoint with valid and invalid input.
    """
    def setUp(self):
        """Set up the test environment."""
        self.app = app.test_client()

    def test_extract_topics_endpoint(self):
        """
        Test the '/extract-topics' endpoint with valid and invalid input.

        This method performs two tests:
        1. Sends a POST request with valid input and checks for a 200 status code.
        2. Sends a POST request with invalid input and checks for a 500 status code 
        with an error message.
        """
        # Test with valid input
        valid_input = [
            {"id": "1",
             "name": "Project 1",
             "description": "quantum",
             "readMe": "Readme about quantum software"},
            {"id": "2",
             "name": "Project 2",
             "description": "agriculture",
             "readMe": "Readme about agriculture software"},
        ]
        response = self.app.post("/extract-topics", json=valid_input)
        self.assertEqual(response.status_code, 200)

        # Test with invalid input
        invalid_input = {"id": "1",
                         "description": "quantum",
                         "readme":  "Readme about quantum software"}
        response = self.app.post("/extract-topics", json=invalid_input)
        self.assertEqual(response.status_code, 500)
        self.assertIn(b"error", response.data)


if __name__ == "__main__":
    unittest.main()
