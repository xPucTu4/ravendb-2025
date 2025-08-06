FROM python:3.12-alpine

WORKDIR /app
COPY server.py response.json ./

RUN pip install --no-cache-dir flask

CMD ["python", "server.py"]
