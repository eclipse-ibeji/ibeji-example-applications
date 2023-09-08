# In-Memory Freyja Example Application

This Freyja Example Application utilizes the in-memory mocks provided by Freyja to demonstrate a minimal example. When you start the application there will initially be no data flowing, but over time signals will get added to the mock digital twin and mapping client. Data is "emitted" by printing to stdout.

## Build and Run

To build and run the application, run the following from the repo root:

```shell
cargo build -p freyja-in-memory-app
cargo run -p freyja-in-memory-app
```

Note that running `cargo build` without specifying the `-p` argument will build every project in the workspace! When working with this repository it's recommended to use the `-p` argument with Cargo commands.