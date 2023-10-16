from flask import Flask, request, jsonify
from dataService import dataService

app = Flask(__name__)


# when POST request
@app.route("/extract-topics", methods=["POST"])
def extract_topics():
    if request.method == "POST":
        try:
            data = request.get_json()
            dataServer1 = dataService(data)
            response = dataServer1.extractTopics()
            return jsonify(response), 200
        except Exception as e:
            return jsonify({"error": str(e)}), 500


if __name__ == "__main__":
    app.run(debug=True)
