# In-Memory Freyja Example Application

This Freyja Example Application utilizes the in-memory mocks provided by Freyja to demonstrate a minimal example. When you start the application there will initially be no data flowing, but over time signals will get added to the mock digital twin and mapping client. Data is "emitted" by printing to stdout.

## Build and Run

To build and run the application, run the following from the repo root:

```shell
cargo run -p freyja-in-memory-app
```

This will rebuild the application as necessary and then run it. Note that running `cargo run` without specifying the `-p` argument will build every project in the workspace! When working with this repository it's recommended to use the `-p` argument with Cargo commands.

## Supported Versions

This template is integrated with [revision `a07b033`](https://github.com/eclipse-ibeji/freyja/commit/a07b03349d23b14d0c215ace341e05d9e4e5195e) (authored 2023-11-09) of the Freyja main project.
