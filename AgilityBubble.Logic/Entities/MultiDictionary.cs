using System;
using System.Collections.Generic;
using AgilityBubble.Logic.Entities.Exceptions;

namespace AgilityBubble.Logic
{
    public class MultiDictionary: AggregateRoot
    {
        public virtual string Code { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual bool IsSystem { get; protected set; }
        public virtual IDictionary<long, MultiDictionaryLine> Lines { get; protected set; }

        protected MultiDictionary()
        {
            
        }
        public MultiDictionary(string code, string description, bool isSystem) : this()
        {
            Code = code;
            Description = description;
            IsSystem = isSystem;
            Lines = new Dictionary<long, MultiDictionaryLine>();
        }

        public virtual void InsertLine(MultiDictionaryLine lineToInsert, MultiDictionaryLine parentLine = null)
        {
            if (lineToInsert == null)
                throw new ArgumentNullException();
            if(parentLine == null)
            {
                Lines.Add(lineToInsert.Id, lineToInsert);
            }
            else
            {
                if (!Lines.ContainsKey(parentLine.Id))
                    throw new InvalidParentMultiDictionaryException();
                Lines[parentLine.Id].Lines.Add(lineToInsert.Id, lineToInsert);
            }
        }

        public virtual MultiDictionaryLine GetLine(long lineId)
        {
            return Lines[lineId];
        }

        public virtual void ChangeCodeTo(string newCode)
        {
            Code = newCode;
        }
    }
}
