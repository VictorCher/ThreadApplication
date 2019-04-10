// Чернышов Виктор. Урок 5
/* Задание:
 * Написать приложение, считающее в раздельных потоках:
 * a. факториал числа N, которое вводится с клавиатуры;
 * b. сумму целых чисел до N. */

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
        /// <summary>
        /// Вычисляет факториал
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int Factorial(int n)
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }

        void UpdateFactRes(object n)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            { textBox1.Text = Factorial((int)n).ToString(); });
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
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int n;
            if (int.TryParse(input1.Text, out n))
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
