using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Lab5
{
    [Serializable]
    [DataContract]
    public class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency _frequency;
        private List<Article> _articles;
        private List<Person> _editors;


        public Magazine(string magazineName, Frequency frequency, DateTime releaseDate, int copiesCount)
        {
            _name = magazineName;
            _frequency = frequency;
            _releaseDate = releaseDate;
            _copiesCount = copiesCount;
            _articles = new List<Article>();
            _editors = new List<Person>();
        }

        public Magazine()
        {
            _name = default;
            _frequency = default;
            _releaseDate = default;
            _copiesCount = default;
            _articles = default;
            _editors = default;
        }
        [DataMember]
        public Frequency Frequency
        {
            get => _frequency;
            set => _frequency = value;
        }


        public double Rating
        {
            get
            {
                if (_articles.Count == 0) return 0;

                return (from Article article in _articles
                        select article.Rating)
                    .Average();
            }
        }
        public bool this[Frequency frequency] => frequency == this._frequency;

        [DataMember]
        public List<Article> Articles
        {
            get => _articles;
            set => _articles = value;
        }

        [DataMember]
        public List<Person> Editors
        {
            get => _editors;
            set => _editors = value;
        }

        public Edition Edition
        {
            get => this;
            set
            {
                _name = value.Name;
                _releaseDate = value.ReleaseDate;
                _copiesCount = value.CopiesCount;
            }
        }

        public IEnumerable ArticlesMoreThan(double rate)
        {
            return (from Article article in _articles select article)
                .Where(article => article != null && article.Rating > rate);
        }

        public IEnumerable ArticlesWithName(string name)
        {
            return (from Article article in _articles select article)
                .Where(article => article.ArticleName.Contains(name));
        }

        public void AddArticles(params Article[] articles)
        {
            foreach (Article article in articles)
            {
                _articles.Add(article);
            }
        }

        public void AddEditors(params Person[] editors)
        {
            foreach (Person editor in editors)
            {
                _editors.Add(editor);
            }
        }

        public override string ToString()
        {
            return "MagazineName: " + _name + "; ReleaseDate: " +
                _releaseDate.ToShortDateString() + "; CopiesCount: " + _copiesCount.ToString() + "; Articles: { " +
                string.Join(", ", (from Article article in _articles select article.ToString()).ToArray()) + " }; Editors: { " +
                string.Join(", ", (from Person editor in _editors select editor.ToString()).ToArray()) + " };";
        }

        public virtual string ToShortString()
        {
            return "MagazineName: " + _name + "; Frequency: " + _frequency.ToString() + "; ReleaseDate: " +
                _releaseDate.ToShortDateString() + "; CopiesCount: " + _copiesCount.ToString() + "; AvgRate: " + Rating;
        }




        public new Magazine DeepCopy()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return formatter.Deserialize(stream) as Magazine;
            }
        }

        public bool Save(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Append))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(GetType());
                    serializer.WriteObject(fs, this);
                    fs.Close();
                    StreamWriter writer = File.AppendText(filename);
                    writer.WriteLine("     ");
                    writer.Close();
                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool Load(string filename, Magazine magazine)
        {
            return magazine.Load(filename);
        }

        public static bool Save(string filename, Magazine magazine)
        {
            return magazine.Save(filename);
        }

        public bool Load(string filename)
        {
            try
            {



                DataContractJsonSerializer serializer = new DataContractJsonSerializer(GetType());
                string str = File.ReadAllLines(filename)[0];

                using (MemoryStream fs = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    Magazine newMagazine = serializer.ReadObject(fs) as Magazine;
                    foreach (var property in GetType().GetProperties())
                    {
                        if (property.CanWrite)
                        {
                            property.SetValue(this, property.GetValue(newMagazine));
                        }
                    }

                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Введіть дані в наступному форматі: Назва статті, Прізвище автора, Ім'я автора, " +
                              "Дата народження автора в форматі dd/mm/yy, Рейтинг статті");
            Console.WriteLine("В якості розділювачів можете використовувати: , # -");

            try
            {
                string[] data = Console.ReadLine().Split(new char[] { '-', '#' });
                Article article = new Article();
                article.ArticleName = data[0];
                article.Author = new Person(data[2], data[1], DateTime.Parse(data[3]));
                article.ArticleRate = float.Parse(data[4]);
                _articles.Add(article);
                return true;
            }
            catch
            {
                Console.WriteLine("Can not parse data");
                return false;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(_articles);
        }

        public IEnumerator AuthorsWhichAreEditors()
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (_editors.Contains(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

        public IEnumerator AuthorsWhoAreNotEditors()
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!_editors.Contains(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

        public void Add(Object ob)
        {
            if (ob is Magazine)
            {
                _articles.Add(ob as Article);
            }
        }
    }
}
