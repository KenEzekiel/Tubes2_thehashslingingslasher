using Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_Stima.src
{
    class TSPBFS : BFS
    {
        public static List<int> Solve(List<Tuple<double, double>> cities)
        {
            // jarak
            double[,] distances = GenerateDistances(cities);

            // Start with the first city as the current node
            Block current = new Block(0, 0);
            current.City = 0;

            // Create a queue to hold the nodes to visit
            BFS queue = new BFS();
            queue.insertNode(current, ' ');

            // Create a dictionary to store the optimal path to each node
            Dictionary<Block, List<char>> paths = new Dictionary<Block, List<char>>();
            paths[current] = new List<char>();

            // Perform BFS to find the optimal path
            while (queue.NodeMoves.Count > 0)
            {
                var node = queue.NodeMoves.Dequeue();
                char move = node.move;
                current = node.node;

                if (!paths.ContainsKey(current))
                {
                    paths[current] = new List<char>(paths[current.getParent()]);
                    paths[current].Add(move);
                    if (current.City == cities.Count - 1)
                    {
                        // We have found a complete path
                        return ConvertPath(paths[current]);
                    }
                }

                // Insert child nodes into the queue
                bool notDeadend = false;
                queue.insertChild(current, move, notDeadend);
            }

            return null;
        }

        private static double[,] GenerateDistances(List<Tuple<double, double>> cities)
        {
            int n = cities.Count;
            double[,] distances = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double distance = Math.Sqrt(
                        Math.Pow(cities[i].Item1 - cities[j].Item1, 2) +
                        Math.Pow(cities[i].Item2 - cities[j].Item2, 2));
                    distances[i, j] = distance;
                    distances[j, i] = distance;
                }
            }

            return distances;
        }

        private static List<int> ConvertPath(List<char> path)
        {
            List<int> result = new List<int>();
            foreach (char move in path)
            {
                int city = int.Parse(move.ToString());
                result.Add(city);
            }
            return result;
        }

    }
}
