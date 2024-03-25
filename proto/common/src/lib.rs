// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

// This module contains constants related to remote protobuf files as well as helpers for referencing them.
// This also helps manage interface versions from a central location, which is particularly helpful for Ibeji since
// the interfaces are referenced in two different crates.

pub const GITHUB_BASE_URL: &str = "https://raw.githubusercontent.com";

pub mod freyja {
    pub const REPO_NAME: &str = "eclipse-ibeji/freyja";
    pub const VERSION: &str = "0.1.0";

    pub mod interfaces {
        pub const CLOUD_CONNECTOR_INTERFACE: &str =
            "interfaces/cloud_connector/v1/cloud_connector.proto";
        pub const MAPPING_SERVICE_INTERFACE: &str =
            "interfaces/mapping_service/v1/mapping_service.proto";
    }
}
