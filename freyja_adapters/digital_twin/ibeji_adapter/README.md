# Ibeji Adapter

The Ibeji Adapter is used to integrate with the [Ibeji In-Vehicle Digital Twin Service](https://github.com/eclipse-ibeji/ibeji), and optionally [Chariott](https://github.com/eclipse-chariott/chariott) to discover Ibeji.

## Configuration

The `res` directory contains two sample config files. The `ibeji_adapter_config.sample.json` will be copied to the build output, so you can edit this file to configure the adapter.

### Common Configuration Fields

Whether or not you are using Chariott, the following config values are required:

- `service_type`: Specifies whether to call Ibeji directly or use Chariott's Service Discovery system to find it. Valid values are `"ChariottDiscoveryService"` and `"InVehicleDigitalTwinService"`.
- `uri`: The URI for the selected service.
- `max_retries`: The maximum number of times to retry failed attempts to communicate with the digital twin service.
- `retry_interval_ms`: The duration between retries in milliseconds.

### Ibeji Without Chariott

To use Ibeji without Chariott, the `service_type` must be `"InVehicleDigitalTwinService"`. No additional configuration is needed to use Ibeji without Chariott.

### Ibeji With Chariott

To use Ibeji with [Chariott's Service Discovery system](https://github.com/eclipse-chariott/chariott/blob/main/service_discovery/README.md), the `service_type` must be `"ChariottDiscoveryService"` and you must specify the following additional config for the adapter:

- `metadata`: Information used to query Chariott's Service Discovery system. This is an object with the following properties:
  - `namespace`: The service namespace for Ibeji.
  - `name`: The service name for Ibeji.
  - `version`: The service version for Ibeji.

Ibeji must also be configured to use Chariott to use this feature.
