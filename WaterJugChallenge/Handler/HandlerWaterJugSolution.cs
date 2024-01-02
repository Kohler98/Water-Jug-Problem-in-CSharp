using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaterJugChallenge.handler
{
    class HandlerWaterJugSolution
    {
        public class WaterJugRespone
        {
            public int jugX { get; set; }
            public int jugY { get; set; }
            public string explanation { get; set; }

        }
        public static List<string> SolveWaterBucketProblem(int x, int y, int target)
        {
            Dictionary<Tuple<int, int>, int> nodeMap = new Dictionary<Tuple<int, int>, int>();
            bool isSolvable = false;
            List<string> solution = new List<string>(); ;
            Dictionary<Tuple<int, int>, string> move = new Dictionary<Tuple<int, int>, string>();
            Dictionary<Tuple<int, int>, Tuple<int, int>> nodePath = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(0, 0));

            while (queue.Count > 0)
            {

                var u = queue.Dequeue();
                if (nodeMap.ContainsKey(u) && nodeMap[u] == 1)
                {
                    continue;
                }

                if ((u.Item1 > x || u.Item2 > y || u.Item1 < 0 || u.Item2 < 0))
                {
                    continue;

                }
                nodeMap[u] = 1;

                if (u.Item1 == target || u.Item2 == target)
                {
                    isSolvable = true;

                    bfsSolution(nodePath, u, move, solution);
                    if (u.Item1 == target)
                    {
                        if (u.Item2 != 0)
                        {
                            WaterJugRespone response = new WaterJugRespone
                            {
                                jugX = u.Item1,
                                jugY = 0,
                                explanation = "Empty JugY",
                            };
                            string json = JsonConvert.SerializeObject(response);
                            solution.Add(json);

                        }
                    }
                    else
                    {
                        if (u.Item1 != 0)
                        {
                            WaterJugRespone response = new WaterJugRespone
                            {
                                jugX = 0,
                                jugY = u.Item2,
                                explanation = "Empty JugX",
                            };
                            string json = JsonConvert.SerializeObject(response);
                            solution.Add(json);
                        }

                    }
                    return solution;
                }
                // Filling the 2nd jug completely
               if (!nodeMap.ContainsKey(new Tuple<int, int>(u.Item1, y)))
                {
                    queue.Enqueue(new Tuple<int, int>(u.Item1, y));
                    nodePath[new Tuple<int, int>(u.Item1, y)] = u;
                    move[new Tuple<int, int>(u.Item1, y)] = "Filling JugY";
                }

                // Filling the 1st jug completely
               if (!nodeMap.ContainsKey(new Tuple<int, int>(x, u.Item2)))
                {
                    queue.Enqueue(new Tuple<int, int>(x, u.Item2));
                    nodePath[new Tuple<int, int>(x, u.Item2)] = u;
                    move[new Tuple<int, int>(x, u.Item2)] = "Filling JugX";
                }

                // Transferring contents of 1st Jug to 2nd Jug
                int d = y - u.Item2;
                if (u.Item1 >= d)
                {
                    int c = u.Item1 - d;
                    if (!nodeMap.ContainsKey(new Tuple<int, int>(c, y)))
                    {
                        queue.Enqueue(new Tuple<int, int>(c, y));
                        nodePath[new Tuple<int, int>(c, y)] = u;
                        move[new Tuple<int, int>(c, y)] = "Transfer from JugX to JugY";
                    }
                }
                else
                {
                    int c = u.Item1 + u.Item2;
                    if (!nodeMap.ContainsKey(new Tuple<int, int>(0, c)))
                    {
                        queue.Enqueue(new Tuple<int, int>(0, c));
                        nodePath[new Tuple<int, int>(0, c)] = u;
                        move[new Tuple<int, int>(0, c)] = "Transfer from JugX to JugY";
                    }
                }
                // Transferring content of 2nd jug to 1st jug
                d = x - u.Item1;
                if (u.Item2 >= d)
                {
                    int c = u.Item2 - d;
                    if (!nodeMap.ContainsKey(new Tuple<int, int>(x, c)))
                    {
                        queue.Enqueue(new Tuple<int, int>(x, c));
                        nodePath[new Tuple<int, int>(x, c)] = u;
                        move[new Tuple<int, int>(x, c)] = "Transfer from JugY to JugX";
                    }
                }
               else
                {
                   int c = u.Item1 + u.Item2;
                   if (!nodeMap.ContainsKey(new Tuple<int, int>(c, 0)))
                    {
                        queue.Enqueue(new Tuple<int, int>(c, 0));
                        nodePath[new Tuple<int, int>(c, 0)] = u;
                        move[new Tuple<int, int>(c, 0)] = "Transfer from JugY to JugX";
                    }
                }

            // Emptying 2nd Jug
               if (!nodeMap.ContainsKey(new Tuple<int, int>(u.Item1, 0)))
                {
                    queue.Enqueue(new Tuple<int, int>(u.Item1, 0));
                    nodePath[new Tuple<int, int>(u.Item1, 0)] = u;
                    move[new Tuple<int, int>(u.Item1, 0)] = "Empty JugY";
                }

            // Emptying 1st jug
               if (!nodeMap.ContainsKey(new Tuple<int, int>(0, u.Item2)))
                {
                    queue.Enqueue(new Tuple<int, int>(0, u.Item2));
                    nodePath[new Tuple<int, int>(0, u.Item2)] = u;
                    move[new Tuple<int, int>(0, u.Item2)] = "Empty JugX";
                }
            }
            if (!isSolvable)
                solution.Add("Solution not possible");
            return solution;
        }
    static void bfsSolution(Dictionary<Tuple<int, int>, Tuple<int, int>> nodePath, Tuple<int, int> u, Dictionary<Tuple<int, int>, string> move, List<string> solution)
    {
        if (u.Item1 == 0 && u.Item2 == 0)
        {

            return;
        }
        bfsSolution(nodePath, nodePath[u], move, solution);
        WaterJugRespone response = new WaterJugRespone
        {
            jugX = u.Item1,
            jugY = u.Item2,
            explanation = move[u],
        };
        string jsonResp = JsonConvert.SerializeObject(response);
        solution.Add(jsonResp);
    }
}

}
