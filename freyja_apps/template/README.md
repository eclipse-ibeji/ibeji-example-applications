# Freyja App Template

This is a template for creating a Freyja application. For more information about writing Freyja applications, see the [Freyja documentation on the subject](https://github.com/eclipse-ibeji/freyja/blob/main/docs/custom-adapters.md).

# Instructions

To edit this template and create a Freyja application, follow these steps:

1. Choose the adapters to use. Freyja requires users to select a Cloud Adapter, a Digital Twin Adapter, and a Mapping Client. Some potential choices are:
    1. Freyja mocks. These are enumerated [here](https://github.com/eclipse-ibeji/freyja/blob/main/docs/quickstart.md#appendix-a) and are generally only suitable for tests or demos.
    1. Example adapters from this repository (coming soon!)
    1. A custom adapter implementation
1. Edit `Cargo.template.toml`:
    1. Change the `rev` property of the `freyja` dependency to match the commit hash of the Freyja version you want to use
    1. Add dependencies for the package(s) you need for your adapters. You can implement adapters in the same crate and import their dependencies here, or you can implement them in separate crates and import those.
    1. Update the package name and version as desired
    1. Rename the file to `Cargo.toml` so that Cargo will be able to find it
1. Edit `src/main.rs`:
    1. Import your adapters with `use` statements
    1. Change the type names in the `freyja_main!` macro invocation to your imported types. Order matters here, so make sure you list your adapters in the same order as they are presented in the template.

To build and run your application, you can use Cargo commands such as `cargo build` and `cargo run`.
