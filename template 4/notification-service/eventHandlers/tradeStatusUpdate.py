import pika
import json
from services.emailService import send_simple_message
import pika
import requests
import json


def setup_handler(channel, exchange_name):
    channel.queue_declare(queue='email_queue', durable=True)
    channel.queue_bind(exchange=exchange_name, queue='email_queue', routing_key='update_trade')
    channel.basic_consume(queue='email_queue', on_message_callback=send_order_email)
    


# exchange_name = 'order_exchange'
# create_order_routing_key = 'update_trade'
# email_queue_name = 'email_queue'
email_template = '<h2>Trade updated</h2><p>We hope you will enjoy our lovely product and don\'t hesitate to contact us if there are any questions.</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Description</th><th>Unit price</th><th>Quantity</th><th>Row price</th></tr></thead><tbody>%s</tbody></table>'

# # Declare the exchange, if it doesn't exist
# channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
# # Declare the queue, if it doesn't exist
# channel.queue_declare(queue=email_queue_name, durable=True)
# # Bind the queue to a specific exchange with a routing key
# channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)

def send_order_email(data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    email = parsed_msg['email']
    items = parsed_msg['trades']
    # items_html = ''.join([ '<tr><td>%s</td><td>%d</td><td>%d</td><td>%d</td></tr>' % (item['description'], item['unitPrice'], item['quantity'], int(item['quantity']) * int(item['unitPrice'])) for item in items ])
    representation = email_template % email % items
    send_simple_message(parsed_msg['email'], 'Successful trade!', representation)



    
