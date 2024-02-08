namespace DirectedAcyclicGraphPathsFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Suunatud atsüklilise graafi võimalikud teed kahe tipu vahel");
            RunUserInteraction();
        }

        private static void RunUserInteraction()
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("\nKas tahad näidet näha? (Y/N) Või lõpeta (E)");
                string? reply = Console.ReadLine();

                if (reply?.ToUpper() == "E")
                {
                    continueRunning = false;
                }
                else if (IsValidInput(reply))
                {
                    HandleInput(reply);
                }
                else
                {
                    Console.WriteLine("Proovi uuesti");
                }
            }
        }

        private static bool IsValidInput(string input)
        {
            return input?.ToUpper() == "Y" || input?.ToUpper() == "N";
        }

        private static void HandleInput(string input)
        {
            if (input.ToUpper() == "Y")
            {
                RunExample();
            }
            else
            {
                GetUserInputGraph();
            }
        }

        private static void RunExample()
        {
            List<(int, int)> exampleGraph = new List<(int, int)> { (1, 2), (1, 3), (2, 4), (3, 4) };
            DisplayGraphAndFindPaths(exampleGraph, 1, 4);
        }

        private static void GetUserInputGraph()
        {
            Console.WriteLine("\nLähme katsetama! Lõpetamiseks sisesta (E).");
            List<(int, int)> edges = new List<(int, int)>();

            while (true)
            {
                Console.Write("Sisesta seos: ");
                string? userInput = Console.ReadLine();
                if (userInput?.ToUpper() == "E") break;

                if (TryParseVertexPair(userInput, out int startEdge, out int endEdge))
                {
                    AddEdgeIfNotExists(edges, startEdge, endEdge);
                }
                else
                {
                    Console.WriteLine("Vigane sisend, proovi uuesti!");
                }
            }

            if (edges.Any())
            {
                while (true)
                {
                    Console.Write("Mis on algus- ja lõputipp? ");
                    string? verticesInput = Console.ReadLine();
                    if (TryParseVertexPair(verticesInput, out int startVertex, out int endVertex))
                    {
                        DisplayGraphAndFindPaths(edges, startVertex, endVertex);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Vigane sisend, proovi uuesti!");
                    }
                }
            }
        }

        private static bool TryParseVertexPair(string userInput, out int start, out int end)
        {
            start = end = 0;

            if (userInput == null)
            {
                return false;
            }

            string[]? parts = userInput?.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts?.Length == 2 && int.TryParse(parts[0], out start) && int.TryParse(parts[1], out end))
            {
                return true;

            }
            return false;
        }


        private static void DisplayGraphAndFindPaths(List<(int, int)> graph, int startVertex, int endVertex)
        {
            Console.WriteLine("\nGraafi tipud & nende seosed:");
            foreach ((int, int) pair in graph)
            {
                Console.WriteLine($"{pair.Item1}, {pair.Item2}");
            }

            Console.WriteLine($"\nKüsime teid tippude {startVertex} ja {endVertex} vahel");
            Console.WriteLine("\nKõikvõimalikud teed nende tippude vahel:");

            GraphPathsFinder finder = new GraphPathsFinder(graph);
            List<List<int>> paths = finder.FindAllPaths(startVertex, endVertex);
            foreach (List<int>? path in paths)
            {
                Console.WriteLine($"[{string.Join(", ", path)}]");
            }
        }

        private static bool AddEdgeIfNotExists(List<(int, int)> edges, int startVertex, int endVertex)
        {
            if (!edges.Contains((startVertex, endVertex)))
            {
                edges.Add((startVertex, endVertex));
                return true;
            }
            else
            {
                Console.WriteLine($"Seos ({startVertex}, {endVertex}) on juba olemas. Seda hetkel ei loe.");
                return false;
            }
        }
    }
}
