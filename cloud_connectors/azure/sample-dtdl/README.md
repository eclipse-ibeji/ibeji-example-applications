# Azure Digital Twins Setup

This document descibes how to setup and Azure Digital Twins instance to use the sample DTDL in this folder.

## Automated Azure Digital Twins Setup

Before starting this section, please read [Prerequisites for Automated Deployment of Azure Resources](../README.md#prerequisites-for-automated-deployment-of-azure-resources).

1. Sign in with Azure CLI. Follow the prompts after entering the following command.

```shell
az login --use-device-code
```

1. Deploy Azure Digital Twins to your resource group.

```shell
cd {repo-root}/cloud_connectors/azure/scripts
chmod +x digital_twins_setup.sh
./digital_twins_setup.sh -r {myRG} -l westus2 -d {myADT}
```

### Automated Azure Digital Twins Setup for Smart Trailer Example

Before starting this section, please read [Prerequisites for Automated Deployment of Azure Resources](../README.md#prerequisites-for-automated-deployment-of-azure-resources).

1. Sign in with Azure CLI. Follow the prompts after entering the following command.

```shell
az login --use-device-code
```

1. Deploy Azure Digital Twins to your resource group.

```shell
cd {repo-root}/cloud_connectors/azure/scripts
chmod +x digital_twins_setup_smart_trailer.sh
./digital_twins_setup_smart_trailer.sh -r {myRG} -l westus2 -d {myADT}
```

### Manual Azure Digital Twins Setup

If you have successfully ran the `digital_twins_setup.sh`, you do not need to follow this section.

The steps below will guide you on manually deploying the Azure Digital Twins resource to your resource group, and creating your Azure Digital Twins instances.

1. Set up your [Azure Digital Twin Instance](https://learn.microsoft.com/en-us/azure/digital-twins/quickstart-azure-digital-twins-explorer#set-up-azure-digital-twins).

    If you wish to use the default mappings in this repository, create the following instances:

    * vehicle
    * hvac
    * obd

    For each instance, use the respective DTDL provided in this directory.

    In your hvac instance, name the two properties as `AmbientAirTemperature` and `IsAirConditioningActive`.

    In your obd instance, name the single property as `HybridBatteryRemaining`.

1. Follow the *Open instance in Azure Digital Twins Explorer* section under [Set up Azure Digital Twins](https://learn.microsoft.com/en-us/azure/digital-twins/quickstart-azure-digital-twins-explorer#set-up-azure-digital-twins) to get the Azure Digital Twin URL of your Azure Digital Twin instance.
