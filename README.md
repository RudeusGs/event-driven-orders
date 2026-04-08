# EventDrivenOrders

An event-driven order processing system built with .NET and RabbitMQ.

This project simulates a real-world e-commerce workflow where orders are processed asynchronously using message queues, improving scalability, performance, and system resilience.

---

## Key Concepts

- Event-driven architecture
- Asynchronous processing with RabbitMQ
- Decoupled services (API & Worker)
- Order lifecycle management
- Retry & failure handling
- Clean architecture (4-layer structure)

---

## Architecture

The system follows a 4-layer architecture:

```text
Presentation Layer (API)
        │
        ▼
Application Layer (Use Cases / Services)
        │
        ▼
Domain Layer (Entities, Enums, Business Rules)
        │
        ▼
Infrastructure Layer (Database, RabbitMQ, External Services)
```
---

## Tech Stack

- .NET Core Web API
- RabbitMQ (Message Queue)
- PostgreSQL
- Entity Framework Core
- Docker (optional)

---

## System Flow

```text
Client
  │
  ▼
API (Create Order)
  │
  ▼
Save Order (Pending)
  │
  ▼
Publish Event (RabbitMQ)
  │
  ▼
Worker (Consumer)
  │
  ▼
Process Order (Stock, Payment)
  │
  ▼
Update Order Status (Completed / Failed)
```
---

## Order Lifecycle

Pending → Processing → Completed / Failed

---

## Database Design

### Orders

| Column       | Type        | Description                  |
|-------------|------------|------------------------------|
| Id          | Guid (PK)  | Unique order identifier      |
| UserId      | Guid (FK)  | Reference to user            |
| Status      | int        | Order status                 |
| TotalAmount | decimal    | Total price of the order     |
| CreatedAt   | datetime   | Order creation time          |

---

### OrderItems

| Column     | Type        | Description                  |
|-----------|------------|------------------------------|
| Id        | Guid (PK)  | Unique identifier            |
| OrderId   | Guid (FK)  | Reference to order           |
| ProductId | Guid (FK)  | Reference to product         |
| Quantity  | int        | Quantity of product          |
| Price     | decimal    | Price at purchase time       |

---

### Products

| Column | Type        | Description              |
|--------|------------|--------------------------|
| Id     | Guid (PK)  | Product identifier       |
| Name   | string     | Product name             |
| Price  | decimal    | Product price            |
| Stock  | int        | Available stock quantity |

---

### Users

| Column | Type        | Description        |
|--------|------------|--------------------|
| Id     | Guid (PK)  | User identifier    |
| Name   | string     | User name          |
| Email  | string     | User email         |

---

### MessageLogs

| Column      | Type        | Description                          |
|------------|------------|--------------------------------------|
| Id         | Guid (PK)  | Unique message identifier            |
| MessageType| string     | Type of message                      |
| Payload    | text       | Message content                      |
| Status     | int        | Processing status                    |
| RetryCount | int        | Number of retry attempts             |

---

## Message Queue

Queue Name: order.created

Sample Message:
{
  "orderId": "guid",
  "userId": "guid"
}

---

## API Endpoints

POST /api/orders  
GET /api/orders/{id}  
GET /api/orders  

---

## Why Event-Driven?

Traditional synchronous systems:
- Block request while processing
- Hard to scale
- Failures affect user experience

Event-driven systems:
- Non-blocking (async processing)
- Easily scalable with multiple workers
- Retry & fault-tolerant
- Decoupled architecture

---

## Advanced Features

- Message retry mechanism
- Dead-letter queue (DLQ)
- Idempotency handling
- Logging & monitoring

---

## Getting Started

1. Clone repo
```bash
git clone https://github.com/RudeusGs/event-driven-orders.git
```
2. Run RabbitMQ (Docker)
```bash
docker run -d --hostname rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
3. Run API
```bash
cd src/OrderFlow.API  
dotnet run  
```
4. Run Worker
```bash
cd src/OrderFlow.Worker  
dotnet run  
```
---

## Future Improvements

- Microservices separation (Payment, Inventory)
- Saga pattern implementation
- Distributed tracing
- Kubernetes deployment

---

## Author

Built for learning and demonstrating event-driven architecture with message queues.
