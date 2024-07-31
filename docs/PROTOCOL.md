# OAINet Protocol Specification

## Overview

OAINet is a decentralized network designed for efficient and secure data exchange, blockchain management, and computational power distribution. This document outlines the protocol and specific endpoints used for communication within the OAINet network. It also details the expected behavior for each endpoint to ensure the network functions smoothly.

## Protocol Structure

### General Format

Requests in OAINet follow a structured format that includes a URI, request parameters, and optionally, nested objects. Here's the general format:

```
oainet://<command>
<name_param>: <value>;
...

<object_name>:
    <name_param>: <value>;
    <nested_object>:
        <nested_name_param>: <nested_value>;
    ;
```

## Endpoints

### 1. Node Information

**URI:** `oainet://node/information`

**Description:** Retrieves general information about the node.

**Request:**
```
oainet://node/information
```

**Response:**
```plaintext
status: "success";
content: {
    "status": "active",
    "block_count": 123,
    "peer_count": 10
}
```

### 2. Wallet Management

#### Get Wallet Balance

**URI:** `oainet://wallet/balance`

**Description:** Retrieves the balance of a specified wallet. Note: Wallets are managed and created by the client, not the node.

**Request:**
```
oainet://wallet/balance
wallet_address: abcd1234;
```

**Response:**
```plaintext
status: "success";
content: {
    "wallet_address": "abcd1234",
    "balance": 100.5
}
```

### 3. Transaction Management

#### Create Transaction

**URI:** `oainet://transaction/create`

**Description:** Creates a new transaction.

**Request:**
```
oainet://transaction/create
sender: abcd1234;
receiver: efgh5678;
amount: 50;
```

**Response:**
```plaintext
status: "pending";
content: {
    "transaction_id": "tx1234"
}
```

### 4. Blockchain Management

#### Add Block

**URI:** `oainet://blockchain/add`

**Description:** Adds a new block to the blockchain.

**Request:**
```
oainet://blockchain/add
block_data:
    previous_hash: "hash_value";
    content: "block_content";
    created_at: "2024-06-01T12:34:56Z";
```

**Response:**
```plaintext
status: "success";
content: {
    "block_id": "block1234"
}
```

##### Get Blockchain Status

**URI:** `oainet://blockchain/status`

**Description:** Retrieves the current status of the blockchain.

**Request:**
```
oainet://blockchain/status
```

**Response:**
```plaintext
status: "success";
content: {
    "block_count": 123,
    "latest_block": "block1234",
    "status": "healthy"
}
```

### Contribution Guidelines

To contribute to OAINet, please follow the instructions in our [Contributing Guide](CONTRIBUTING.md).

### TODO

For a list of tasks and features that need to be completed, please refer to our [TODO List](TODO.md).