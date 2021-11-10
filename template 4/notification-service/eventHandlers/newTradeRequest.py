import pika
import json
from services.emailService import send_simple_message
import pika
import requests
import json
"""
This is the template for the email that will be sent to the user when a trade request is made.
"""

exchange_name = 'order_exchange'
create_order_routing_key = 'create_trade'
email_queue_name = 'email_queue'
email_template = '<h2>Trade updated</h2><p>We hope you will enjoy your trade and don\'t contact us if there are any questions. :P</p>'

def setup_handler(channel, exchange_name):
    channel.queue_declare(queue=email_queue_name, durable=True)
    channel.queue_bind(exchange=exchange_name, queue=email_queue_name,routing_key=create_order_routing_key)
    channel.basic_consume(queue=email_queue_name, on_message_callback=send_order_email, auto_ack=True)

def send_order_email(ch, method, properties, data):
    print(data)
    parsed_msg = json.loads(data)
    print(parsed_msg)
    email = parsed_msg['receiver']['email']
    # items = parsed_msg['tradeRequest']
    items_html = ''.join([ '<tr><td>%s</td></tr>' % email])
    representation = email_template % items_html
    send_simple_message(email, 'Successful trade!', representation)





