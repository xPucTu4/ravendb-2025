from flask import Flask, request, jsonify
import uuid

app = Flask(__name__)

# Store registration IDs in memory (optional, for realism)
registrations = {}

@app.route('/api/v2/dns-n-cert/register', methods=['POST'])
def handle_register():
    reg_id = str(uuid.uuid4())
    registrations[reg_id] = "SUCCESS"
    return jsonify({"Id": reg_id})

@app.route('/api/v2/dns-n-cert/registration-result', methods=['POST'])
def handle_registration_result():
    reg_id = request.args.get("id")
    status = registrations.get(reg_id, "SUCCESS")
    return jsonify({"Status": status})

@app.route('/', methods=['POST'])
@app.route('/<path:path>', methods=['POST'])
def catch_all(path=""):
    with open('response.json') as f:
        return f.read(), 200

if __name__ == "__main__":
    app.run(host='::', port=5000)
