```markdown
# 🛠 Practice – Production-like Microservices System

Этот репозиторий содержит **production-like** решение с микросервисной архитектурой, реализованное в рамках практики Ozon.Route256. Все сервисы запускаются через `docker-compose`, используют **gRPC**, **Kafka**, **PostgreSQL**, симуляторы нагрузки и Service Discovery.

## 📦 Архитектура

Проект включает **4 микросервиса**, а также необходимые инфраструктурные компоненты:

| Компонент               | Назначение                                   |
|-------------------------|----------------------------------------------|
| `customer-service`      | gRPC-сервис для работы с клиентами           |
| `orders-generator-*`    | Генераторы заказов (Web, Mobile, API)        |
| `service-discovery`     | Service Discovery (распределение кластеров)  |
| `logistic-simulator`    | Симулятор логистики                          |
| `Kafka (broker-1/2)`    | Очереди сообщений                            |
| `Zookeeper`             | Координация Kafka-брокеров                   |
| `PostgreSQL`            | Хранилище данных клиентов                    |

Все сервисы взаимодействуют через **gRPC** или **Kafka**, используют общие `.proto`-контракты и работают в локальной среде, имитируя продакшн.

## 🚀 Быстрый старт

> ⚠️ Требования: Docker и Docker Compose установлены.

### 1. Клонируйте репозиторий

```bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name
````

### 2. Запустите систему

```bash
docker-compose up --build
```

> Используйте `--build`, чтобы пересобрать образы при изменении кода.

### 3. Проверка

После запуска:

* `http://localhost:5081` — gRPC endpoint `customer-service`
* `http://localhost:5500` — Service Discovery HTTP
* Kafka доступна на портах `29091` и `29092`
* PostgreSQL на порту `5400` (`user=test, password=test`)

Все сервисы стартуют с healthcheck'ами и полностью готовы к работе.

## 🧩 Технологии

* [.NET 8 / C# 12](https://learn.microsoft.com/en-us/dotnet/)
* [gRPC](https://grpc.io/)
* [Apache Kafka](https://kafka.apache.org/)
* [Docker Compose](https://docs.docker.com/compose/)
* [PostgreSQL](https://www.postgresql.org/)
* [Zookeeper](https://zookeeper.apache.org/)
* Clean Architecture подход (по слоям)

## 🔧 Структура каталогов

```
src/
├── Ozon.Route256.Practice.CustomerService
├── Ozon.Route256.Practice.OrdersGenerator
├── Ozon.Route256.Practice.ServiceDiscovery
├── Ozon.Route256.Practice.LogisticsSimulator
```

Каждый сервис содержит `Dockerfile`, настройки и слой инфраструктуры, необходимые для автономного запуска.

## 🧪 Тестирование

Проект включает генерацию заказов через три источника (`Web`, `Mobile`, `Api`), которые публикуют сообщения в Kafka. `customer-service` обрабатывает запросы через gRPC. Поведение системы можно наблюдать через логи.

## 🧼 Завершение работы

```bash
docker-compose down
```

Если нужно освободить место:

```bash
docker system prune -a
```

## 📎 Лицензия

MIT License.
