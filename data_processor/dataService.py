from preprocessing import preprocessDocs
from topicModel import extractTopics


# Extracts the documents and ids from json data
def getData(data):
    docs = []
    ids = []
    for dto in data: 
        doc = ""
        if dto.get("id"):
            ids.append(dto["id"])
        if dto.get("name"):
            doc += dto["name"] + " "
        if dto.get("description"):
            doc += dto["description"] + " "
        if dto.get("readme"):
            doc += dto["readme"] + " "
        docs.append(doc)
    return ids, docs


class dataService:
    def __init__(self, data):
        self.data = data  # data should be a list of JSON objects

    def extractTopics(self):
        ids, docs = getData(self.data)
        preprocessed_docs = preprocessDocs(docs)
        topics = extractTopics(preprocessed_docs)
        response = []
        for id, topic in zip(ids, topics): 
            dict = {"projectId": id}
            dict.update(topic)
            response.append(dict)
        return response

    def getTop5Topics(self, ecosystem):
        return ""
