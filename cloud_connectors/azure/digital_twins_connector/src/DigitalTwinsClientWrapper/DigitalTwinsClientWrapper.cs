// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

using System.ComponentModel;

using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;

namespace Microsoft.ESDV.CloudConnector.Azure
{
    /// <summary>
    /// This class wraps the DigitalTwinsClient class in the Azure Digital Twins SDK
    /// Before calling the UpdateDigitalTwinAsync(...) method, you will need to be authenticated via your terminal by typing
    /// `az login --use-device-code --scope https://digitaltwins.azure.net/.default`
    /// </summary>
    public class DigitalTwinsClientWrapper
    {
        // The Azure Digital Twins Client.
        private readonly DigitalTwinsClient _client;

        // The logger.
        private readonly ILogger<DigitalTwinsClientWrapper> _logger;

        /// <summary>
        /// Checks if a path starts with a slash.
        /// </summary>
        /// <param name="path">the path.</param>
        /// <returns>Returns true if the path starts with a slash, otherwise false.</returns>
        private static bool DoesPathStartsWithSlash(string path)
        {
            return path.StartsWith('/');
        }

        /// <summary>
        /// Constructor for DigitalTwinsClientWrapper
        /// </summary>
        /// <param name="client">A DigitalTwinsClient</param>
        /// <param name="logger">An ILogger</param>
        public DigitalTwinsClientWrapper(DigitalTwinsClient client, ILogger<DigitalTwinsClientWrapper> logger)
        {
            _client = client;
            _logger = logger;
            _logger.LogInformation("Starting Azure Digital Client");
        }

        /// <summary>
        /// Updates a digital twin's property.
        /// </summary>
        /// <example>
        /// Invoking <code>UpdateDigitalTwinAsync("dtmi:sdv:Cloud:Vehicle:Cabin:HVAC:AmbientAirTemperature;1", "44")</code>
        /// sets the dtmi "dtmi:sdv:Cloud:Vehicle:Cabin:HVAC:AmbientAirTemperature;1" to 44.
        /// </example>
        /// <param name="modelID">the model ID that a digital twin instance is based on.</param>
        /// <param name="instanceID">the digital twin instance ID.</param>
        /// <param name="instancePropertyPath">the property path of a digital twin instance to update.</param>
        /// <param name="data">the data used to update a digital twin instance's property.</param>
        /// <exception cref="Azure.RequestFailedException">Rethrown if the client throws this exception</exception>
        /// <exception cref="NotSupportedException">Thrown if the data parameter could not be parsed</exception>
        /// <returns>Returns a task for updating a digital twin instance.</returns>
        public async Task UpdateDigitalTwinAsync(string modelID, string instanceID, string instancePropertyPath, string data)
        {
            List<Type> dataTypes = new() { typeof(double), typeof(bool), typeof(int) };
            var jsonPatchDocument = new JsonPatchDocument();

            foreach (Type type in dataTypes)
            {
                try
                {
                    // Parse the data string to a type
                    dynamic value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(data);

                    if (!DoesPathStartsWithSlash(instancePropertyPath))
                    {
                        instancePropertyPath = "$/{instancePropertyPath}";
                    }

                    // Once we're able to parse the data string to a type
                    // we append it to the jsonPatchDocument
                    jsonPatchDocument.AppendAdd(instancePropertyPath, value);

                    // First UpdateDigitalTwinAsync call may block due to initial authorization.
                    await _client.UpdateDigitalTwinAsync(instanceID, jsonPatchDocument);
                    _logger.LogInformation(
                        "Successfully set instance {InstanceID}{InstancePropertyPath} based on model {ModelID} to {Data}",
                        instanceID,
                        instancePropertyPath,
                        modelID,
                        data);
                    return;
                }
                catch (RequestFailedException ex)
                {
                    _logger.LogError(
                        "Cannot set instance {InstanceID}{InstancePropertyPath} based on model {ModelID} to {Data} due to {Message}",
                        instanceID,
                        instancePropertyPath,
                        modelID,
                        data,
                        ex.Message);
                    throw;
                }
                // Try to parse string data with the next type if we're unsuccessful.
                catch (Exception ex) when (ex is NotSupportedException || ex is ArgumentException || ex is FormatException)
                {
                    continue;
                }
            }

            _logger.LogError(
                "Failed to parse data. Cannot set instance {InstanceID}{InstancePropertyPath} based on model {ModelID} to {Data}",
                instanceID,
                instancePropertyPath,
                modelID,
                data);
            throw new NotSupportedException();
        }
    }
}
