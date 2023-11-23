#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT license.
# SPDX-License-Identifier: MIT

# Exits immediately on failure.
set -e

# Copy any configuration files present to service configuration.
cp -rf /mnt/config /sdv/

# Acquire access token scoped to the Azure Digital Twin Service.
az login --use-device-code --scope https://digitaltwins.azure.net/.default

# Start the Digital Twin Connector service.
dotnet ./DigitalTwinsConnector.dll