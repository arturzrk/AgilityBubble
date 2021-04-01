using System.Collections.Generic;

namespace AgilityBubble.Logic
{
    public class MultiDictionaryLine: Entity
    {
        public string Code { get; protected set; }
        public string Description { get; protected set; }
        public int SortOrder { get; protected set; }
        public bool IsObsolete { get; protected set; }

        public virtual IDictionary<long, MultiDictionaryLine> Lines { get; protected set; } =
            new Dictionary<long, MultiDictionaryLine>();

        private MultiDictionaryLine()
        {
            
        }
        public MultiDictionaryLine(string code, string description, int sortOrder, bool isObsolete)
        {
            Code = code;
            Description = description;
            SortOrder = sortOrder;
            IsObsolete = isObsolete;
        }
    }
}