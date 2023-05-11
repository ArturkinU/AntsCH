using System;
using System.Collections.Generic;

namespace Ants
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> points = TspFileReader.ReadTspFile(@".\TSP\kroA150.tsp");    // Parse TSPlib file and load as List<Point>

            Graph graph = new Graph(points, true);  // Create Graph
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
            double greedyShortestTourDistance = greedyAlgorithm.Run();  // get shortest tour using greedy algorithm

            Parameters parameters = new Parameters(2, 0.1, 0.01, (1.0 / (graph.Dimensions * greedyShortestTourDistance)), 0.9, 20, 10000);
            parameters.Show();

            Solver solver = new Solver(parameters, graph);
            List<double> results = solver.RunACS(); // Run ACS

            Console.WriteLine("Time: " + solver.GetExecutionTime());
            Console.ReadLine();
        }
    }
}
