// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use ibeji_adapter::ibeji_adapter::IbejiAdapter;
use azure_cloud_connector_adapter::azure_cloud_connector_adapter::AzureCloudConnectorAdapter;
use in_memory_mock_mapping_client::in_memory_mock_mapping_client::InMemoryMockMappingClient;

freyja::freyja_main! {
    IbejiAdapter,
    AzureCloudConnectorAdapter,
    InMemoryMockMappingClient
}
