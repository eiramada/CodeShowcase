namespace DirectedAcyclicGraphPathsFinder
{
    public class GraphPathsFinder
    {
        private Dictionary<int, List<int>> AdjacencyList;
         
        public GraphPathsFinder(List<(int, int)> edges)
        {
            AdjacencyList = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                if (!AdjacencyList.ContainsKey(edge.Item1))
                {
                    AdjacencyList[edge.Item1] = new List<int>();
                }
                AdjacencyList[edge.Item1].Add(edge.Item2);
            }
        }

        public List<List<int>> FindAllPaths(int startVertex, int endVertex)
        {
            var paths = new List<List<int>>();
            var visited = new HashSet<int>();
            FindAllPathsUtil(startVertex, endVertex, visited, new List<int>(), paths);
            return paths;
        }

        private void FindAllPathsUtil(int currentVertex, int endVertex, HashSet<int> visited, List<int> path, List<List<int>> paths)
        {
            visited.Add(currentVertex);
            path.Add(currentVertex);

            if (currentVertex == endVertex)
            {
                paths.Add(new List<int>(path));
            }

            else if (AdjacencyList.ContainsKey(currentVertex))
            {
                foreach (int neighbor in AdjacencyList[currentVertex])
                {
                    if (!visited.Contains(neighbor))
                    {
                        FindAllPathsUtil(neighbor, endVertex, visited, path, paths);
                    }
                }
            }

            path.RemoveAt(path.Count - 1);
            visited.Remove(currentVertex);
        }
    }
}
