import nltk
import re

from nltk.stem.wordnet import WordNetLemmatizer
from nltk.corpus import stopwords

# Define a function for preprocessing
def preprocess_readme(readme_content):
    # Define a regular expression pattern to match words and numbers
    pattern = r'\b\w+\b|\b\d+\b'
    # Find all matches in the text using the pattern
    tokens = re.findall(pattern, readme_content.lower())
    
    
    # Lemmatize the documents
    lemmatizer = WordNetLemmatizer()
    tokens = [lemmatizer.lemmatize(token) for token in tokens]

    # Remove numbers but not words that contain numbers
    tokens = [token for token in tokens if not token.isnumeric()]
    
    # Download the stopwords for English
    nltk.download('stopwords')

    # Get the English stopwords
    stoplist = set(stopwords.words('english'))
    # Remove common words and tokenize
    own_stoplist = ["project", "solution", "http", "https", "readme"]
    tokens = [token for token in tokens if token not in stoplist]
    tokens = [token for token in tokens if token not in own_stoplist]
    
    #print(tokens)
    return tokens

