using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lab5
{
    class TestCollection
    {
        private readonly List<Edition> _editionList;
        private readonly List<string> _stringList;
        private readonly Dictionary<Edition, Magazine> _editionDictionary;
        private readonly Dictionary<string, Magazine> _stringDictionary;
        public static Magazine GenerateMagazine(int elementsCount)
        {
            return new Magazine(default, default, default, default)
            {
                Editors = new List<Person>(elementsCount),
                Articles = new List<Article>(elementsCount)
            };
        }

        public TestCollection(int value)
        {
            _editionList = new List<Edition>(value);
            _stringList = new List<string>(value);
            _editionDictionary = new Dictionary<Edition, Magazine>(value);
            _stringDictionary = new Dictionary<string, Magazine>(value);

            for (int i = 0; i < value; i++)
            {
                _editionList.Add(new Edition((value + i).ToString(), DateTime.Now, i));
                _stringList.Add((value + i).ToString());
                _editionDictionary.Add(_editionList[i], GenerateMagazine(value));
                _stringDictionary.Add(_stringList[i], GenerateMagazine(value));
            }
        }


        private void MeasureTimeList(IList collection, int index)
        {
            Stopwatch time = Stopwatch.StartNew();
            object ob = collection[index];
            time.Stop();
            Console.WriteLine($"Element with index {index}: time {time.Elapsed}");
        }

        private void MeasureTimeDictionary(IDictionary dictionary, object key, int index)
        {
            Stopwatch time = Stopwatch.StartNew();
            object ob = dictionary[key];
            time.Stop();
            Console.WriteLine($"Element with key index {index}: time {time.Elapsed}");

        }
        public void MeasureTime()
        {
            Console.WriteLine("Edition list time:");
            MeasureTimeList(_editionList, _editionList.Count / 2);
            MeasureTimeList(_editionList, 0);
            MeasureTimeList(_editionList, _editionList.Count - 1);

            Console.WriteLine("\nString list time: ");
            MeasureTimeList(_stringList, _stringList.Count / 2);
            MeasureTimeList(_stringList, 0);
            MeasureTimeList(_stringList, _stringList.Count - 1);

            Console.WriteLine("\nDictionary with key string time:");
            MeasureTimeDictionary(_stringDictionary, _stringDictionary.ElementAt(_stringDictionary.Count / 2).Key, _stringDictionary.Count / 2);
            MeasureTimeDictionary(_stringDictionary, _stringDictionary.ElementAt(_stringDictionary.Count - 1).Key, _stringDictionary.Count - 1);
            MeasureTimeDictionary(_stringDictionary, _stringDictionary.ElementAt(0).Key, 0);

            Console.WriteLine("\nDictionary with key Edition");
            MeasureTimeDictionary(_editionDictionary, _editionDictionary.ElementAt(_editionDictionary.Count / 2).Key, _editionDictionary.Count / 2);
            MeasureTimeDictionary(_editionDictionary, _editionDictionary.ElementAt(_editionDictionary.Count - 1).Key, _editionDictionary.Count - 1);
            MeasureTimeDictionary(_editionDictionary, _editionDictionary.ElementAt(0).Key, 0);
        }
    }
}
