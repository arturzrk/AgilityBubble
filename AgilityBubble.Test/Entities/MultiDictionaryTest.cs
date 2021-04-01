using System;
using AgilityBubble.Logic.Entities.Exceptions;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace AgilityBubble.Test
{
    using Logic;
    using Xunit;

    public class MultiDictionaryTest
    {
        private readonly MultiDictionary _sut;

        public MultiDictionaryTest()
        {
            var fixture = new Fixture();
            var code = fixture.Create<string>();
            var description = fixture.Create<string>();
            var isSystem = fixture.Create<bool>();
            _sut = new MultiDictionary(code,description,isSystem);
        }

        [Theory]
        [AutoMockData]
        public void Given_NoParent_When_InsertLine_Returns_MultiDictionary_WithLineInserted(MultiDictionaryLine lineToInsert)
        {
            _sut.InsertLine(lineToInsert);
            var result = _sut.GetLine(lineToInsert.Id);

            result.Should().Be(lineToInsert);
        }

        [Theory]
        [AutoMockData]
        public void Given_CorrectParent_When_InsertLine_LineInsertedInTheParentCollection(MultiDictionaryLine lineToInsert,
            MultiDictionaryLine parentLine)
        {
            _sut.InsertLine(parentLine);
            _sut.InsertLine(lineToInsert, parentLine);

            var result = _sut.GetLine(parentLine.Id);

            result.Should().Be(parentLine);
            parentLine.Lines.Count.Should().Be(1);
            parentLine.Lines[lineToInsert.Id].Should().Be(lineToInsert);
        }

        [Theory]
        [AutoMockData]
        public void Given_ParentNotIn_When_InsertLine_InvalidParentExceptionIsThrown(MultiDictionaryLine lineToInsert,
            MultiDictionaryLine parentLine)
        {
            Action action = () =>_sut.InsertLine(lineToInsert,parentLine);
            action.Should().Throw<InvalidParentMultiDictionaryException>();
        }

        [Fact]
        public void Trying_to_insert_null_line_throws()
        {
            Action action = () => _sut.InsertLine(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
