// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Azure;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace Microsoft.ESDV.CloudConnector.Azure {
    /// <summary>
    /// This class contains the info to target an Azure Digital Twin instance.
    /// </summary>
    public class DigitalTwinsInstance
    {
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        [JsonPropertyName("instance_id")]
        public string InstanceId { get; set; }

        [JsonPropertyName("instance_property_path")]
        public string InstancePropertyPath { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }

    public class MQTTConnectorAzureFunction
    {
        private readonly ILogger _logger;

        private const string KEYVAULT_SETTINGS = "KEYVAULT_SETTINGS";

        // Maps a string data type name to its concrete data type.
        private static readonly Dictionary<string, Type> dataTypeNameToConverterMap = new()
        {
            { "int", typeof(int) },
            { "double", typeof(double) },
            { "boolean", typeof(bool) }
        };

        public MQTTConnectorAzureFunction(ILogger<MQTTConnectorAzureFunction> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Checks if a path starts with a slash.
        /// </summary>
        /// <param name="path">the path.</param>
        /// <returns>Returns true if the path starts with a slash, otherwise false.</returns>
        public static bool DoesPathStartsWithSlash(string path)
        {
            return path.StartsWith('/');
        }

        /// <summary>
        /// Gets the data type from a data type name.
        /// </summary>
        /// <param name="dataTypeName">the name of the data type.
        /// <exception cref="NotSupportedException">Thrown if the data type is not supported.</exception>
        /// <returns>Returns a task for updating a digital twin instance.</returns>
        public static Type GetDataTypeFromString(string dataTypeName)
        {
            if (!dataTypeNameToConverterMap.TryGetValue(dataTypeName, out Type value))
            {
                throw new NotSupportedException($"No conversion for {dataTypeName}");
            }

            return value;
        }

        /// <summary>
        /// Updates a digital twin's property.
        /// </summary>
        /// <param name="client">the Azure Digital Twins client.</param>
        /// <param name="instance">the digital twin instance to update.</param>
        /// <param name="dataTypeName">the name of the data type.
        /// <returns>Returns a task for updating a digital twin instance.</returns>
        public static async Task UpdateDigitalTwinAsync(DigitalTwinsClient client, DigitalTwinsInstance instance, string dataTypeName = "double")
        {
            JsonPatchDocument jsonPatchDocument = new();

            try
            {
                // Get the concrete data type of an instance's data based on its string data type name
                // then uses that concrete data type to change the data from string to its concrete data type.
                Type dataType = GetDataTypeFromString(dataTypeName);
                dynamic convertedDataToType = Convert.ChangeType(instance.Data, dataType, CultureInfo.InvariantCulture);

                if (!DoesPathStartsWithSlash(instance.InstancePropertyPath))
                {
                    instance.InstancePropertyPath = $"/{instance.InstancePropertyPath}";
                }
                jsonPatchDocument.AppendAdd(instance.InstancePropertyPath, convertedDataToType);
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is InvalidCastException || ex is FormatException)
            {
                throw new NotSupportedException($"Cannot convert {instance.Data}. {ex.Message}");
            }

            try
            {
                await client.UpdateDigitalTwinAsync(instance.InstanceId, jsonPatchDocument);
            }
            catch(RequestFailedException ex)
            {
                string errorMessage = @$"Cannot set instance {instance.InstanceId}{instance.InstancePropertyPath}
                    based on model {instance.ModelId} to {instance.Data} due to {ex.Message}";
                throw new NotSupportedException(errorMessage);
            }
        }

        /// <summary>
        /// An Azure Function that updates an Azure Digital Twin based on the request.
        /// </summary>
        /// <param name="cloudEvent">the cloudEvent request that is received.</param>
        /// <param name="logger">the logger</param>
        /// <exception>An exception is thrown if the Azure Digital Twin client cannot update an instance.</exception>
        /// <returns></returns>
        [FunctionName("MQTTConnectorAzureFunction")]
        public async Task Run([EventGridTrigger] CloudEvent cloudEvent)
        {
            DigitalTwinsInstance instance = cloudEvent.Data.ToObjectFromJson<DigitalTwinsInstance>();

            try
            {
                DefaultAzureCredential credential = new();
                string adt_instance_url = Environment.GetEnvironmentVariable(KEYVAULT_SETTINGS, EnvironmentVariableTarget.Process);
                DigitalTwinsClient client = new(new Uri(adt_instance_url), credential);
                await UpdateDigitalTwinAsync(client, instance);
                _logger.LogInformation(@$"Successfully set instance {instance.InstanceId}{instance.InstancePropertyPath}
                    based on model {instance.ModelId} to {instance.Data}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
