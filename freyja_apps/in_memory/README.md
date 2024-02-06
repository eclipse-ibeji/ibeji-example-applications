# In-Memory Freyja Example Application

This Freyja Example Application utilizes the in-memory mocks provided by Freyja to demonstrate a minimal example. When you start the application there will initially be no data flowing, but over time signals will get added to the mock digital twin and mapping client. Data is "emitted" by printing to stdout.

## Build and Run

To build and run the application, run the following from the repo root:

```shell
cargo run -p freyja-in-memory-app
```

This will rebuild the `freyja-in-memory-app` application as necessary and then run it.

## Containerize the In Memory Freyja Example Application

To build and run the application in a container, follow the steps under
[Docker Containers](../../container/README.md#docker-containers) or
[Podman Containers](../../container/README.md#podman-containers).

Use `freyja-in-memory-app` for the `APP_NAME` build arg when building the container.

>Note: This is not a very useful example but shows how the application can be containerized.
