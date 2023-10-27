// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

#![allow(unused_imports)]

use std::env;
use serde::{Deserialize, Serialize};

/// If feature 'containerize' is set, will modify a localhost uri to point to container's localhost
/// DNS alias. Otherwise, returns the uri as a String.
///
/// # Arguments
/// * `uri` - The uri to potentially modify.
pub fn get_uri(uri: &str) -> Result<String, Box<dyn std::error::Error + Send + Sync>> {
    #[cfg(feature = "containerize")]
    let uri = {
        // Container env variable names.
        const HOST_GATEWAY_ENV_VAR: &str = "HOST_GATEWAY";
        const LOCALHOST_ALIAS_ENV_VAR: &str = "LOCALHOST_ALIAS";

        // Return an error if container env variables are not set.
        let host_gateway = env::var(HOST_GATEWAY_ENV_VAR)?;
        let localhost_alias = env::var(LOCALHOST_ALIAS_ENV_VAR)?; // DevSkim: ignore DS162092

        uri.replace(&localhost_alias, &host_gateway) // DevSkim: ignore DS162092
    };

    Ok(uri.to_string())
}

/// Configuration for the Ibeji Adapter.
/// Supports two different schemas based on the service discovery method.
#[derive(Clone, Serialize, Deserialize)]
#[serde(tag = "service_discovery_method")]
pub enum Config {
    /// Use a URI from the config for the In-Vehicle Digital Twin Service
    FromConfig {
        /// The URI for the In-Vehicle Digital Twin Service
        uri: String,

        /// The maximum number of retries for communication attempts
        max_retries: u32,

        /// The duration between retries in milliseconds
        retry_interval_ms: u64,
    },

    /// Use Chariott's Service Discovery system to discover the In-Vehicle Digital Twin Service
    ChariottServiceDiscovery {
        /// The URI for the Chariott Discovery Service
        uri: String,

        /// The maximum number of retries for communication attempts
        max_retries: u32,

        /// The duration between retries in milliseconds
        retry_interval_ms: u64,

        /// The request to send to Chariott
        discover_request: ChariottDiscoverRequest,
    },
}

/// A Chariott Service Discovery request
#[derive(Clone, Serialize, Deserialize)]
pub struct ChariottDiscoverRequest {
    /// The service namespace
    pub namespace: String,

    /// The service name
    pub name: String,

    /// The service version
    pub version: String,
}
