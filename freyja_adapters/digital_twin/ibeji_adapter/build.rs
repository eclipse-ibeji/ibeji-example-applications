// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

use std::{env, fs, path::Path};

const OUT_DIR: &str = "OUT_DIR";
const SAMPLE_CONFIG_FILE: &str = "res/ibeji_adapter_config.sample.json";
const CONFIG_FILE: &str = "ibeji_adapter_config.json";

fn main() {
    // The current directory of the build script is the package's root directory
    let config_path = env::current_dir().unwrap().join(SAMPLE_CONFIG_FILE);

    let target_dir = env::var(OUT_DIR).unwrap();
    let dest_path = Path::new(&target_dir).join(CONFIG_FILE);

    fs::copy(&config_path, dest_path).unwrap();

    println!(
        "The config ibeji_adapter_config.json is located in the {} directory",
        target_dir
    );
    println!("cargo:rerun-if-changed={}", config_path.to_str().unwrap());
}