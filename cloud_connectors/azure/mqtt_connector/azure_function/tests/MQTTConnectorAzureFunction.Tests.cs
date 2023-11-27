// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// SPDX-License-Identifier: MIT

using System.Text.Json;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace Microsoft.ESDV.CloudConnector.Azure.Tests
{
    [TestFixture]
    public class MQTTConnectorAzureFunctionTests
    {
        private DigitalTwinsClient _client;
        private DigitalTwinsInstance _instance;
        private MQTTConnectorAzureFunction _connector;

        [SetUp]
        public void Setup()
        {
            _client = new Mock<DigitalTwinsClient>().Object;
            _connector = new MQTTConnectorAzureFunction(new Mock<ILogger<MQTTConnectorAzureFunction>>().Object);
            _instance = new DigitalTwinsInstance
            {
                ModelId = "some-model",
                InstanceId = "some-instance",
                InstancePropertyPath = "some-instance-property",
                Data = null
            };
        }

        [Test]
        public void ConvertStringToDataTypeShouldSucceed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(MQTTConnectorAzureFunction.GetDataTypeFromString("int"), Is.EqualTo(typeof(int)));
                Assert.That(MQTTConnectorAzureFunction.GetDataTypeFromString("double"), Is.EqualTo(typeof(double)));
                Assert.That(MQTTConnectorAzureFunction.GetDataTypeFromString("boolean"), Is.EqualTo(typeof(bool)));
            });
            Assert.Throws<NotSupportedException>(() => MQTTConnectorAzureFunction.GetDataTypeFromString("invalid-converter"));
        }

        [Test]
        public async Task UpdateDigitalTwinAsyncShouldSucceed()
        {
            _instance.Data = "44.5";
            await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance, "double");
            Assert.Pass();

            _instance.Data = "44";
            await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance, "int");
            Assert.Pass();

            _instance.Data = "true";
            await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance, "boolean");
            Assert.Pass();
        }

        [Test]
        public void UpdateDigitalTwinAsyncThrowNotSupported()
        {
            _instance.Data = null;
            Assert.ThrowsAsync<NotSupportedException>(async () => await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance));

            _instance.Data = "test1234";
            Assert.ThrowsAsync<NotSupportedException>(async () => await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance, "invalid-converter"));

            _instance.Data = "";
            Assert.ThrowsAsync<NotSupportedException>(async () => await MQTTConnectorAzureFunction.UpdateDigitalTwinAsync(_client, _instance, "double"));
        }

        [Test]
        public void CanDeserializeDigitalTwinsInstance()
        {
            string input = @"{
                ""model_id"": ""some-model"",
                ""instance_id"": ""some-instance"",
                ""instance_property_path"": ""some-instance-property"",
                ""data"": ""42""
            }";

            BinaryData data = BinaryData.FromString(input);

            Assert.DoesNotThrow(() => data.ToObjectFromJson<DigitalTwinsInstance>());
        }
    }
}
