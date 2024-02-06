// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use grpc_data_adapter::grpc_data_adapter_factory::GRPCDataAdapterFactory;
use ibeji_adapter::ibeji_adapter::IbejiAdapter;
use in_memory_mock_cloud_adapter::in_memory_mock_cloud_adapter::InMemoryMockCloudAdapter;
use in_memory_mock_mapping_adapter::in_memory_mock_mapping_adapter::InMemoryMockMappingAdapter;
use managed_subscribe_data_adapter::managed_subscribe_data_adapter_factory::ManagedSubscribeDataAdapterFactory;
use mqtt_data_adapter::mqtt_data_adapter_factory::MqttDataAdapterFactory;

freyja::freyja_main! {
    IbejiAdapter,
    InMemoryMockCloudAdapter,
    InMemoryMockMappingAdapter,
    [GRPCDataAdapterFactory, ManagedSubscribeDataAdapterFactory, MqttDataAdapterFactory],
}
