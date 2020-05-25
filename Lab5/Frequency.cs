using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lab5
{
    [Serializable]
    [DataContract]
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }
}
