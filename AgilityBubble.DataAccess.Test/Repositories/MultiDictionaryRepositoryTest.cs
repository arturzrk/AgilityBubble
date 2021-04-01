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
        public void Can_Create(MultiDictionary multiDictionary)
        {
            var sut = new MultiDictionaryRepository();
            sut.Save(multiDictionary);

            var result = sut.GetById(multiDictionary.Id);

            result.Should().Be(multiDictionary);

        }
    }
}
