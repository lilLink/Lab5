using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5
{
    class MagazineCollection
    {
        private List<Magazine> _magazines;

        public delegate void MagazineListHandler(object source, MagazineListHandlerEventArgs args);

        public string CollectionName { get; set; }

        public event MagazineListHandler MagazineAdded;
        public event MagazineListHandler MagazineReplaced;
        public MagazineCollection()
        {
            _magazines = new List<Magazine>();
        }
        public void AddDefaults()
        {
            _magazines.Add(new Magazine("New-York Times", Frequency.Weekly, new DateTime(2009, 12, 11), 25000));
            MagazineAdded?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName, "AddedElement", 0));
            _magazines.Add(new Magazine("Daily Bugles", Frequency.Monthly, new DateTime(2012, 1, 1), 200000));
            MagazineAdded?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName, "AddedElement", 1));

        }

        public void AddMagazines(params Magazine[] magazines)
        {
            foreach (Magazine magazine in magazines)
            {
                MagazineAdded?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName,
                    "AddedElement", _magazines.Count));
                _magazines.Add(magazine);
            }
        }

        public bool Replace(int j, Magazine magazine)
        {
            if (_magazines.Count <= j) return false;

            _magazines[j] = magazine;
            MagazineReplaced?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName, "MagazineReplaced", j));
            return true;
        }

        public Magazine this[int index]
        {
            get => _magazines[index];
            set
            {
                _magazines[index] = value;
                MagazineReplaced?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName, "MagazineReplaced", index));
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(_magazines)}: {string.Join(", ", (from Magazine magazine in _magazines select magazine.ToString()).ToArray())}";
        }

        public virtual string ToShortString()
        {
            return
                $"{nameof(_magazines)}: {string.Join(", ", (from Magazine magazine in _magazines select magazine.ToShortString()).ToArray())}";
        }

        public void SortByName()
        {
            _magazines.Sort();
        }

        public void SortByDate()
        {
            _magazines.Sort(new Edition());

        }

        public void SortByCopiesCount()
        {
            _magazines.Sort(new EditionByCopiesCountComparer());
        }

        public double MaxAvgRate
        {
            get
            {
                if (_magazines.Count == 0) return default;

                return (from Magazine magazine in _magazines select magazine.Rating).Max();
            }
        }

        public IEnumerable<Magazine> MonthlyMagazines => (from Magazine magazine in _magazines select magazine)
            .Where(magazine => magazine.Frequency == Frequency.Monthly);

        public List<Magazine> RatingGroup(double value)
        {
            var ratingQry = from Magazine magazine in _magazines
                            where magazine.Rating >= value
                            group magazine by magazine.Rating;

            return ratingQry.SelectMany(group => group).ToList();
        }
    }
}
