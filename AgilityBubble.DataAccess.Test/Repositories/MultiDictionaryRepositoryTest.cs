using AgilityBubble.DataAccess.Repositories;
using AgilityBubble.Logic;
using FluentAssertions;
using Xunit;

namespace AgilityBubble.DataAccess.Test.Repositories
{
    public class MultiDictionaryRepositoryTest : IClassFixture<DataAccessTestsUtils>
    {
        [Theory]
        [AutoMockData]
        public void Can_Create(MultiDictionary multiDictionary, MultiDictionary child1Dictionary, MultiDictionary child2Dictionary)
        {
            var sut = new MultiDictionaryRepository();
            multiDictionary.InsertLine(child1Dictionary);
            multiDictionary.InsertLine(child2Dictionary);
            sut.Save(multiDictionary);

            var result = sut.GetById(multiDictionary.Id);

            result.Should().Be(multiDictionary);
            result.Lines[child1Dictionary.Code].Should().Be(child1Dictionary);
            result.Lines[child2Dictionary.Code].Should().Be(child2Dictionary);

        }
    }
}
