# Container Examples

This document describes how to utilize the provided Dockerfiles for containerization.

## Dockerfile Selection

### Freyja Apps

To containerize the [Example Freyja Apps](../freyja_apps/), use
[Dockerfile.freyja_apps](../Dockerfile.freyja_apps). This dockerfile defaults to the
[In Memory Example Application](../freyja_apps/in_memory/).

Currently the following example applications can be containerized:

- [In Memory Example Application](../freyja_apps/in_memory/)
- [Ibeji Adapter Example Application](../freyja_apps/ibeji_adapter)

Each supported application has a README describing the steps to containerize the application.

### Freyja Cloud Connectors

Coming soon!

### Env Files

To run an application in Docker, ensure that the [docker.env](./config/docker.env) is passed in
when running the container.

To run an application in Podman, ensure that the [podman.env](./config/podman.env) is passed in
when running the container.
