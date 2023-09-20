import logging
import nltk
import re
# logging.basicConfig(format='%(asctime)s : %(levelname)s : %(message)s', level=logging.INFO)

from nltk.stem.wordnet import WordNetLemmatizer
from nltk.corpus import stopwords

# Define a function for preprocessing
def preprocess_readme(readme_content):
    # Split the document into tokens
    #tokenizer = RegexpTokenizer(r'\w')
    # Define a regular expression pattern to match words and numbers
    pattern = r'\b\w+\b|\b\d+\b'
    #tokens = word_tokenize(readme_content.lower()) 
    # Find all matches in the text using the pattern
    tokens = re.findall(pattern, readme_content.lower())
    
    
    # Lemmatize the documents
    lemmatizer = WordNetLemmatizer()
    tokens = [lemmatizer.lemmatize(token) for token in tokens]

    # Remove numbers but not words that contain numbers
    tokens = [token for token in tokens if not token.isnumeric()]

    #symbols_to_remove = [":", "#", "/", "*", "!", "=", "[", "]", "(", ")", "'", "-"]
    #tokens = [token for token in tokens if token not in symbols_to_remove]

    # Remove double quotation marks
    #tokens = [token for token in tokens if token != '"']
    
    # Download the stopwords for English
    nltk.download('stopwords')

    # Get the English stopwords
    stoplist = set(stopwords.words('english'))

    # remove common words and tokenize
    #stoplist = ["use", "your", "solution", "project","the", "a", "an", "and", "or", "in", "on", "at", "is", "name", "readme", "https", "of", "from", "to", "it", "by", "as", "if", "we", "into", "for"]
    tokens = [token for token in tokens if token not in stoplist]

    #print(documents)
    
    #print(tokens)
    return tokens

