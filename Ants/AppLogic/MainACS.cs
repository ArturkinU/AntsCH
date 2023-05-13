using System;
using System.Collections.Generic;

namespace Ants
{
    class MainACS
    {
        static void Start(int Beta, double GlobalEvaporationRate, double LocalEvaporationRate, double Q0, int AntCount, int Iterations, List<AntPoint> Points)
        {
               // Parse TSPlib file and load as List<Point>

            Graph graph = new Graph(Points, true);  // Create Graph
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
            double greedyShortestTourDistance = greedyAlgorithm.Run();  // get shortest tour using greedy algorithm

            Parameters parameters = new Parameters()
            {
                AntCount = AntCount,
                Beta = Beta,
                GlobalEvaporationRate = GlobalEvaporationRate,
                LocalEvaporationRate = LocalEvaporationRate,
                Iterations = Iterations,
                Q0 = Q0,
                T0 = (1.0 / (graph.Dimensions * greedyShortestTourDistance)),

            };

            parameters.Show();

            Solver solver = new Solver(parameters, graph);
            List<double> results = solver.RunACS(); // Run ACS

            Console.WriteLine("Time: " + solver.GetExecutionTime());
            Console.ReadLine();
        }
    }
}
