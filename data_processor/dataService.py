from preprocessing import preprocessDocs 
from topicModel import extractTopics

# Extracts the readme from json data
def getReadme(data_dict):
    try:
        return data_dict.get('readme')
    except:
        print("No readme found") 

class dataService:
    def __init__(self, data):
        self.data = data    # data should be a list of JSON objects

    def extractTopics(self):
        readmes = [getReadme(dto) for dto in self.data]
        preprocessed_readmes = preprocessDocs(readmes)
        topics = extractTopics(preprocessed_readmes)
        return topics

    def getTop5Topics(self, ecosystem):
        return ""

    