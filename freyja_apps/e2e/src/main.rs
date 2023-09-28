// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use ibeji_adapter::ibeji_adapter::IbejiAdapter;
use in_memory_mock_cloud_adapter::in_memory_mock_cloud_adapter::InMemoryMockCloudAdapter;
use in_memory_mock_mapping_client::in_memory_mock_mapping_client::InMemoryMockMappingClient;

freyja::freyja_main! {
    IbejiAdapter,
    InMemoryMockCloudAdapter,
    InMemoryMockMappingClient
}
