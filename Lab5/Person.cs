using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lab5
{
    [Serializable]
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }

        public Person()
        {
            Name = default;
            Surname = default;
            BirthDate = new DateTime();
        }
        public Person(String name, String surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
        public int BirthYear
        {
            get
            {
                return BirthDate.Year;
            }

            set
            {
                BirthDate = new DateTime(value, BirthDate.Month, BirthDate.Day);
            }
        }

        public override string ToString()
        {
            return "Name: " + Name + "; Surname: " + Surname + "; BirthDate: " + BirthDate.ToShortDateString();
        }

        public virtual string ToShortString()
        {
            return "Name: " + Name + "; Surname" + Surname;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Person))
                return false;

            return this.Name.Equals(((Person)obj).Name) && this.Surname.Equals(((Person)obj).Surname) &&
                this.BirthDate.Equals(((Person)obj).BirthDate);
        }

        public static bool operator ==(Person a, Person b)
        {
            if (a == null)
            {
                return b == null;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Person a, Person b)
        {
            if (a == null)
            {
                return b != null;
            }

            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            var hashCode = 442983081;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            hashCode = hashCode * -1521134295 + BirthYear.GetHashCode();
            return hashCode;
        }

        public Person DeepCopy()
        {
            return new Person(string.Copy(Name), string.Copy(Surname), new DateTime(BirthDate.Year, BirthDate.Month, BirthDate.Day));
        }
    }
}
