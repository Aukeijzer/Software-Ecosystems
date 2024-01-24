"""
app
===

This module defines a Flask application for extracting topics from preprocessed data.
"""

from flask import Flask, request, jsonify
from flasgger import Swagger
from topic_service import TopicService

# app = Flask(__name__)

app = Flask(__name__, static_url_path='/', static_folder='_build/html/')
swagger = Swagger(app)

@app.route('/')
def serve_sphinx_docs(path='index.html'):
    """
    Shows documentation when starting the application.

    """
    return app.send_static_file(path)

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
      400:
        description: Bad Request
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
        except ValueError as ve:
            return jsonify({"error": f"ValueError: {str(ve)}"}), 400
        except KeyError as ke:
            return jsonify({"error": f"KeyError: {str(ke)}"}), 400

if __name__ == "__main__":
    print("Swagger UI on: http://localhost:5000/apidocs/")
    app.run(host='0.0.0.0', debug=True)
