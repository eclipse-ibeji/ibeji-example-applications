# Ibeji Adapter Freyja Example Application

This Freyja Example Application utilizes the [Ibeji Digital Twin Adapter](../../freyja_adapters/digital_twin/ibeji_adapter/) and an in-memory mock cloud connector adapter to show a minimal connected local example of how to retrieve data from the vehicle.

## Build and Run

To build and run the application, follow these steps:

1. (Optional) If necessary, author configuration overrides for the [`InMemoryMockMappingClient`](https://github.com/eclipse-ibeji/freyja/tree/main/mapping_clients/in_memory_mock_mapping_client). Refer to the adapter README files for instructions on how to do this. This repository provides overrides in the [`.freyja`](../../.freyja/) directory that can be used with the [`mixed` sample provided by Ibeji](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed).

1. Set the `$FREYJA_HOME` environment variable. If you are using the provided overrides, you can run the following command to set the variable:

        export FREYJA_HOME={path-to-repo-root}/.freyja

    Alternatively, you can set the variable in a [Cargo configuration file](https://doc.rust-lang.org/cargo/reference/config.html) to only enable the variable while running a Cargo command.

1. Run the following from the repo root:

        cargo run -p freyja-ibeji-adapter-app

    This will rebuild the `freyja-ibeji-adapter-app` application as necessary and then run it.

## Containerize the Ibeji Adapter Freyja Example Application

To build and run the application in a container, follow the steps under [Docker](#docker) or
[Podman](#podman). Ensure that the `$FREYJA_HOME` environment variable is set.

### Docker

#### Prerequisites

[Install Docker](https://docs.docker.com/engine/install/)

#### Running in Docker

To run the service in a Docker container:

1. Run the following command in the project's root directory to build the docker container from the
Dockerfile:

    ```shell
    docker build -t freyja_ibeji_adapter --build-arg APP_NAME=freyja-ibeji-adapter-app -f Dockerfile.freyja_apps .
    ```

    The `APP_NAME` build arg needs to be set as `Dockerfile.freyja_apps` defaults to the
    [In Memory Example Application](../in_memory/).

1. Once the container has been built, start the container in interactive mode with the following
command in the project's root directory:

    ```shell
    docker run -v ${FREYJA_HOME}:/sdv/.freyja --name freyja_ibeji_adapter -p 60010:60010 --env-file=./container/config/docker.env --add-host=host.docker.internal:host-gateway -it --rm freyja_ibeji_adapter
    ```

    `-v` mounts the `$FREYJA_HOME` path set above in the container allowing the application to use
    the provided overrides for the mixed sample in Ibeji. 

1. To detach from the container, enter:

    <kbd>Ctrl</kbd> + <kbd>p</kbd>, <kbd>Ctrl</kbd> + <kbd>q</kbd>

1. To stop the container, enter:

    ```shell
    docker stop freyja_ibeji_adapter
    ```

### Podman

#### Prerequisites

[Install Podman](https://podman.io/docs/installation)

#### Running in Podman

To run the service in a Podman container:

1. Run the following command in the project's root directory to build the podman container from the
Dockerfile:

    ```shell
    podman build -t freyja_ibeji_adapter --build-arg=APP_NAME=freyja-ibeji-adapter-app -f Dockerfile.freyja_apps .
    ```

    The `APP_NAME` build arg needs to be set as `Dockerfile.freyja_apps` defaults to the
    [In Memory Example Application](../in_memory/).

1. Once the container has been built, start the container with the following command in the
project's root directory:

    ```shell
    podman run --mount=type=bind,src=${FREYJA_HOME},dst=/sdv/.freyja,ro=true -p 60010:60010 --env-file=./container/config/podman.env --network=slirp4netns:allow_host_loopback=true localhost/freyja_ibeji_adapter
    ```

    `-v` mounts the `$FREYJA_HOME` path set above in the container allowing the application to use
    the provided overrides for the mixed sample in Ibeji. 

1. To stop the container, run:

    ```shell
    podman ps -f ancestor=localhost/freyja_ibeji_adapter:latest --format="{{.Names}}" | xargs podman stop
    ```
