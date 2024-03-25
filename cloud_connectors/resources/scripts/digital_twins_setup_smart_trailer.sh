#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT license.
# SPDX-License-Identifier: MIT

set -e

# Set the current directory to where the script lives.
cd "$(dirname "$0")"

# Function to display usage information
usage() {
    echo "Usage: $0 [-r|--resource-group-name] <RESOURCE_GROUP_NAME> [-l|--location] <DEPLOYMENT_LOCATION> [-d|--digital-twins-name] <DIGITAL_TWINS_NAME>"
    echo "Example:"
    echo "  $0 -r myRG -l westus2 -d myADT"
}

# Parse command line arguments
while [[ $# -gt 0 ]]
do
    key="$1"

    case $key in
        -r|--resource-group-name)
            resource_group="$2"
            shift # past argument
            shift # past value
            ;;
        -l|--location)
            location="$2"
            shift # past argument
            shift # past value
            ;;
        -d|--digital-twins-name)
            digital_twin_name="$2"
            shift # past argument
            shift # past value
            ;;
        -h|--help)
            usage
            exit 0
            ;;
        *)
            echo "Unknown argument: $key"
            usage
            exit 1
    esac
done

# Check if all required arguments have been set
if [[ -z "${resource_group}" || -z "${location}" || -z "${digital_twin_name}" ]]; then
    echo "Error: Missing required arguments:"
    [[ -z "${resource_group}" ]] && echo "  -r|--resource-group-name"
    [[ -z "${location}" ]] && echo "  -l|--location"
    [[ -z "${digital_twin_name}" ]] && echo "  -d|--digital-twins-name"
    echo -e "\n"
    usage
    exit 1
fi

# Check if the Digital Twins instance exists
if az dt show -n "$digital_twin_name" > /dev/null 2>&1; then
    echo "Digital Twins instance '$digital_twin_name' already exists in resource group '$resource_group'"
else
    echo -e "\nCreating the Azure Digital Twins resource"
    az dt create --dt-name "$digital_twin_name" --resource-group "$resource_group" --location "$location"
fi

# Assign the Digital Twins Data Owner role
echo -e "\nAssigning the Azure Digital Twins Data Owner role"
user_object_id=$(az ad signed-in-user show --query id -o tsv)
az dt role-assignment create --dt-name "$digital_twin_name" --assignee "$user_object_id"  --role "Azure Digital Twins Data Owner"

# Upload the sample DTDL models
echo -e "\nUploading sample DTDL models"
for file in $(find ../sample_dtdl -name "trailer.json"); do
    if ! az dt model create --dt-name ${digital_twin_name} --models $file; then
        echo "$file" DTDL already uploaded.
    fi
done

# Create the Azure Digital Twin instances
echo -e "\nCreating the Azure Digital Twin instances"
az dt twin create --dt-name "$digital_twin_name" --dtmi "dtmi:sdv:Cloud:Trailer;1" --twin-id trailer

echo -e "\nSetup finished for Freyja's Sample Azure Digital Twins"
exit 0