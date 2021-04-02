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
        public virtual IDictionary<string, MultiDictionary> Lines { get; protected set; }

        protected MultiDictionary()
        {
            
        }
        public MultiDictionary(string code, string description, bool isSystem) : this()
        {
            Code = code;
            Description = description;
            IsSystem = isSystem;
            Lines = new Dictionary<string, MultiDictionary>();
        }

        public virtual void InsertLine(MultiDictionary lineToInsert, MultiDictionary parentLine = null)
        {
            if (lineToInsert == null)
                throw new ArgumentNullException();
            if(parentLine == null)
            {
                Lines.Add(lineToInsert.Code, lineToInsert);
            }
            else
            {
                var parent = FindByCode(parentLine.Code);
                if (parent == null)
                    throw new InvalidParentMultiDictionaryException();
                parent.InsertLine(lineToInsert);
            }
        }

        public virtual MultiDictionary FindByCode(string code)
        {
            if (Lines.ContainsKey(code))
                return Lines[code];
            foreach (var dictionary in Lines.Values)
            {
                var result = dictionary.FindByCode(code);
                if (result != null)
                    return result;
            }
            return null;
        }

        public virtual void ChangeCodeTo(string newCode)
        {
            Code = newCode;
        }
    }
}
