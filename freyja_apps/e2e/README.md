# End-To-End Freyja Example Application

This Freyja Example Application utilizes the [Ibeji Digital Twin Adapter] and the [Azure Cloud Connector Adapter] to show a minimal end-to-end example of how to sync data from the vehicle to the cloud.

## Build and Run

To build and run the application, run the following from the repo root:

```shell
cargo run -p freyja-e2e-app
```

This will rebuild the application as necessary and then run it. Note that running Cargo commands without specifying the `-p` argument might target every package in the workspace! When working with this repository it's recommended to use the `-p` argument with Cargo commands.