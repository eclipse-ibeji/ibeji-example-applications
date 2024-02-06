# Freyja App Template

This is a template for creating a Freyja application. For more information about writing Freyja applications, see the [Freyja documentation on custom adapters](https://github.com/eclipse-ibeji/freyja/blob/main/docs/tutorials/custom-adapters.md).

## Instructions

To create your own Freyja application, you can copy this template and make the following changes:

1. Choose the adapters to use. Freyja requires users to select a Cloud Adapter, a Digital Twin Adapter, a Mapping Adapter, and at least one Data Adapter Factory. Some potential choices are:
    1. Freyja mocks. These are enumerated [in the Freyja quickstart documentation](https://github.com/eclipse-ibeji/freyja/blob/main/docs/tutorials/quickstart.md#appendix-a) and are generally only suitable for tests or demos.
    1. [Example adapters from this repository](../../freyja_adapters/).
    1. A custom adapter implementation. For more information on how to write and use these, see the see the [Freyja documentation on custom adapters](https://github.com/eclipse-ibeji/freyja/blob/main/docs/tutorials/custom-adapters.md).
1. Edit `Cargo.template.toml`:
    1. Add dependencies for the package(s) you need for your adapters. You can implement adapters in the same crate and import their dependencies here, or you can implement them in separate crates and import those.
    1. Update the package name and version as desired.
    1. (Optional) Add a `rev` property to the `freyja` dependency if you want to use a version other than the most recent version. Note that when you pull a package from a git repository, the version will be locked in the `Cargo.lock` file until you run `cargo update` ([more info](https://doc.rust-lang.org/cargo/reference/specifying-dependencies.html#specifying-dependencies-from-git-repositories)), so this will not automatically pull future changes that are potentially breaking.
    1. Rename the file to `Cargo.toml` so that Cargo will be able to find it.
1. Edit `src/main.rs`:
    1. Import your adapters with `use` statements.
    1. Change the type names in the `freyja_main!` macro invocation to your imported types. Order matters here, so make sure you list your adapters in the same order as they are presented in the template.

To build and run your application, you can use Cargo commands such as `cargo run`. When working with this repository it's recommended to use the `-p` argument followed by the application package name with Cargo commands. Note that running Cargo commands without specifying the `-p` argument might target every package in the workspace!
