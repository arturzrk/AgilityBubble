using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityBubble.Logic
{
    public class Dictionary : Entity
    {
        public string Code { get; protected set; }
        public string Description { get; protected set; }
        public bool IsSystem { get; protected set; }

        private Dictionary()
        {
        }

        public Dictionary(string code, string description, bool isSystem): this()
        {
            Code = code;
            Description = description;
            IsSystem = isSystem;
        }
    }
}
