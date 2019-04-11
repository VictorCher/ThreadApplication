// Чернышов Виктор. Урок 5

/* Задание 1:
 * Написать приложение, считающее в раздельных потоках:
 * a. факториал числа N, которое вводится с клавиатуры;
 * b. сумму целых чисел до N. */

/* Задание 2:
 * Написать приложение, выполняющее парсинг CSV-файла произвольной структуры и
 * сохраняющего его в обычный txt-файл. Все операции проходят в потоках. CSV-файл заведомо
 * имеет большой объем. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace ThreadApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Thread thread1, thread2;
        /// <summary>
        /// Вычисляет факториал
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static double Factorial(double n)
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }

        void UpdateFactRes(object n)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            { textBox1.Text = Factorial((double)n).ToString(); });
        }

        /// <summary>
        /// Вычисляет сумму
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int Sum(int n)
        {
            int res = 0;
            for (int i = 0; i <= n; i++) res += i;
            return res;
        }

        void UpdateSumRes(object n)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            { textBox2.Text = Sum((int)n).ToString(); });
        }

        public MainWindow()
        {
            InitializeComponent();
            thread1 = new Thread(new ThreadStart(ParseCsvToTxt.Start));
            thread2 = new Thread(new ThreadStart(ParseCsvToTxt.Save));
            thread1.Name = "Thread1";
            thread1.Start();
            thread2.Name = "Thread2";
            thread2.Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double n;
            if (double.TryParse(input1.Text, out n))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UpdateFactRes));
                thread.Name = "Вторичный поток 1";
                thread.Start(n);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int n;
            if (int.TryParse(input2.Text, out n))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UpdateSumRes));
                thread.Name = "Вторичный поток 2";
                thread.Start(n);
            }
        }
    }
}
