using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class MagazineListHandlerEventArgs : System.EventArgs
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public int ElementIndex { get; set; }

        public MagazineListHandlerEventArgs()
        {
            CollectionName = default;
            ChangeType = default;
            ElementIndex = default;
        }

        public MagazineListHandlerEventArgs(string collectionName, string changeType, int elementIndex)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ElementIndex = elementIndex;
        }

        public override string ToString()
        {
            return $"{nameof(CollectionName)}: {CollectionName}, {nameof(ChangeType)}: {ChangeType}, {nameof(ElementIndex)}: {ElementIndex}";
        }


    }
}
