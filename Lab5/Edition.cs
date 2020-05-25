using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lab5
{
    [Serializable]
    [DataContract]
    public class Edition : IComparable, IComparer<Edition>
    {

        protected string _name;
        protected DateTime _releaseDate;
        protected int _copiesCount;

        public Edition(string name, DateTime releaseDate, int copiesCount)
        {
            _name = name;
            _releaseDate = releaseDate;
            _copiesCount = copiesCount;
        }

        public Edition()
        {
            _name = default;
            _releaseDate = default;
            _copiesCount = default;
        }

        [DataMember]
        public string Name { get => _name; set => _name = value; }
        [DataMember]
        public DateTime ReleaseDate { get => _releaseDate; set => _releaseDate = value; }
        [DataMember]
        public int CopiesCount
        {
            get => _copiesCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Incorrect copies count");
                }
                _copiesCount = value;
            }
        }

        public virtual object DeepCopy()
        {
            return new Edition(_name, new DateTime(_releaseDate.Ticks), _copiesCount);
        }



        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _releaseDate.GetHashCode();
                hashCode = (hashCode * 397) ^ _copiesCount;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(_name)}: {_name}, {nameof(_releaseDate)}: {_releaseDate}, {nameof(_copiesCount)}: {_copiesCount}";
        }

        public override bool Equals(object obj)
        {
            return obj is Edition edition &&
                   _name == edition._name &&
                   _releaseDate == edition._releaseDate &&
                   _copiesCount == edition._copiesCount;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Edition otherEdition = obj as Edition;

            if (otherEdition != null)
            {
                return string.Compare(_name, otherEdition._name, StringComparison.InvariantCulture);
            }
            else
            {
                throw new ArgumentException("Obj can't be null");
            }

        }

        int IComparer<Edition>.Compare(Edition x, Edition y)
        {
            return DateTime.Compare(x.ReleaseDate, y.ReleaseDate);
        }

        public static bool operator ==(Edition left, Edition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Edition left, Edition right)
        {
            return !Equals(left, right);
        }
    }
}
