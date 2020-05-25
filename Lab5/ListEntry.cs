using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class ListEntry
    {
        public string CollectionName { get; set; }
        public string EventName { get; set; }
        public int ElementIndex { get; set; }

        public ListEntry()
        {
            CollectionName = default;
            EventName = default;
            ElementIndex = default;
        }

        public ListEntry(string collectionName, string eventName, int elementIndex)
        {
            CollectionName = collectionName;
            EventName = eventName;
            ElementIndex = elementIndex;
        }

        public override string ToString()
        {
            return $"{nameof(CollectionName)}: {CollectionName}, {nameof(EventName)}: {EventName}, {nameof(ElementIndex)}: {ElementIndex}";
        }
    }
}
