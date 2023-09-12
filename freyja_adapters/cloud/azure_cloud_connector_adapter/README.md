# Azure Cloud Connector Adapter

This is an example implementation of an adapter for the [Azure Cloud Connectors](../../../cloud_connectors/azure/README.md).

This adapter is used to communicate with an Azure Cloud Connector to synchronize in-vehicle signals with the cloud.

## Prerequisites

### Azure Cloud Connector

You will need to either have the [Azure Digital Twins Connector](../../../cloud_connectors/azure/digital_twins_connector/README.md) or [Azure MQTT Connector](../../../cloud_connectors/azure/mqtt_connector/README.md) running. This cloud adapter is capable of interfacing with both connectors.

## Configuration

This cloud adapter can be configured using the `res/azure_cloud_connector_adapter_config.json` file. This file is automatically copied to the build output and contains the following properties:

- `cloud_connector_url`: The URL of the cloud connector. This should match the configuration for your cloud connector.
- `max_retries`: The maximum number of times to retry failed attempts to send data to the cloud connector.
- `retry_interval_ms`: The duration between retries.
