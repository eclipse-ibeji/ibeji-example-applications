# End-To-End Freyja Example Application

This Freyja Example Application utilizes the [Ibeji Digital Twin Adapter](https://github.com/eclipse-ibeji/freyja/tree/main/adapters/digital_twin/ibeji_adapter/) and the [Standard GRPC Cloud Adapter](https://github.com/eclipse-ibeji/freyja/tree/main/adapters/cloud/grpc_cloud_adapter/) to show a minimal end-to-end example of how to sync data from the vehicle to the cloud.

## Build and Run

To build and run the application, follow these steps:

1. (Optional) If necessary, author configuration overrides for the [`InMemoryMockMappingAdapter`](https://github.com/eclipse-ibeji/freyja/tree/main/adapters/mapping/in_memory_mock_mapping_adapter). Refer to the adapter README files for instructions on how to do this. This repository provides overrides for the samples listed under [Supported Samples](../../README.md#supported-ibeji-samples).

1. Set the `$FREYJA_HOME` environment variable. For example, to use the provided overrides for the [Ibeji mixed sample](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed), you can run the command below.

        export FREYJA_HOME={path-to-repo-root}/.freyja/mixed_sample

    Alternatively, you can set the variable in a [Cargo configuration file](https://doc.rust-lang.org/cargo/reference/config.html) to only enable the variable while running a Cargo command.

1. Run the following from the repo root:

        cargo run -p freyja-e2e-app

        This will rebuild the `freyja-e2e-app` application as necessary and then run it.

## Containerize the End-To-End Freyja Example Application

To build and run the application in a container, follow the steps under
[Docker Containers](../../container/README.md#docker-containers) or
[Podman Containers](../../container/README.md#podman-containers).

Use `freyja-e2e-app` for the `APP_NAME` build arg when building the container.
