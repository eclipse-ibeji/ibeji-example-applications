# End-To-End Freyja Example Application

This Freyja Example Application utilizes the [Ibeji Digital Twin Adapter](../../freyja_adapters/digital_twin/ibeji_adapter/) and the [Azure Cloud Connector Adapter](../../freyja_adapters/cloud/azure_cloud_connector_adapter/) to show a minimal end-to-end example of how to sync data from the vehicle to the cloud.

## Build and Run

To build and run the application, follow these steps:

1. (Optional) If necessary, author configuration overrides for the [`InMemoryMockMappingClient`](https://github.com/eclipse-ibeji/freyja/tree/main/mapping_clients/in_memory_mock_mapping_client). Refer to the adapter README files for instructions on how to do this. This repository provides overrides in the [`.freyja`](../../.freyja/) directory that can be used with the [`mixed` sample provided by Ibeji](https://github.com/eclipse-ibeji/ibeji/tree/main/samples/mixed).

1. Set the `$FREYJA_HOME` environment variable. If you are using the provided overrides, you can run the following command to set the variable:

        export FREYJA_HOME={path-to-repo-root}/.freyja

    Alternatively, you can set the variable in a [Cargo configuration file](https://doc.rust-lang.org/cargo/reference/config.html) to only enable the variable while running a Cargo command.

1. Run the following from the repo root:

        cargo run -p freyja-e2e-app

        This will rebuild the `freyja-e2e-app` application as necessary and then run it.
