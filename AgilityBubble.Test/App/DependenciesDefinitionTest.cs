using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgilityBubble.DataAccess;
using AgilityBubble.Logic;
using AgilityBubble.UI;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AgilityBubble.Test.App
{
    public class DependenciesDefinitionTest
    {
        private readonly ServiceCollection _services;

        public DependenciesDefinitionTest()
        {
            var configuration = new ConfigurationBuilder().Build();
            var startup = new Startup(configuration);
            _services = new ServiceCollection();
            startup.ConfigureServices(_services);

        }
        [Fact]
        public void MultiDictionaryViewModel_Resolves()
        {
            var provider = _services.BuildServiceProvider();
            var sut = provider.GetRequiredService<MultiDictionaryViewModel>();
            sut.Should().BeOfType<MultiDictionaryViewModel>();
        }
    }
}
