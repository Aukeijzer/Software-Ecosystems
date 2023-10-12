from modelLDA import extractTopic
from flask import Flask, request, jsonify

app = Flask(__name__)

# extracts readme from json data
def getReadme(data_dict):
    try:
        return data_dict.get('readme')
    except:
        print("No readme found")    

# when POST request 
@app.route("/extract-topics", methods=["POST"])
def extract_topics():
    if request.method == "POST":
        try:
            data = request.get_json()
            readme = getReadme(data)
            topics = extractTopic(readme)
            data["topics"] = topics
            return jsonify(data), 200
        except Exception as e:
            return jsonify({"error": str(e)}), 500

if __name__ == "__main__":
    app.run(debug=True)

