// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

#![allow(unused_imports)]

use serde::{Deserialize, Serialize};
use std::env;

/// If feature 'containerize' is set, will modify a localhost uri to point to container's localhost
/// DNS alias. Otherwise, returns the uri as a String.
///
/// # Arguments
/// * `uri` - The uri to potentially modify.
pub fn get_uri(uri: &str) -> Result<String, Box<dyn std::error::Error + Send + Sync>> {
    #[cfg(feature = "containerize")]
    let uri = {
        // Container env variable names.
        let host_gateway_env_var: &str = "HOST_GATEWAY";
        let host_alias_env_var: &str = "LOCALHOST_ALIAS";

        // Return an error if container env variables are not set.
        let host_gateway = env::var(host_gateway_env_var)?;
        let host_alias = env::var(host_alias_env_var)?;

        uri.replace(&host_alias, &host_gateway)
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
