import requests
def send_simple_message(to, subject, body):
    return requests.post(
        f"https://api.mailgun.net/v3/sandbox118e31ac11e54daa978b8d2a90dfda30.mailgun.org/messages",
		auth=("api", "a6339980604192e0e9e306bd7fd695b8-2ac825a1-abf1553a"),
		data={"from": "Mailgun Sandbox <postmaster@sandbox118e31ac11e54daa978b8d2a90dfda30.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})
