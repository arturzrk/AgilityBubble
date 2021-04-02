using System;
using System.Linq;
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
        public void Given_NoParent_When_InsertLine_Returns_MultiDictionary_WithLineInserted(MultiDictionary lineToInsert)
        {
            _sut.InsertLine(lineToInsert);

            _sut.Lines[lineToInsert.Code].Should().Be(lineToInsert);
        }

        [Theory]
        [AutoMockData]
        public void Given_CorrectParent_When_InsertLine_LineInsertedInTheParentCollection(MultiDictionary lineToInsert,
            MultiDictionary parentLine)
        {
            _sut.InsertLine(parentLine);
            _sut.InsertLine(lineToInsert, parentLine);

            _sut.Lines[parentLine.Code].Lines[lineToInsert.Code].Should().Be(lineToInsert);
        }

        [Theory]
        [AutoMockData]
        public void Given_ParentNotIn_When_InsertLine_InvalidParentExceptionIsThrown(MultiDictionary lineToInsert,
            MultiDictionary parentLine)
        {
            Action action = () =>_sut.InsertLine(lineToInsert,parentLine);
            action.Should().Throw<InvalidParentMultiDictionaryException>();
        }

        [Theory]
        [AutoMockData]
        public void Given_3Levels_When_InsertLine_LineProperlyInserted(MultiDictionary level0Line,
            MultiDictionary level1Line, MultiDictionary level2Line)
        {
            _sut.InsertLine(level0Line);
            _sut.InsertLine(level1Line, level0Line);
            _sut.InsertLine(level2Line, level1Line);

            var result = _sut.Lines[level0Line.Code].Lines[level1Line.Code].Lines[level2Line.Code];
            result.Should().Be(level2Line);
        }

        [Fact]
        public void Trying_to_insert_null_line_throws()
        {
            Action action = () => _sut.InsertLine(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
