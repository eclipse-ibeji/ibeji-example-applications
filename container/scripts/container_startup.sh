#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT license.
# SPDX-License-Identifier: MIT

# Exits immediately on failure.
set -eu

# Copy any configuration files present to service configuration.
# If there is a configuration file with the same name at `/sdv/.freyja/config` this will overwrite
# that file with the mounted configuration file.
cp -rf /mnt/config /sdv/.freyja

# Start the Freyja service.
/sdv/service