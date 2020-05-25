using System;
using System.IO;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Magazine magazine = new Magazine("Daily Buglees", Frequency.Monthly, new DateTime(2010, 12, 12), 250000);

            magazine.AddArticles(new Article(new Person("Nick", "Back", new DateTime(1990, 10, 20)),
                "Corona-Time", 2.2));
            magazine.AddEditors(new Person("Kek", "lol", new DateTime(1988, 8, 7)));
            magazine.Save("file.txt");
            Console.WriteLine("Оригінал: ");
            Console.WriteLine(magazine);
            Magazine newMagazine = magazine.DeepCopy();
            Console.WriteLine("Копія");
            Console.WriteLine(newMagazine.ToString());
            Console.WriteLine("Введіть адресу файлу звідки потрібно зчитати:");
            string path = Console.ReadLine();
            if (!File.Exists(path))
            {
                File.Create(path);
                Console.WriteLine("Файл було створено, будь ласка заповність його.");

                return;
            }

            if (!magazine.Load(path))
            {
                return;
            }

            Console.WriteLine(magazine);
            magazine.AddFromConsole();
            magazine.Save(path);
            Console.WriteLine(magazine);
            Console.WriteLine("Робота статичних методів серіалізації/десеріалізації");
            Magazine.Load(path, magazine);
            magazine.AddFromConsole();
            Magazine.Save(path, magazine);
            Console.WriteLine(magazine);


            Console.WriteLine(newMagazine.ToString());
        }
    }
}
