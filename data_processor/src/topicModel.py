from sklearn.feature_extraction.text import CountVectorizer
from sklearn.decomposition import LatentDirichletAllocation
from sklearn.feature_extraction.text import CountVectorizer
import numpy as np
from bertopic import BERTopic
from bertopic.representation import KeyBERTInspired
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer

def preprocess_text(text):
    # Tokenize text
    tokens = word_tokenize(text)

    # Remove punctuation and non-alphabetic characters
    tokens = [word for word in tokens if word.isalpha()]

    # Lemmatize words
    lemmatizer = WordNetLemmatizer()
    tokens = [lemmatizer.lemmatize(word) for word in tokens]

    return " ".join(tokens)

def extractTopicsLDA(docs, top_X_topics=3):
    preprocessedDocs = [preprocess_text(doc) for doc in docs]

    vectorizer = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    X = vectorizer.fit_transform(preprocessedDocs)
    
    lda = LatentDirichletAllocation(n_components=5, random_state=42)
    lda.fit(X)

    feature_names = vectorizer.get_feature_names_out()
    top_n_words = 5
    topics = []
    for doc in preprocessedDocs:
        probabilities = lda.transform(vectorizer.transform([doc]))[0]
        top_topic_indexes = np.argsort(probabilities)[::-1][:top_X_topics]
        
        doc_topics = []
        for topic_idx in top_topic_indexes:
            top_feature_indexes = lda.components_[topic_idx].argsort()[:-top_n_words - 1:-1]
            top_features = [feature_names[i] for i in top_feature_indexes]
            word_distribution = lda.components_[topic_idx] / np.sum(lda.components_[topic_idx])
            topic_probability = word_distribution.max()

            topic_info = {
                'keywords': top_features,
                'topicId': int(topic_idx), 
                'probability': float(topic_probability) 
            }

            doc_topics.append(topic_info)
        
        topics.append({
            "topics": doc_topics
        })

    return topics

# Does not work yet
def extractTopicsBERTopic(preprocessedDocs):
    # preprocessedDocs = [preprocess_text(doc) for doc in docs]
    topics= []
      # Fit model on readmes
    rm = KeyBERTInspired()
    vectorizer_model = CountVectorizer(ngram_range=(1, 2), stop_words="english")
    model = BERTopic(
        representation_model=rm, 
        nr_topics=5, 
        top_n_words=5, 
        verbose=True, 
        min_topic_size=3, 
        vectorizer_model=vectorizer_model
    )

    _ = model.fit_transform(preprocessedDocs)

    prob_distr = model.approximate_distribution(preprocessedDocs)

    for docs in prob_distr[0]: 
        topics_in_document = []
        doc_topics = {}
        for topic_id, topic_prob in enumerate(docs): 
            if topic_prob > 0: # set threshold for probability
                doc_topic = {}
                doc_topic["topic"] = topic_id
                doc_topic["probability"] = topic_prob
                doc_topic["keywords"] = [keyword[0] for keyword in model.get_topic(topic_id)] 
                topics_in_document.append(doc_topic)
        doc_topics["topics"] = (topics_in_document)
        topics.append(doc_topics)

    return topics

# preprocessed_docs = [
#     "QuantumSimulator is a powerful tool for researchers and developers interested in quantum computing. It provides an intuitive interface to create, visualize, and analyze quantum circuits. With this simulator, you can simulate complex quantum algorithms and study their behavior without the need for expensive quantum hardware.",
#     "AI_ImageRecognition is an advanced image recognition system that leverages deep learning techniques to identify objects and patterns within images. With its robust architecture, it can be integrated into various applications, including autonomous vehicles, security systems, and medical imaging devices. The software is designed for accuracy and efficiency, making it an ideal choice for projects requiring high-performance image recognition capabilities.",
#     "Agriculture_MonitoringSystem is a user-friendly platform tailored for the agriculture sector. It offers real-time monitoring of crop health, soil conditions, and weather patterns. Farmers can make data-driven decisions, optimize irrigation, and prevent crop diseases. Agricultural researchers benefit from the system's data collection capabilities, enabling them to conduct experiments and analyze agricultural trends. With its intuitive interface and customizable features, this system is essential for modernizing farming practices.",
#     "QuantumEncryptionTool is a cutting-edge solution for securing communication channels against quantum threats. Traditional encryption methods can be vulnerable to quantum attacks, but with quantum key distribution (QKD), this tool ensures secure key exchange between parties. It leverages the principles of quantum mechanics, such as quantum entanglement and superposition, to establish unbreakable encryption keys. Whether you're safeguarding financial transactions or sensitive government data, this tool provides the highest level of security.",
#     "AI_DrugDiscovery revolutionizes the pharmaceutical industry by significantly reducing the time and resources required for discovering new drugs. Powered by advanced machine learning algorithms, this platform analyzes vast datasets, identifies potential drug candidates, and predicts their effectiveness. Researchers can focus their efforts on the most promising compounds, leading to faster drug trials and approvals. The platform's predictive analytics and virtual screening capabilities make it an indispensable tool for pharmaceutical companies striving to improve healthcare outcomes.",
#     "Agriculture_SmartIrrigation is a smart solution for farmers looking to enhance crop yields while conserving water resources. Using real-time data from soil moisture sensors, weather forecasts, and crop requirements, the system automates irrigation processes. It precisely delivers the right amount of water to each plant, minimizing wastage and reducing costs. By promoting sustainable irrigation practices, this system not only benefits farmers economically but also contributes to environmental conservation.",
#     "QuantumAlgorithmLibrary is a comprehensive collection of quantum algorithms and protocols designed to empower researchers and developers in the field of quantum computing. The library includes implementations of popular algorithms like Shor's algorithm, Grover's algorithm, and quantum error correction codes. It serves as a valuable resource for studying quantum algorithms, testing new ideas, and developing applications in quantum information processing. The library is constantly updated with the latest advancements in the field, making it a go-to resource for anyone interested in quantum computing.",
#     "AI_FinancialPredictor is a powerful tool for investors and financial analysts seeking accurate predictions in the volatile world of finance. Using machine learning algorithms, the predictor analyzes historical market data, identifies patterns, and forecasts future trends. Whether you're a day trader looking for short-term insights or a long-term investor planning your portfolio, this tool provides actionable predictions to guide your investment decisions. Stay ahead of the market with AI_FinancialPredictor and make informed choices based on data-driven analysis.",
#     "Agriculture_CropAnalysis combines the power of artificial intelligence and satellite imagery to revolutionize crop monitoring. Farmers can assess the health of their crops remotely, identify pest infestations, and predict yield levels. By leveraging machine learning algorithms, the system provides insights into optimal planting patterns, irrigation schedules, and fertilization methods. This technology-driven approach not only maximizes agricultural productivity but also promotes sustainable farming practices. Join the agriculture revolution with Agriculture_CropAnalysis and transform your farming experience.",
#     "QuantumArtificialIntelligence represents the next frontier in computational technology, merging the power of quantum computing with the intelligence of machine learning. This hybrid system tackles problems that are beyond the capabilities of classical computers, such as optimization tasks in logistics, complex simulations in materials science, and cryptanalysis. By harnessing the unique strengths of quantum algorithms and deep learning models, QuantumArtificialIntelligence opens new avenues for scientific discovery and technological innovation. Join",
#     "AI_SocialSentimentAnalysis is an innovative tool that dives deep into the vast ocean of social media data, deciphering sentiments, emotions, and trends. By employing advanced natural language processing and machine learning algorithms, this tool accurately gauges public reactions to events, products, or topics. Marketers can gain invaluable insights into customer perceptions, allowing for tailored marketing strategies. Researchers can analyze societal trends, aiding in social studies and policy-making. With AI_SocialSentimentAnalysis, unlock the pulse of the internet and harness the power of collective opinion to drive informed decisions.",
#     "Agriculture_PrecisionFarming is a holistic approach to modern agriculture, combining the prowess of AI, IoT, and data analytics. This solution transforms farming into a data-driven science, enhancing productivity and sustainability. Farmers can monitor soil quality, crop health, and weather conditions in real-time, enabling precise irrigation, fertilization, and pest control. The AI algorithms analyze historical data to predict optimal planting times and crop rotations, maximizing yields. Moreover, IoT sensors provide actionable insights, ensuring efficient resource utilization. Agriculture_PrecisionFarming is not just a technology; it's a revolution in agriculture, fostering sustainable practices and ensuring food security for the future.",
#     "QuantumComputing_SimulationSuite is a sophisticated toolkit designed for quantum explorers, researchers, and developers eager to unravel the mysteries of quantum computing. This suite offers a diverse range of quantum simulators, allowing users to experiment with quantum algorithms, quantum error correction, and quantum circuit design. Enriched with an intuitive user interface, it provides a seamless experience for both beginners and experts. Dive into the world of superposition, entanglement, and quantum gates. Visualize quantum states, run simulations, and analyze results with precision. Whether you're a physicist, computer scientist, or quantum enthusiast, this suite opens the doors to limitless possibilities and groundbreaking discoveries.",
#     "AI_MedicalDiagnosis is a revolutionary leap in healthcare, leveraging the power of artificial intelligence to transform medical diagnostics. This advanced system analyzes patient symptoms, medical history, and diagnostic reports with unparalleled accuracy. Using deep learning algorithms, it detects subtle patterns and abnormalities, enabling early diagnosis of diseases such as cancer, diabetes, and cardiovascular conditions. Healthcare professionals benefit from precise recommendations, improving the speed and accuracy of medical assessments. By facilitating early intervention and personalized treatment plans, AI_MedicalDiagnosis not only saves lives but also reduces healthcare costs. Join the healthcare revolution and embrace a future where timely and accurate diagnoses pave the way for healthier lives.",
#     "Agriculture_CropPrediction revolutionizes traditional farming by predicting crop yields with unprecedented precision. Powered by advanced machine learning algorithms, this system analyzes historical and real-time data, including weather patterns, soil quality, and crop varieties. By understanding the complex interplay of factors influencing crop growth, farmers can make informed decisions on planting schedules, irrigation, and harvesting. Agricultural businesses benefit from accurate yield forecasts, enabling optimized supply chain management and financial planning. The system's predictive insights empower farmers to adapt to changing conditions, mitigate risks, and maximize profits. With Agriculture_CropPrediction, embrace a new era of agriculture, where data-driven intelligence transforms fields into bountiful harvests.",
#     "QuantumNetworking_ProtocolSuite is at the forefront of quantum communication, offering a robust suite of protocols for secure and efficient data transmission in quantum networks. This suite encompasses protocols for quantum key distribution, quantum teleportation, and quantum entanglement swapping, ensuring the integrity and confidentiality of transmitted information. Researchers and engineers can explore quantum cryptography, quantum routing, and quantum error correction, paving the way for quantum internet and quantum cloud computing. With QuantumNetworking_ProtocolSuite, enter the realm of quantum networking, where the laws of quantum mechanics redefine the boundaries of secure communication. Join the quantum revolution and build the foundation for the future of information exchange.",
#     "AI_FinancialAdvisory redefines the landscape of wealth management and investment strategies. This intelligent platform combines cutting-edge artificial intelligence with financial expertise to offer personalized, data-driven financial advice. By analyzing market trends, economic indicators, and individual risk profiles, AI_FinancialAdvisory creates customized investment portfolios, maximizing returns and minimizing risks. Investors gain access to real-time analytics, portfolio performance reports, and proactive investment recommendations. Whether you're a novice investor or a seasoned financial expert, this platform empowers you to make informed decisions, achieve financial goals, and secure your financial future. Experience the future of finance with AI_FinancialAdvisory and embark on a journey towards financial prosperity."
# ]


# extractTopicsBERTopic(preprocessed_docs)

# print()