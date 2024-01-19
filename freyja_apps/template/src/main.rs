// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use my_adapters::MyCloudAdapter;
use my_adapters::MyDigitaTwinAdapter;
use my_adapters::MyMappingClient;
use my_adapters::MyDataAdapterFactory;

freyja::freyja_main! {
    MyDigitaTwinAdapter,
    MyCloudAdapter,
    MyMappingClient,
    [MyDataAdapterFactory],
}
