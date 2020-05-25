using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5
{
    class Listener
    {
        private List<ListEntry> _changes;

        public Listener()
        {
            _changes = new List<ListEntry>();
        }

        public void MagazineListHandler(object source, MagazineListHandlerEventArgs args)
        {
            _changes.Add(new ListEntry(args.CollectionName, args.ChangeType, args.ElementIndex));
        }

        public override string ToString()
        {
            return String.Join(",", _changes.Select(change => change.ToString()).ToArray());
        }
    }
}
