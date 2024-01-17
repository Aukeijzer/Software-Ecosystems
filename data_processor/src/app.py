"""
Module: app

This module defines a Flask application for extracting topics from
 preprocessed data.

"""

from flask import Flask, request, jsonify
from flasgger import Swagger
from topic_service import TopicService

app = Flask(__name__)
swagger = Swagger(app)


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
            response = topic_service.extract_topics()
            return response, 200
        except Exception as e:
            return jsonify({"error": str(e)}), 500

if __name__ == "__main__":
    print("Swagger UI on: http://localhost:5000/apidocs/")
    app.run(debug=True)