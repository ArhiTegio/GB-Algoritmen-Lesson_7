using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;
using System.Reflection;

namespace GB_Algoritmen_Lesson_7
{
    class Program
    {
        static Dictionary<string, Act> dict = new Dictionary<string, Act>
        {
            { "1", new AdjacencyMatrixView() },
        };

        static void Main(string[] args)
        {
            var ex = new Extension();
            var q = new Questions();
            var n = "";
            WriteLine("С# - Алгоритмы и структуры данных. Задание 7.");
            WriteLine("Кузнецов");
            var list = new HashSet<char>(dict.Select(x => x.Key[0]));
            while (n != "0")
            {
                WriteLine("Введите номер интересующей вас задачи:" + Environment.NewLine +
                    "1.	Нажмите для запуска программы." + Environment.NewLine +
                    "   Написать функции, которые считывают матрицу смежности из файла и выводят её на экран." + Environment.NewLine +
                    "   Написать рекурсивную функцию обхода графа в глубину." + Environment.NewLine +
                    "   Написать функцию обхода графа в ширину." + Environment.NewLine +
                    "   *Создать библиотеку функций для работы с графами." + Environment.NewLine +
                    "0. Нажмите для выхода из программы.");

                n = q.Question<int>("Введите ", list, true);
                if (n == "0") break;
                dict[n].Work();
            }

            ReadKey();
        }
    }

    abstract class Act
    {
        public abstract void Work();
    }
}
