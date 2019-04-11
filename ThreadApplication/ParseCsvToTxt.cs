using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadApplication
{
    public class Events
    {
        public string Event { get; set; }
        public string TimeStamp { get; set; }
        public Events(string line)
        {
            bool flag = false;
            foreach(char c in line)
            {
                if (c == ';')
                {
                    if (!flag)
                    {
                        flag = true;
                        continue;
                    }
                    else return;
                }
                if (!flag) Event += c;
                else TimeStamp += c;
            }
        }
    }

    public class ParseCsvToTxt
    {
        static List<Events> events = new List<Events>();

        /// <summary>
        /// Читает файл CSV
        /// </summary>
        public static void Start()
        {

            events.Clear();
            StreamReader sr = new StreamReader("..\\..\\log.csv", Encoding.GetEncoding(1251));
            while (! sr.EndOfStream ) // Пока не конец потока (файла)
            {
                try
                {
                    events.Add(new Events(sr.ReadLine()));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            sr.Close();
        }

        /// <summary>
        /// Сохраняет в файл txt
        /// </summary>
        public static void Save()
        {
            while (MainWindow.thread1.IsAlive) ;
            if (events == null) return;
            using (StreamWriter sw = new StreamWriter("log.txt"))
            {
                foreach (var v in events)
                    sw.WriteLine($"Произошло событие: {v.Event,15}, Время: {v.TimeStamp}");
            }
            events.Clear();
        }
    }
}
