using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaterJugChallenge.handler
{
    class HandlerWaterJugSolution
    {

        //this class is used to build the final json which will be sending in the response
        public class WaterJugRespone
        {
            public int jugX { get; set; }
            public int jugY { get; set; }
            public string explanation { get; set; }

        }
        public static List<string> SolveWaterBucketProblem(int x, int y, int target)
        {
            Dictionary<Tuple<int, int>, int> nodeMap = new Dictionary<Tuple<int, int>, int>(); // stores the nodes visited
            bool isSolvable = false; 
            List<string> solution = new List<string>(); // stores the solution
            Dictionary<Tuple<int, int>, string> move = new Dictionary<Tuple<int, int>, string>(); // stores the movements made (transfer, empty,fill)
            Dictionary<Tuple<int, int>, Tuple<int, int>> nodePath = new Dictionary<Tuple<int, int>, Tuple<int, int>>();// stores the solution path
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();// stores the current states 
            Tuple<int, int> path;// it's an aux variable which stores the keys to acccess the nodes in the nodeMap variable, so we can check if the nodes are visited or not
            queue.Enqueue(new Tuple<int, int>(0, 0));

            while (queue.Count > 0)
            {

                var u = queue.Dequeue();

                // if the nodeMap in the position u is visited we skip this iteration
                if (nodeMap.ContainsKey(u) && nodeMap[u] == 1)
                {
                    continue;
                }
               
                if ((u.Item1 > x || u.Item2 > y || u.Item1 < 0 || u.Item2 < 0))
                {
                    continue;

                }

                // we mark the nodes visited
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
                    break;
                }
                // Filling the jugy completely
                path = new Tuple<int, int>(u.Item1, y);
                if (!nodeMap.ContainsKey(path))
                {
                    queue.Enqueue(path);
                    nodePath[path] = u;
                    move[path] = "Filling JugY";
                }

                // Filling the jugx completely
                path = new Tuple<int, int>(x, u.Item2);
               if (!nodeMap.ContainsKey(path))
                {
                    queue.Enqueue(path);
                    nodePath[path] = u;
                    move[path] = "Filling JugX";
                }

                // Transferring from  JugX to JugY
                int d = y - u.Item2;
                if (u.Item1 >= d)
                {
                    int c = u.Item1 - d;
                    path = new Tuple<int, int>(c, y);
                    if (!nodeMap.ContainsKey(path))
                    {
                        queue.Enqueue(path);
                        nodePath[path] = u;
                        move[path] = "Transfer from JugX to JugY";
                    }
                }
                else
                {
                    int c = u.Item1 + u.Item2;
                    path = new Tuple<int, int>(0, c);
                    if (!nodeMap.ContainsKey(path))
                    {
                        queue.Enqueue(path);
                        nodePath[path] = u;
                        move[path] = "Transfer from JugX to JugY";
                    }
                }
                // Transferring from JugY to JugX
                d = x - u.Item1;
                if (u.Item2 >= d)
                {
                    int c = u.Item2 - d;
                    path = new Tuple<int, int>(x, c);
                    if (!nodeMap.ContainsKey(path))
                    {
                        queue.Enqueue(path);
                        nodePath[path] = u;
                        move[path] = "Transfer from JugY to JugX";
                    }
                }
               else
                {
                   int c = u.Item1 + u.Item2;
                    path = new Tuple<int, int>(c, 0);
                    if (!nodeMap.ContainsKey(path))
                    {
                        queue.Enqueue(path);
                        nodePath[path] = u;
                        move[path] = "Transfer from JugY to JugX";
                    }
                }

                // Emptying JugY
                path = new Tuple<int, int>(u.Item1, 0);
                if (!nodeMap.ContainsKey(path))
                {
                    queue.Enqueue(path);
                    nodePath[path] = u;
                    move[path] = "Empty JugY";
                }

                // Emptying JugX
                path = new Tuple<int, int>(0, u.Item2);
                if (!nodeMap.ContainsKey(path))
                {
                    queue.Enqueue(path);
                    nodePath[path] = u;
                    move[path] = "Empty JugX";
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
