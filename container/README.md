# Containers

This document describes how to utilize the provided Dockerfiles for containerization.

## Dockerfile Selection

### Freyja Apps

To containerize the [Example Freyja Apps](../freyja_apps/) use one of the following dockerfiles:

- [Dockerfile.freyja_apps.amd64](../Dockerfile.freyja_apps.amd64) - For x86-64 architecture.
- [Dockerfile.freyja_apps.arm64](../Dockerfile.freyja_apps.arm64) - For aarch64 architecture.

These dockerfiles default to the
[In Memory Example Application](../freyja_apps/in_memory/).

Currently the following example applications are supported:

- [In Memory Example Application](../freyja_apps/in_memory/README.md)
- [Ibeji Adapter Example Application](../freyja_apps/ibeji_adapter/README.md)
- [End to End Example Application](../freyja_apps/e2e/README.md)

See [Docker Containers](#docker-containers) or [Podman Containers](#podman-containers) for
information on how to build and run supported applications in a container.

### Freyja Cloud Connectors

To containerize the
[Azure Digitals Twin Cloud Connector](../cloud_connectors/azure/digital_twins_connector/README.md)
use one of the following dockerfiles:

- [cloud_connectors/azure/Dockerfile.dt_connector.amd64](../cloud_connectors/azure/Dockerfile.dt_connector.amd64) -
For x86-64 architecture.
- [cloud_connectors/azure/Dockerfile.dt_connector.arm64](../cloud_connectors/azure/Dockerfile.dt_connector.arm64) -
For aarch64 architecture.

See [Docker Containers](#docker-containers) or [Podman Containers](#podman-containers) for
information on how to build and run supported applications in a container.

## Configuration Overrides

This repository provides overrides for the samples listed under
[Supported Ibeji Samples](../README.md#supported-ibeji-samples). Configuration is provided in the
[`.freyja`](../.freyja/) directory, with each sample having its own directory. For example, the
configuration for the
[Ibeji mixed sample](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed) is provided
under the [.freyja/mixed_sample](../.freyja/mixed_sample/) directory.

## Docker Containers

### Prequisites

[Install Docker](https://docs.docker.com/engine/install/)

### Running in Docker

To run the service in a Docker container:

>Note: Before running any of the following commands, replace all placeholders (wrapped with `<>`).

1. Run the following command in the project root directory to build the docker container from the
Dockerfile:

    ```shell
    docker build -t <image_name> -f <Dockerfile> [--build-arg=APP_NAME=<project name>] .
    ```

    For example, to build an image for the `ibeji-adapter` project:

    ```shell
    docker build -t ibeji_adapter -f Dockerfile.freyja_apps.amd64 --build-arg APP_NAME=freyja-ibeji-adapter-app .
    ```

    Or to build an image for the `azure digital twins cloud connector` example for aarch64:

    ```shell
    docker build -t azure_dt_connector -f ./cloud_connectors/azure/Dockerfile.dt_connector.arm64 .
    ```

    >Note: The build arg `APP_NAME` needs to be passed in for all example freyja applications to
    build the correct example.

1. Once the container has been built, start the container in interactive mode with the following
command in the project root directory:

    ```shell
    docker run --name <container_name> --network=host -it --rm <image_name>
    ```

    For example, to run the `ibeji_adapter` image built in step 1:

    ```shell
    docker run --name ibeji_adapter --network=host -it --rm ibeji_adapter
    ```

    >Note: A custom network is recommended when using a container for anything but testing.

1. To detach from the container, enter:

    <kbd>Ctrl</kbd> + <kbd>p</kbd>, <kbd>Ctrl</kbd> + <kbd>q</kbd>

1. To stop the container, enter:

    ```shell
    docker stop <container_name>
    ```

    For example, to stop the `ibeji_adapter` container started in step 2:

    ```shell
    docker stop ibeji_adapter
    ```

### Running in Docker with overridden configuration

Follow the steps in [Running in Docker](#running-in-docker) to build the container.

1. To run the container with overridden configuration, create your config file and set an
environment variable called CONFIG_HOME to the path to the config file:

    ```shell
    export CONFIG_HOME={path to directory containing config file}
    ```

    For example, to set the configuration for the
    [Ibeji mixed sample](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed), run:

    ```shell
    export CONFIG_HOME={path-to-repo-root}/.freyja/mixed_sample/config/
    ```

    >Note: See [Configuration Overrides](#configuration-overrides) for information on how to find
    the provided configuration for the
    [Supported Ibeji Samples](../README.md#supported-ibeji-samples).

1. Then run the container with the following command:

    ```shell
    docker run -v ${CONFIG_HOME}:/mnt/config --name <container_name> --network=host -it --rm <image_name>
    ```

    For example, to run the `ibeji_adapter` image with overridden configuration:

    ```shell
    docker run -v ${CONFIG_HOME}:/mnt/config --name ibeji_adapter --network=host -it --rm ibeji_adapter
    ```

## Podman Containers

### Prequisites

[Install Podman](https://podman.io/docs/installation)

### Running in Podman

To run the service in a Podman container:

>Note: Before running any of the following commands, replace all placeholders (wrapped with `<>`).

1. Run the following command in the project root directory to build the podman container from the
Dockerfile:

    ```shell
    podman build -t <image_name> -f <Dockerfile> [--build-arg=APP_NAME=<project name>] .
    ```

    For example, to build an image for the `ibeji-adapter` project:

    ```shell
    podman build -t ibeji_adapter -f Dockerfile.freyja_apps.amd64 --build-arg APP_NAME=freyja-ibeji-adapter-app .
    ```

    Or to build an image for the `azure digital twins cloud connector` example for aarch64:

    ```shell
    podman build -t azure_dt_connector -f ./cloud_connectors/azure/Dockerfile.dt_connector.arm64 .
    ```

    >Note: The build arg `APP_NAME` needs to be passed in for all example freyja applications to
    build the correct example.

1. Once the container has been built, start the container with the following command in the project
root directory:

    ```shell
    podman run --network=host <image_name>
    ```

    For example, to run the `ibeji_adapter` image built in step 1:

    ```shell
    podman run --network=host ibeji_adapter
    ```

    >Note: A custom network is recommended when using a container for anything but testing.

1. To stop the container, run:

    ```shell
    podman ps -f ancestor=<image_name> --format="{{.Names}}" | xargs podman stop
    ```

    For example, to stop the `ibeji_adapter` container started in step 2:

    ```shell
    podman ps -f ancestor=localhost/ibeji_adapter:latest --format="{{.Names}}" | xargs podman stop
    ```

### Running in Podman with overridden configuration

Follow the steps in [Running in Podman](#running-in-podman) to build the container.

1. To run the container with overridden configuration, create your config file and set an
environment variable called CONFIG_HOME to the path to the config file:

    ```shell
    export CONFIG_HOME={path to directory containing config file}
    ```

    For example, to set the configuration for the
    [Ibeji mixed sample](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed), run:

    ```shell
    export CONFIG_HOME={path-to-repo-root}/.freyja/mixed_sample/config/
    ```

    >Note: See [Configuration Overrides](#configuration-overrides) for information on how to find
    the provided configuration for the
    [Supported Ibeji Samples](../README.md#supported-ibeji-samples).

1. Then run the container with the following command:

    ```shell
    podman run --mount=type=bind,src=${CONFIG_HOME},dst=/mnt/config,ro=true --network=host <image_name>
    ```

    For example, to run the `ibeji_adapter` image with overridden configuration:

    ```shell
    podman run --mount=type=bind,src=${CONFIG_HOME},dst=/mnt/config,ro=true --network=host ibeji_adapter
    ```
