﻿/*
 * This file is part of Astarte.
 *
 * Copyright 2023 SECO Mind Srl
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * SPDX-License-Identifier: Apache-2.0
 */

using MQTTnet.Client.Options;

namespace AstarteDeviceSDKCSharp.Transport.MQTT
{
    public class MutualSSLAuthenticationMqttConnectionInfo : IMqttConnectionInfo
    {

        private readonly Uri _brokerUrl;
        private readonly IMqttClientOptions _mqttConnectOptions;
        private readonly string _clientId = string.Empty;

        public MutualSSLAuthenticationMqttConnectionInfo(Uri brokerUrl, string astarteRealm,
        string deviceId, MqttClientOptionsBuilderTlsParameters tlsOptions)
        {
            _brokerUrl = brokerUrl;
            _mqttConnectOptions = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer(_brokerUrl.Host, _brokerUrl.Port)
            .WithTls(tlsOptions)
            .WithCleanSession(false)
            .WithCommunicationTimeout(TimeSpan.FromSeconds(60))
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
            .WithSessionExpiryInterval(0)
            .Build();

            _clientId = $"{astarteRealm}/{deviceId}";
        }

        public Uri GetBrokerUrl() => _brokerUrl;

        public string GetClientId() => _clientId;

        public IMqttClientOptions GetMqttConnectOptions() => _mqttConnectOptions;
    }
}
