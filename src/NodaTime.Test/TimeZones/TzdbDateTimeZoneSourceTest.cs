﻿#region Copyright and license information
// Copyright 2001-2009 Stephen Colebourne
// Copyright 2009-2012 Jon Skeet
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
#endregion

using System;
using System.Linq;
using NUnit.Framework;
using NodaTime.TimeZones;

namespace NodaTime.Test.TimeZones
{
    [TestFixture]
    public class TzdbDateTimeZoneSourceTest
    {
        [Test]
        public void ZoneMapping()
        {
            // We don't know what the mapping will be like on Mono. (We could be on TZDB-based system time zones, for example.)
            if (TestHelper.IsRunningOnMono)
            {
                return;
            }
            var source = new TzdbDateTimeZoneSource("NodaTime.TimeZones.Tzdb");
            var bclZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            Assert.AreEqual("Europe/London", source.MapTimeZoneId(bclZone));
        }

        /// <summary>
        /// Simply tests that every ID in the built-in database can be fetched. This is also
        /// helpful for diagnostic debugging when we want to check that some potential
        /// invariant holds for all time zones...
        /// </summary>
        [Test]
        public void ForId_AllIds()
        {
            var source = new TzdbDateTimeZoneSource("NodaTime.TimeZones.Tzdb");
            foreach (string id in source.GetIds())
            {
                Assert.IsNotNull(source.ForId(id));
            }
        }

        [Test]
        public void UtcEqualsBuiltIn()
        {
            var zone = new TzdbDateTimeZoneSource("NodaTime.TimeZones.Tzdb").ForId("UTC");
            Assert.AreEqual(DateTimeZone.Utc, zone);
        }
    }
}