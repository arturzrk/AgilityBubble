using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

public class AutoMockDataAttribute : AutoDataAttribute
{
    static Random random = new Random();
    public AutoMockDataAttribute() : base(() => BuildFixture())
    {
    }
    private static IFixture BuildFixture()
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true, GenerateDelegates = true });
        fixture.Register<int>(() => random.Next());
        return fixture;
    }
}