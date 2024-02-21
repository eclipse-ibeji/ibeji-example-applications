#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT license.
# SPDX-License-Identifier: MIT

# Running `cargo test --workspace --doc` will fail if the workspace contains only binary packages.
# Normally doc tests are only supported for library packages and binary packages will be skipped,
# but for some reason if a workspace contains only binary packages then the test command will fail.
# This script runs the test command and suppresses the error caused in this case.
# Unfortunately there are no tools to easily navigate a workspace and filter packages by type,
# so this script works by checking the output of the test command.
# Although this is not robust, it is far simpler than the alternative.

OUTPUT=$(cargo test --workspace --doc 2>&1)
RETURN_CODE=$?

echo $OUTPUT
echo

if [[ "$OUTPUT" =~ (error: no library targets found in packages) ]]; then
    echo "Detected 'no library targets' error, which is being suppressed"
    exit 0
else
    echo "Command executed with code " $RETURN_CODE
    exit $RETURN_CODE
fi