using AgilityBubble.Logic;
using FluentAssertions;
using Xunit;

namespace AgilityBubble.DataAccess.Test
{
    public class DataAccessConfigurationTests : IClassFixture<DataAccessTestsUtils>
    {
        [Fact]
        public void Database_Can_be_Initialized_With_Test_Dictionary()
        {
            using (var session = SessionFactory.OpenSession())
            {
                long id = 1;
                var dictionary = session.Get<MultiDictionary>(id);
                dictionary.Code.Should().Be("First");
            }
        }
    }
}
