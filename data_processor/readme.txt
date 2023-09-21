AI Introduction
Artificial intelligence (AI) is intelligence demonstrated by machines, as opposed to intelligence of humans and other animals. Example tasks in which this is done include speech recognition, computer vision, translation between (natural) languages, as well as other mappings of inputs.

AI applications include advanced web search engines (e.g., Google Search), recommendation systems (used by YouTube, Amazon, and Netflix), understanding human speech (such as Siri and Alexa), self-driving cars (e.g., Waymo), generative or creative tools (ChatGPT and AI art), automated decision-making, and competing at the highest level in strategic game systems (such as chess and Go).

As machines become increasingly capable, tasks considered to require "intelligence" are often removed from the definition of AI, a phenomenon known as the AI effect. For instance, optical character recognition is frequently excluded from things considered to be AI, having become a routine technology.

Artificial intelligence was founded as an academic discipline in 1956, and in the years since it has experienced several waves of optimism, followed by disappointment and the loss of funding (known as an "AI winter"), followed by new approaches, success, and renewed funding. AI research has tried and discarded many different approaches, including simulating the brain, modeling human problem solving, formal logic, large databases of knowledge, and imitating animal behavior. In the first decades of the 21st century, highly mathematical and statistical machine learning has dominated the field, and this technique has proved highly successful, helping to solve many challenging problems throughout industry and academia.

The various sub-fields of AI research are centered around particular goals and the use of particular tools. The traditional goals of AI research include reasoning, knowledge representation, planning, learning, natural language processing, perception, and the ability to move and manipulate objects. General intelligence (the ability to solve an arbitrary problem) is among the field's long-term goals. To solve these problems, AI researchers have adapted and integrated a wide range of problem-solving techniques, including search and mathematical optimization, formal logic, artificial neural networks, and methods based on statistics, probability, and economics. AI also draws upon computer science, psychology, linguistics, philosophy, and many other fields.

Samples, Reference Architectures & Best Practices
This repository is meant to organize Microsoft's Open Source AI based repositories.

Keywords
batch scoring, realtime scoring, model training, MLOps, Azure Machine Learning, computer vision, natural language processing, recommenders

Table of contents
Getting Started
AI100 - Samples
AI200 - Reference Architectures
AI300 - Best Practices
Contributing
Getting Started 
This repository is arranged as submodules so you can either pull all the tutorials or simply the ones you want. To pull all the tutorials run:

git clone --recurse-submodules https://github.com/microsoft/ai
if you have git older than 2.13 run:

git clone --recursive https://github.com/microsoft/ai.git
To pull a single submodule (e.g. DeployDeepModelKubernetes) run:

git clone https://github.com/microsoft/ai
cd ai
git submodule init submodules/DeployDeepModelKubernetes
git submodule update
AI100 - Samples
Samples are a collection of open source Python repositories created by the Microsoft product teams, which focus on AI services.

Title	Description
Azure ML Python SDK	Python notebooks with ML and deep learning examples with Azure Machine Learning
Azure Cognitive Services Python SDK	Learn how to use the Cognitive Services Python SDK with these samples
Azure Intelligent Kiosk	Here you will find several demos showcasing workflows and experiences built on top of the Microsoft Cognitive Services.
MML Spark Samples	MMLSpark is an ecosystem of tools aimed towards expanding the distributed computing framework Apache Spark in several new directions.
Seismic Deep Learning Samples	Deep Learning for Seismic Imaging and Interpretation.
AI200 - Reference Architectures 
Our reference architectures are arranged by scenario. Each architecture includes open source practices, along with considerations for scalability, availability, manageability, and security.

Title	Language	Environment	Design	Description	Status
Deploy Classic ML Model on Kubernetes	Python	CPU	Real-Time Scoring	Train LightGBM model locally using Azure ML, deploy on Kubernetes or IoT Edge for real-time scoring	Build Status
Deploy Deep Learning Model on Kubernetes	Python	Keras	Real-Time Scoring	Deploy image classification model on Kubernetes or IoT Edge for real-time scoring using Azure ML	Build Status
Hyperparameter Tuning of Classical ML Models	Python	CPU	Training	Train LightGBM model locally and run Hyperparameter tuning using Hyperdrive in Azure ML	
Deploy Deep Learning Model on Pipelines	Python	GPU	Batch Scoring	Deploy PyTorch style transfer model for batch scoring using Azure ML Pipelines	Build Status
Deploy Classic ML Model on Pipelines	Python	CPU	Batch Scoring	Deploy one-class SVM for batch scoring anomaly detection using Azure ML Pipelines	
Deploy R ML Model on Kubernetes	R	CPU	Real-Time Scoring	Deploy ML model for real-time scoring on Kubernetes	
Deploy R ML Model on Batch	R	CPU	Scoring	Deploy forecasting model for batch scoring using Azure Batch and doAzureParallel	
Deploy Spark ML Model on Databricks	Python	Spark	Batch Scoring	Deploy a classification model for batch scoring using Databricks	
Train Distributed Deep Leaning Model	Python	GPU	Training	Distributed training of ResNet50 model using Batch AI	
AI300 - Best Practices 
Our best practices are arranged by topic. Each best pratice repository includes open source methods, along with considerations for scalability, availability, manageability, and security.

Title	Description
Computer Vision	Accelerate the development of computer vision applications with examples and best practice guidelines for building computer vision systems
Natural Language Processing	State-of-the-art methods and common scenarios that are popular among researchers and practitioners working on problems involving text and language.
Recommenders	Examples and best practices for building recommendation systems, provided as Jupyter notebooks.
MLOps	MLOps empowers data scientists and app developers to help bring ML models to production.
Recommend a Scenario
If there is a particular scenario you are interested in seeing a tutorial for please fill in a scenario suggestion

Ongoing Work
We are constantly developing interesting AI reference architectures using Microsoft AI Platform. Some of the ongoing projects include IoT Edge scenarios, model scoring on mobile devices, add more... To follow the progress and any new reference architectures, please go to the AI section of this link.

Contributing 
This project welcomes contributions and suggestions. Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the Microsoft Open Source Code of Conduct. For more information see the Code of Conduct FAQ or contact opencode@microsoft.com with any additional questions or comments.