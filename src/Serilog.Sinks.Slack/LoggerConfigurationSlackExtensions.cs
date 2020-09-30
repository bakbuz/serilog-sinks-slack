﻿using System;
using Serilog.Configuration;
using Serilog.Events;
// Copyright 2014 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Sinks.Slack
{
    /// <summary>
    /// Adds the WriteTo.MSSqlServer() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationSlackExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to a channel in Slack.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="webhookUri">WebHook Uri that allows Slack Incoming Webhooks (https://api.slack.com/incoming-webhooks).</param>
        /// <param name="renderMessageImplementation">Optional delegate to build json to send to slack webhook.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">FormatProvider to apply in <see cref="LogEvent.RenderMessage(IFormatProvider)"/>. It overrides default behaviour.</param>
        /// <param name="username">Username or Botname.</param>
        /// <param name="iconEmoji">Icon emoji.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerConfiguration,
            string webhookUri,
            SlackSink.RenderMessageMethod renderMessageImplementation = null,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            string username = null,
            string iconEmoji = null
        )
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(nameof(loggerConfiguration));

            if (string.IsNullOrWhiteSpace(webhookUri))
                throw new ArgumentNullException(nameof(webhookUri));

            return loggerConfiguration.Sink(
                new SlackSink(
                    webhookUri,
                    renderMessageImplementation,
                    formatProvider,
                    username,
                    iconEmoji
                ),
                restrictedToMinimumLevel);
        }
    }
}
