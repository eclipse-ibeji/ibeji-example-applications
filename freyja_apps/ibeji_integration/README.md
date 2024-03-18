# Freyja-Ibeji Integration Example Application

This Freyja Example Application utilizes the [Standard GRPC Digital Twin Adapter](https://github.com/eclipse-ibeji/freyja/tree/main/adapters/digital_twin/grpc_digital_twin_adapter/) configured to connect to Ibeji, as well as the in-memory mock mapping service and cloud connector adapter to show a minimal connected local example of how to retrieve data from the vehicle.

## Build and Run

To build and run the application, follow these steps:

1. (Optional) If necessary, author configuration overrides for the [`InMemoryMockMappingAdapter`](https://github.com/eclipse-ibeji/freyja/tree/main/adapters/mapping/in_memory_mock_mapping_adapter). Refer to the adapter README files for instructions on how to do this. This repository provides overrides for the samples listed under [Supported Ibeji Samples](../../README.md#supported-ibeji-samples).

1. Set the `$FREYJA_HOME` environment variable. For example, to use the provided overrides for the [Ibeji mixed sample](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed), you can run the command below.

        export FREYJA_HOME={path-to-repo-root}/.freyja/mixed_sample

    Alternatively, you can set the variable in a [Cargo configuration file](https://doc.rust-lang.org/cargo/reference/config.html) to only enable the variable while running a Cargo command.

1. Run the following from the repo root:

        cargo run -p freyja-ibeji-integration-app

    This will rebuild the `freyja-ibeji-integration-app` application as necessary and then run it.

## Containerize the Freyja-Ibeji Integration Example Application

To build and run the application in a container, follow the steps under
[Docker Containers](../../container/README.md#docker-containers) or
[Podman Containers](../../container/README.md#podman-containers).

Use `freyja-ibeji-integration-app` for the `APP_NAME` build arg when building the container.
