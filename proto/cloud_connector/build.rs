// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use freyja_build_common::compile_remote_proto;
use freyja_proto_common::interface_url;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    compile_remote_proto(interface_url!(freyja, CLOUD_CONNECTOR_INTERFACE), &[])?;

    Ok(())
}
