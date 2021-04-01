using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgilityBubble.Logic;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AgilityBubble.Test
{
    public class AutoMockTests
    {
        [Fact]
        public void AutoFixtureCreatesComplexTypesWithProperties()
        {
            var fixture = new Fixture();
            var mockedLine = fixture.Create<MultiDictionaryLine>();

            mockedLine.Code.Should().NotBeNullOrEmpty();
        }
    }
}
