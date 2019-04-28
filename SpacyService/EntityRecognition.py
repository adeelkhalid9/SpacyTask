from flask import Flask
from flask import request
import spacy
from spacy import displacy
from collections import Counter
import en_core_web_sm

app = Flask(__name__, static_url_path='/static')

app = Flask(__name__)

@app.route('/')
def running():
    return "Running..."

@app.route('/recognizer', methods = ['POST'])
def name_entity_recognition():
    if request.method == 'POST':
        rawData = request.values['rawData']
        if rawData:
            nlp = en_core_web_sm.load()
            doc = nlp(rawData)
            #print([(X.text, X.label_) for X in doc.ents])
            json_doc = doc.to_json()
            return str(json_doc)
    else:
        return "Test dara is not found"


if __name__ == "__main__":
    app.run(host='0.0.0.0')