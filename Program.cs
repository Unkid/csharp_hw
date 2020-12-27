using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace HelloWorld
{
    public class Composer
    {
        public Composer() { }
        public Composer(string first_name, string second_name, string nickname = null)
        {
            First_name = first_name;
            Second_name = second_name;
            Nickname ??= nickname;
        }
        public string First_name;
        public string Second_name;
        // Ну куда же без сценического имени? Может быть не обязательным.
        public string Nickname;
    }
    public class Song
    {
        public Song() { }
        public Song(string name, int year, List<string> genres, List<Composer> composers = null)
        {
            Name = name;
            Year = year;
            Genres = genres;
            Composers = composers ?? new List<Composer>();
        }
        public string Name;
        public int Year;
        public List<string> Genres = new List<string>();
        public List<Composer> Composers = new List<Composer>();

    }

    public class Album
    {
        public Album() { }
        public Album(string name, List<Song> songs)
        {
            Name = name;
            Songs = songs;
        }

        public void Save(string name)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Album));
            XmlWriterSettings s = new XmlWriterSettings()
            {
                Indent = true
            };
            using (XmlWriter wrt = XmlWriter.Create(name, s))
            {
                ser.Serialize(wrt, this);
            }
        }
        public string Name;
        public List<Song> Songs = new List<Song>();
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Создание 2 композиторов
            List<Composer> Composers = new List<Composer>();
            Composers.Add(new Composer("Sasha", "Unkudinova", "Lil_Unkid"));
            Composers.Add(new Composer("Grigory", "Izumov", "MC_Gri"));
            // Создание жанров
            List<string> Genres = new List<string>();
            Genres.Add("Hip-hop");
            Genres.Add("Rap");
            // Создание песен
            List<Song> Songs = new List<Song>();
            Songs.Add(new Song("Love outline", 2020, Genres, Composers));
            Songs.Add(new Song("Moscow", 2019, Genres, Composers));
            // Создание Альбома
            Album Laokin = new Album("Laokin", Songs);
            // Тест сериализации
            Laokin.Save("Laokin.xml");
        }
    }
}
