using AgilityBubble.Logic;
using FluentNHibernate.Mapping;

namespace AgilityBubble.DataAccess
{
    public class MultiDictionaryMap : ClassMap<MultiDictionary>
    {
        public MultiDictionaryMap()
        {
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.Description);
            Map(x => x.IsSystem);
            HasMany(x => x.Lines).KeyColumn("ParentMultiDictionaryID")
                .Cascade.All().AsMap(x => x.Code).Not.LazyLoad().Fetch.Join();
        }
    }
}
