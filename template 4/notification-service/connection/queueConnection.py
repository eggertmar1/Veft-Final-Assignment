import pika
import os
from retry import retry

exchange_name = os.environ.get("EXCHANGE_NAME") or 'order_exchange'
user = os.environ.get('RABBITMQ_DEFAULT_USER') or 'guest'
password = os.environ.get('RABBITMQ_DEFAULT_PASS') or 'guest'
virtual_host = os.environ.get('RABBITMQ_DEFAULT_HOST') or '/'
# host = os.environ.get('RABBITMQ_HOST') or 'host'
port = os.environ.get('RABBITMQ_PORT') or '5672'



@retry(pika.exceptions.AMQPConnectionError, delay=5)
def setup():
    # queue_host = os.environ.get('QUEUE_HOST') or 'justTradeIt'
    queue_host = 'localhost'
    connection = pika.BlockingConnection(
        pika.ConnectionParameters(
            queue_host,port,virtual_host, credentials=pika.PlainCredentials('justTradeIt', 'justTradeIt')))
    channel = connection.channel()
    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

    return (
        channel,
        connection,
        exchange_name
    )