FROM python:latest

RUN apt-get update && apt-get install -y \
	build-essential 

RUN apt-get update
RUN apt-get -y install \
    tesseract-ocr \
    tesseract-ocr-jpn
RUN apt-get clean

RUN pip install --upgrade pip; \
    pip install \
    pillow \
#    opencv-python \
#    wand \
    pytesseract


RUN pip install Flask

COPY . /app
WORKDIR /app
ENV PORT 5000
ENTRYPOINT ["python"]
CMD ["PdfToText.py"]