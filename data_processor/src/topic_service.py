"""
topic_service
=============

This module provides a service class for extracting and mapping topics from a
list of projects' data.
"""

from topic_model import extract_topics_lda
from preprocessing import preprocess_docs
from map_topic import map_topics


def get_data(data):
    """
    Extract document information and ids from json data.

    Parameters
    ----------
    data : list
        A list of dictionaries representing project data.

    Returns
    -------
    tuple
        A tuple containing a list of ids and list of document information.
    """
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


class TopicService:
    """
    A class for extracting and mapping topics from a list of projects data.

    Attributes
    ----------
    data : list
        A list of dictionaries representing project data.
    """
    def __init__(self, data):
        """
        Initialize the TopicService instance.

        Parameters
        ----------
        data : list
            A list of dictionaries representing project data.
        """
        self.data = data

    def extract_topics(self):
        """
        Extract topics from the project data using preprocessing, topic
          modeling and topic mapping.

        Returns
        -------
        list
            A list of dictionaries containing project IDs and their
              corresponding mapped topics.
        """
        ids, docs = get_data(self.data)
        preprocessed_docs = preprocess_docs(docs)
        topics = extract_topics_lda(preprocessed_docs, 5)
        mapped_topics = map_topics(topics)

        response = []
        for id_, topic in zip(ids, mapped_topics):
            dict_ = {"projectId": id_}
            dict_.update(topic)
            response.append(dict_)
        return response
