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
            var mockedLine = fixture.Create<MultiDictionary>();

            mockedLine.Code.Should().NotBeNullOrEmpty();
        }
    }
}
