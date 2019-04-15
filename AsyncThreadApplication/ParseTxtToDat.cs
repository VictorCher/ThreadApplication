using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncThreadApplication
{
    class Operation
    {
        public float Result { get; set; }
        public float Number1 { get; set; }
        public float Number2 { get; set; }
        public string Operator { get; set; }
        public bool Fault { get; set; }

        public Operation(string line)
        {
            line += " ";
            bool noFault = false;
            int num = 0;
            string temp = null;
            int operation = 0;
            float number1 = 0;
            float number2 = 0;
            foreach (char c in line)
            {
                if (c == ' ' || c == '\n')
                {
                    if (num == 0) noFault = int.TryParse(temp, out operation);
                    else if (num == 1) float.TryParse(temp, out number1);
                    else float.TryParse(temp, out number2);
                    temp = null;
                    num++;
                }
                if (!noFault) operation = 0;
                temp += c;
            }
            if (operation == 1)
            {
                Result = number1 * number2;
                Operator = "*";
            }
            else if (operation == 2 && number2 != 0)
            {
                Result = number1 / number2;
                Operator = "/";
            }
            else Fault = true;
            Number1 = number1;
            Number2 = number2;
        }
    }

    class ParseTxtToDat
    {

        /// <summary>
        /// Читает файл txt
        /// </summary>
        public static List<Operation> Read(string path)
        {
            List<Operation> results = new List<Operation>();
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        Operation parse = new Operation(sr.ReadLine());
                        if (!parse.Fault) results.Add(parse);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Сохраняет в файл result.dat
        /// </summary>
        public static void Save(List<Operation> data)
        {
            if (data == null) return;
            using (StreamWriter sw = new StreamWriter("result.dat"))
            {
                foreach (var v in data)
                    sw.WriteLine($"{v.Number1,2} {v.Operator,2} {v.Number2,2}  =  {v.Result}");
            }
            data.Clear();
        }
    }
}
