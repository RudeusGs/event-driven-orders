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

Presentation (API)
↓  
Application (Use Cases / Services)  
↓  
Domain (Entities, Enums, Business Rules)  
↓  
Infrastructure (Database, RabbitMQ, External Services)

---

## Tech Stack

- .NET Core Web API
- RabbitMQ (Message Queue)
- PostgreSQL
- Entity Framework Core
- Docker (optional)

---

## System Flow

Client
↓
API (Create Order)
↓
Save Order (Pending)
↓
Publish Event → RabbitMQ
↓
Worker (Consumer)
↓
Process Order (Stock, Payment)
↓
Update Order Status (Completed / Failed)

---

## Order Lifecycle

Pending → Processing → Completed / Failed

---

## Database Design

### Orders
- Id
- UserId
- Status
- TotalAmount
- CreatedAt

### OrderItems
- Id
- OrderId
- ProductId
- Quantity
- Price

### Products
- Id
- Name
- Price
- Stock

### Users
- Id
- Name
- Email

### MessageLogs
- Id
- MessageType
- Payload
- Status
- RetryCount

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
git clone https://github.com/your-username/event-driven-orders.git

2. Run RabbitMQ (Docker)  
docker run -d --hostname rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

3. Run API  
cd src/OrderFlow.API  
dotnet run  

4. Run Worker  
cd src/OrderFlow.Worker  
dotnet run  

---

## Future Improvements

- Microservices separation (Payment, Inventory)
- Saga pattern implementation
- Distributed tracing
- Kubernetes deployment

---

## Author

Built for learning and demonstrating event-driven architecture with message queues.
