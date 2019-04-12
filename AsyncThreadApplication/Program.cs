// Чернышов Виктор. Урок 6

/* Задание 1:
 * Даны две двумерных матрицы размерностью 100х100 каждая.
 * Написать приложение, производящее их параллельное умножение.
 * Матрицы заполняются случайными целыми числами от 0 до 10. */

/* Задание 2:
 * В директории лежат файлы. По структуре они содержат три числа,
 * разделенные пробелами. Первое число — целое, обозначает действие: 
 * 1 — умножение и 2 — деление. Остальные два — числа с плавающей точкой.
 * Написать многопоточное приложение, выполняющее эти действия над числами
 * и сохраняющее результат в файл result.dat . Файлов в директории
 * заведомо много. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncThreadApplication
{
    class Program
    {
        static int size = 100;
        static int[,] Create()
        {
            Random rnd = new Random();
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rnd.Next(10);
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
            return matrix;
        }
        static int[,] Multiplication(int[,] a, int[,] b)
        {
            int[,] r = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        static void Print(int[,] a)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            #region Задание 1
            Console.WriteLine("Первая матрица:");
            int[,] A = Create();
            Console.WriteLine("Вторая матрица:");
            int[,] B = Create();
            Console.WriteLine("Результирующая матрица:");
            int[,] C = Multiplication(A,B);
            Print(C);
            #endregion

            #region Задание 2
            List<string> path = new List<string>()
            {
                "..\\..\\data1.txt",
                "..\\..\\data2.txt",
                "..\\..\\data3.txt",
            };
            List<Operation> results = new List<Operation>();
            bool add = false;
            foreach (string file in path)
            {
                results= ParseTxtToDat.Read(file);
                ParseTxtToDat.Save(results, add);
                add = true;
            }
            #endregion
            Console.WriteLine("Конец программы");
            Console.ReadKey();
        }
    }
}
