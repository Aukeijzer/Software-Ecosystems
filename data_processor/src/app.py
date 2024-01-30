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

Module: app
===========

This module defines a Flask application for extracting topics from preprocessed data.
"""

from flask import Flask, request, jsonify
from flasgger import Swagger
from topic_service import TopicService

app = Flask(__name__)
swagger = Swagger(app)

@app.route('/')
def redirect_to_swagger():
    """
    Redirect to swagger UI

    """
    return app.redirect("/apidocs/")

# Handles POST request
@app.route("/extract-topics", methods=["POST"])
def extract_topics():
    """
    Extract topics from the preprocessed data.
    ---
    parameters:
      - name: data
        in: body
        required: true
        schema:
          type: array
          items:
            type: object
            properties:
              id:
                type: string
              name:
                type: string
              description:
                type: string
              readme:
                type: string
    responses:
      200:
        description: Topics extracted successfully
        schema:
          type: object
          properties:
            result:
              type: array
              items: 
                type: string
      500:
        description: Internal Server Error
        schema:
          type: object
          properties:
            error:
              type: string
    """
    if request.method == "POST":
        try:
            data = request.get_json()
            topic_service = TopicService(data)
            response = topic_service.extract_topics_bertopic()
            return response, 200
        except Exception as e:
            return jsonify({"error": f"ValueError: {str(e)}"}), 500

if __name__ == "__main__":
    print("Swagger UI on: http://localhost:5000/apidocs/")
    app.run(host='0.0.0.0', debug=True)
