import nltk
import re

from nltk.stem.wordnet import WordNetLemmatizer
from nltk.corpus import stopwords

nltk.download('stopwords')
nltk.download('wordnet')

# Define a dictionary to map abbreviations to their full forms
abbreviation_mapping = {
    'ml': 'machine_learning',
    'ai': 'artificial_intelligence',
    'qc': 'quantum_computing',
    'js': 'javascript',
    'q1': 'first_qubit',
    'q2': 'second_qubit',
    'cx': 'controlled_X_gate '
}

# Function to expand abbreviations in a list of tokens
def expand_abbreviations(tokens):
    expanded_tokens = []

    for token in tokens:
        # Check if the token is an abbreviation in the mapping
        if token in abbreviation_mapping:
            expanded_tokens.extend(abbreviation_mapping[token].split())
        else:
            expanded_tokens.append(token)

    return expanded_tokens

# Function to preprocess a single document
def preprocess_readme(content):
    # Remove punctuation from the content
    content = re.sub(r'[^\w\s#]', '', content)
    
    # Define a regular expression pattern to match words and numbers
    pattern = r'\b\w+\b|\b\d+\b|#\w+'

    # Find all matches in the text using the compiled pattern
    tokens = re.findall(pattern, content.lower())

    # Remove numbers but not words that contain numbers
    tokens = [token for token in tokens if not token.isnumeric()]

    # Download the stopwords for English
    nltk.download('stopwords')

    # Get the English stopwords
    stoplist = set(stopwords.words('english'))
    
    # Remove common words and tokenize
    own_stoplist = ["def", "project", "solution", "http", "https", "readme", "use", "example", "license", "text", "guide", "source", "theme"]
    tokens = [token for token in tokens if token not in stoplist]
    tokens = [token for token in tokens if token not in own_stoplist]

    tokens = expand_abbreviations(tokens)

    # Additional preprocessing steps if needed
    tokens = [token for token in tokens if len(token) > 1]

    return tokens

# # Define a function for preprocessing
# def preprocess_readme(readme_content):
#     # Define a regular expression pattern to match words and numbers
#     pattern = r'\b\w+\b|\b\d+\b|#\w+'
#     # Find all matches in the text using the pattern
#     tokens = re.findall(pattern, readme_content.lower())
    
#     # Remove words that are only 1 letter long
#     #tokens = [token for token in tokens if len(token) > 1]
    
#     # Lemmatize the documents
#     lemmatizer = WordNetLemmatizer()
#     tokens = [lemmatizer.lemmatize(token) for token in tokens]

#     # Remove numbers but not words that contain numbers
#     tokens = [token for token in tokens if not token.isnumeric()]
    
#     # Download the stopwords for English
#     nltk.download('stopwords')

#     # Get the English stopwords
#     stoplist = set(stopwords.words('english'))
#     # Remove common words and tokenize
#     own_stoplist = ["project", "solution", "http", "https", "readme", "use", "example", "license", "text", "guide", "source", "theme"]
#     tokens = [token for token in tokens if token not in stoplist]
#     tokens = [token for token in tokens if token not in own_stoplist]
    
#     #print(tokens)
#     return tokens

