using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lab5
{
    [XmlInclude(typeof(Article))]
    [XmlInclude(typeof(Magazine))]
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
