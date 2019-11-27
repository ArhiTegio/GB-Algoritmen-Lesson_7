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
    [Serializable]
    public class AdjacencyMatrix
    {
        public AdjacencyMatrix(Dictionary<string, List<Direction>> arrayConnection)
        {
            Matrix = new SerializableDictionary<string, int[]>();
            Name = new SerializableDictionary<string, int>();
            var step = 0;
            foreach (var e in arrayConnection)
            {
                Name.Add(e.Key, step);
                ++step;
            }

            foreach (var e in arrayConnection)
            {
                Matrix.Add(e.Key, new int[Name.Count]);
                foreach(var t in e.Value)
                    Matrix[e.Key][Name[t.Name]] = t.Weight;                
            }
        }
        
        public AdjacencyMatrix()
        {
        }
       
        public SerializableDictionary<string, int> Name { get; set; }
       
        public SerializableDictionary<string, int[]> Matrix { get; set; }
        
        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            var load = FileOperation<AdjacencyMatrix>.LoadFromXmlFormat(path);
            Name = load.Name;
            Matrix = load.Matrix;
        }

        /// <summary>
        /// Сохранить в файл
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path) => FileOperation<AdjacencyMatrix>.SaveAsXmlFormat(this, path);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"  {Name.Select(x => x.Key).Aggregate((x, y) => $"{x} {y}")}");
            foreach (var e in Matrix)
                sb.Append($"{e.Key} {e.Value.Select(x => x.ToString()).Aggregate((x, y) => $"{x} {y}")}");
            return sb.ToString();
        }

        public void Print()
        {
            WriteLine($"  {Name.Select(x => x.Key).Aggregate((x, y) => $"{x} {y}")}");
            foreach (var e in Matrix)
                WriteLine($"{e.Key} {e.Value.Select(x => x.ToString()).Aggregate((x, y) => $"{x} {y}")}");
        }
    }

    public class Direction
    {
        public Direction(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
