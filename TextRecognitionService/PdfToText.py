import os
from flask import Flask
from flask import request
from pytesseract import pytesseract
#from wand.image import Image
from PIL import Image as PI
import io

app = Flask(__name__)

@app.route('/')
def running():
    return "Running..."

@app.route('/convertfiles', methods = ['POST'])
def pdf_to_text():
    file = request.files['file']
    filename = file.filename
    file.save(filename)

    #pdf = Image(filename = filename, resolution = 300)

    #with Img(filename='sample.pdf', resolution=300) as img:
     #   img.compression_quality = 99
     #   img.save(filename='sample.jpg')

    #with open('sample.pdf', 'rb') as fd:
    #    with Image(file=fd, resolution=100, format="PDF[0]") as pdf:
    #        pdf.transform(resize="x200")
    #        pdf.save(filename="sample.jpg")

    #pdfImage = file

    #imageBlobs = []

    #for img in pdfImage.sequence:
    #    imgPage = wi(image = img)
    #    imageBlobs.append(imgPage.make_blob('jpeg'))

    #recognized_text = []

    #for imgBlob in imageBlobs:
    #    im = Image.open(io.BytesIO(imgBlob))
    #    text = pytesseract.image_to_string(im, lang = 'eng')
    #   recognized_text.append(text)


    im = PI.open(filename)
    text = pytesseract.image_to_string(im, lang = 'eng')
    
    return text


if __name__ == "__main__":
    app.run(host='0.0.0.0')