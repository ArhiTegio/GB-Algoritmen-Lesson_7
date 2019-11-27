using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            matrix.Print();
            matrix.Save("1.ml");
            var matrix2 = new AdjacencyMatrix(new Dictionary<string, List<Direction>>());
            matrix2.Load("1.ml");
            matrix2.Print();
        }

        public string BypassWidth(string pos)
        {

        }

        public string BypassHeight(string pos)
        {

        }

        public string DijkstrasAlgorithm()
        {

        }

        public string AlgorithmKruskal()
        {

        }
    }
}
