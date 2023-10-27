# In-Memory Freyja Example Application

This Freyja Example Application utilizes the in-memory mocks provided by Freyja to demonstrate a minimal example. When you start the application there will initially be no data flowing, but over time signals will get added to the mock digital twin and mapping client. Data is "emitted" by printing to stdout.

## Build and Run

To build and run the application, run the following from the repo root:

```shell
cargo run -p freyja-in-memory-app
```

This will rebuild the `freyja-in-memory-app` application as necessary and then run it.

## Supported Versions

This template is integrated with [revision `a07b033`](https://github.com/eclipse-ibeji/freyja/commit/a07b03349d23b14d0c215ace341e05d9e4e5195e) (authored 2023-11-09) of the Freyja main project.

## Containerize the In Memory Freyja Example Application

To build and run the application in a container, follow the steps under [Docker](#docker) or
[Podman](#podman). This is not a very useful example but shows how the application can be
containerized.

### Docker

#### Prerequisites

[Install Docker](https://docs.docker.com/engine/install/)

#### Running in Docker

To run the service in a Docker container:

1. Run the following command in the project's root directory to build the docker container from the
Dockerfile:

    ```shell
    docker build -t freyja_in_memory -f Dockerfile.freyja_apps .
    ```

1. Once the container has been built, start the container in interactive mode with the following
command in the project's root directory:

    ```shell
    docker run --name freyja_in_memory --env-file=./container/config/docker.env -it --rm freyja_in_memory
    ```

1. To detach from the container, enter:

    <kbd>Ctrl</kbd> + <kbd>p</kbd>, <kbd>Ctrl</kbd> + <kbd>q</kbd>

1. To stop the container, enter:

    ```shell
    docker stop freyja_in_memory
    ```

### Podman

#### Prerequisites

[Install Podman](https://podman.io/docs/installation)

#### Running in Podman

To run the service in a Podman container:

1. Run the following command in the project's root directory to build the podman container from the
Dockerfile:

    ```shell
    podman build -t freyja_in_memory -f Dockerfile.freyja_apps .
    ```

1. Once the container has been built, start the container with the following command in the
project's root directory:

    ```shell
    podman run --env-file=./container/config/podman.env localhost/freyja_in_memory
    ```

1. To stop the container, run:

    ```shell
    podman ps -f ancestor=localhost/freyja_in_memory:latest --format="{{.Names}}" | xargs podman stop
    ```
