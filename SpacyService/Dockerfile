FROM python:latest

RUN apt-get update && apt-get install -y \
	build-essential 

RUN apt-get update
RUN apt-get -y install
RUN apt-get clean

RUN pip install --upgrade pip; \
    pip install \
    spacy

RUN python -m spacy download en_core_web_sm

RUN pip install Flask

COPY . /app
WORKDIR /app
ENV PORT 5000
ENTRYPOINT ["python"]
CMD ["EntityRecognition.py"]