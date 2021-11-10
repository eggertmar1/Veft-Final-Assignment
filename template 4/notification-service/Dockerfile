FROM python:3.8-slim-buster
WORKDIR /app
LABEL maintainer="Eggert Mar Eggertsson"
ENV PYTHON_ENV production
COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt
COPY . .
CMD ["python3", "service.py"]