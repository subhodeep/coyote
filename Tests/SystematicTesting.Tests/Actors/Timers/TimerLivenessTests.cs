﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Microsoft.Coyote.Actors;
using Microsoft.Coyote.Actors.Timers;
using Microsoft.Coyote.Specifications;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Coyote.SystematicTesting.Tests.Actors
{
    public class TimerLivenessTests : BaseSystematicTest
    {
        public TimerLivenessTests(ITestOutputHelper output)
            : base(output)
        {
        }

        private class TimeoutReceivedEvent : Event
        {
        }

        private class Client : StateMachine
        {
            [Start]
            [OnEntry(nameof(Initialize))]
            [OnEventDoAction(typeof(TimerElapsedEvent), nameof(HandleTimeout))]
            private class Init : State
            {
            }

            private void Initialize()
            {
                this.StartTimer(TimeSpan.FromMilliseconds(10));
            }

            private void HandleTimeout()
            {
                this.Monitor<LivenessMonitor>(new TimeoutReceivedEvent());
            }
        }

        private class LivenessMonitor : Monitor
        {
            [Start]
            [Hot]
            [OnEventGotoState(typeof(TimeoutReceivedEvent), typeof(TimeoutReceived))]
            private class NoTimeoutReceived : State
            {
            }

            [Cold]
            private class TimeoutReceived : State
            {
            }
        }

        [Fact(Timeout = 5000)]
        public void TestTimerLiveness()
        {
            var configuration = GetConfiguration();
            configuration.TimeoutDelay = 1;
            configuration.LivenessTemperatureThreshold = 150;
            configuration.MaxSchedulingSteps = 300;
            configuration.TestingIterations = 1000;

            this.Test(r =>
            {
                r.RegisterMonitor<LivenessMonitor>();
                r.CreateActor(typeof(Client));
            },
            configuration: configuration);
        }
    }
}
