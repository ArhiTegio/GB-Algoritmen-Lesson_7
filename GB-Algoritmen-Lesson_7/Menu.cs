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
    class AdjacencyMatrixView : Act
    {
        public override void Work()
        {
            var matrix = new AdjacencyMatrix(new Dictionary<string, List<Direction>>()
            {
                { "A", new List<Direction>() { new Direction("D", 2), new Direction("B", 1) } },
                { "B", new List<Direction>() { new Direction("A", 1), new Direction("C", 4), new Direction("E", 5) } },
                { "C", new List<Direction>() { new Direction("B", 4), new Direction("D", 3) } },
                { "D", new List<Direction>() { new Direction("A", 2), new Direction("E", 2), new Direction("C", 3) } },
                { "E", new List<Direction>() { new Direction("D", 2), new Direction("B", 5), } },
                { "G", new List<Direction>() { new Direction("I", 7), } },
                { "H", new List<Direction>() { new Direction("I", 8) } },
                { "I", new List<Direction>() { new Direction("G", 7), new Direction("H", 8) } },
            });


            WriteLine("Заполнение и вывод на экран:");
            matrix.Print();
            matrix.Save("1.ml");
            var matrix2 = new AdjacencyMatrix(new Dictionary<string, List<Direction>>());
            matrix2.Load("1.ml");


            WriteLine("Запись и загрузка матрицы:");
            matrix2.Print();
            WriteLine("Алгоритм в ширину:");
            WriteLine(BypassWidth(matrix2));
            WriteLine("Алгоритм в высоту:");
            WriteLine(BypassHeight(matrix2));

            WriteLine("Алгоритм Дейкстры:");
            matrix = new AdjacencyMatrix(new Dictionary<string, List<Direction>>()
            {
                { "X0", new List<Direction>() { new Direction("X1", 4), new Direction("X2", 8), new Direction("X3", 3) } },
                { "X1", new List<Direction>() { new Direction("X0", 4), new Direction("X2", 1), new Direction("X5", 5) } },
                { "X2", new List<Direction>() { new Direction("X0", 8), new Direction("X1", 1), new Direction("X3", 8), new Direction("X4", 2) } },
                { "X3", new List<Direction>() { new Direction("X0", 3), new Direction("X2", 8), new Direction("X6", 4) } },
                { "X4", new List<Direction>() { new Direction("X1", 8), new Direction("X2", 2), new Direction("X5", 2), new Direction("X7", 5) } },
                { "X5", new List<Direction>() { new Direction("X1", 5), new Direction("X4", 2), new Direction("X7", 3) } },
                { "X6", new List<Direction>() { new Direction("X3", 4), new Direction("X7", 2) } },
                { "X7", new List<Direction>() { new Direction("X4", 5), new Direction("X5", 3), new Direction("X6", 2) } },
            });
            matrix.Print();
            WriteLine("Согласно алгоритму Дейкстры кротчайший путь:");
            WriteLine(DijkstrasAlgorithm(matrix, "X6", "X1"));
        }

        public string BypassWidth(AdjacencyMatrix matrix)
        {
            var hash = new HashSet<string>();
            var queue = new Queue<string>();
            var field = new Dictionary<string, int>();

            var n = 1;
            queue.Enqueue(matrix.Name.First().Key);
            hash.Add(matrix.Name.First().Key);
            field.Add(matrix.Name.First().Key, n);
            var checkField = "";
            while(field.Count != matrix.Name.Count)
            {
                if (queue.Count != 0)
                {
                    checkField = queue.Dequeue();

                    for (int i = 0; i < matrix.Matrix[checkField].Length; ++i)
                        if (!hash.Contains(matrix.Pos[i]))
                            if (matrix.Matrix[checkField][i] != 0)
                            {
                                field.Add(matrix.Pos[i], n);
                                queue.Enqueue(matrix.Pos[i]);
                                hash.Add(matrix.Pos[i]);
                            }
                    checkField = "";
                }

                if (field.Count != matrix.Name.Count && queue.Count == 0)
                {
                    n++;
                    for (int i = 0; i < matrix.Name.Count; ++i)
                        if (!hash.Contains(matrix.Pos[i]))
                        {

                            field.Add(matrix.Pos[i], n);
                            queue.Enqueue(matrix.Pos[i]);
                            hash.Add(matrix.Pos[i]);
                            break;
                        }
                }
            };

            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            foreach (var e in field)
            {
                sb.Append(e.Key);
                sb2.Append(e.Value);
            }

            return sb.Append(Environment.NewLine).Append(sb2).ToString();
        }

        public string BypassHeight(AdjacencyMatrix matrix)
        {
            var hash = new HashSet<string>();
            var stack = new Stack<string>();
            var field = new Dictionary<string, int>();

            var n = 1;
            stack.Push(matrix.Name.First().Key);
            hash.Add(matrix.Name.First().Key);
            field.Add(matrix.Name.First().Key, n);
            var checkField = "";
            while (field.Count != matrix.Name.Count)
            {
                if (stack.Count != 0)
                {
                    checkField = stack.Pop();

                    for (int i = 0; i < matrix.Matrix[checkField].Length; ++i)
                        if (!hash.Contains(matrix.Pos[i]))
                            if (matrix.Matrix[checkField][i] != 0)
                            {
                                field.Add(matrix.Pos[i], n);
                                stack.Push(matrix.Pos[i]);
                                hash.Add(matrix.Pos[i]);
                            }
                    checkField = "";
                }

                if (field.Count != matrix.Name.Count && stack.Count == 0)
                {
                    n++;
                    for (int i = 0; i < matrix.Name.Count; ++i)
                        if (!hash.Contains(matrix.Pos[i]))
                        {

                            field.Add(matrix.Pos[i], n);
                            stack.Push(matrix.Pos[i]);
                            hash.Add(matrix.Pos[i]);
                            break;
                        }
                }
            };

            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            foreach (var e in field)
            {
                sb.Append(e.Key);
                sb2.Append(e.Value);
            }

            return sb.Append(Environment.NewLine).Append(sb2).ToString();
        }

        public string DijkstrasAlgorithm(AdjacencyMatrix matrix, string startPos, string finalPos)
        {
            var queue = new Queue<string>();
            var dictEvaluation = new Dictionary<string, int>();
            var dict = new Dictionary<string, List<Direction>>();
            foreach(var e in matrix.Matrix)
            {
                dict.Add(e.Key, new List<Direction>());
                dictEvaluation.Add(e.Key, int.MaxValue);
                for (int i = 0; i < e.Value.Length; ++i)                
                    if(e.Value[i] != 0) 
                        dict[e.Key].Add(new Direction(matrix.Pos[i], e.Value[i]));                
            }
            dictEvaluation[startPos] = 0;
            queue.Enqueue(startPos);
            var checkPos = "";
            while (queue.Count != 0)
            {
                checkPos = queue.Dequeue();
                foreach(var direction in dict[checkPos])
                    if (dictEvaluation[direction.Name] > direction.Weight + dictEvaluation[checkPos])
                    {
                        dictEvaluation[direction.Name] = direction.Weight + dictEvaluation[checkPos];
                        queue.Enqueue(direction.Name);
                    }
            }

            var sb = new StringBuilder();
            sb.Append(finalPos);
            queue.Enqueue(finalPos);
            while (queue.Count != 0)
            {
                checkPos = queue.Dequeue();

                foreach (var direction in dict[checkPos])
                    if (dictEvaluation[direction.Name] == dictEvaluation[checkPos] - direction.Weight)
                    {
                        sb.Append($"-{direction.Weight}-{direction.Name}");
                        queue.Enqueue(direction.Name);
                        break;
                    }
            }
            return sb.ToString();
        }
    }
}
