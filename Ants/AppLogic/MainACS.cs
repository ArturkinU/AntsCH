using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Ants
{
    class MainACS
    {
        public void Start(int Beta, int Alpha, double EvaporationRate, int AntCount, int Iterations, List<AntPoint> Points, ref Canvas LineLayer, ref bool CanEditPoints, ref Label infoPanel, ref Image Loader)
        {
               // Parse TSPlib file and load as List<Point>

            Graph graph = new Graph(Points, true);  // Create Graph
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
            double greedyShortestTourDistance = greedyAlgorithm.Run();  // get shortest tour using greedy algorithm

            Parameters parameters = new Parameters()
            {
                AntCount = AntCount,
                Beta = Beta,
                GlobalEvaporationRate = EvaporationRate,
                Iterations = Iterations
                
            };

          

            Solver solver = new Solver(parameters, graph);
            LineLayer.Children.Clear();
            List<double> results = solver.RunACS(); // Run ACS
            List<Edge> EdgesWalk = solver.AntEdgesWalk;

            infoPanel.Content = $"Длина пути: {results.Last()}m";
            CanEditPoints = false;
            Loader.BeginAnimation(Image.OpacityProperty, new DoubleAnimation()
            {
                To = 100,
                From = 0,
                Duration = TimeSpan.FromSeconds(10)
            });

            foreach (Edge edge in EdgesWalk)
            {
                LineLayer.Children.Add(drowLine(edge));
            }



        }

        public Line drowLine(Edge edge)
        {
        Line line = new Line() {
            
               X1 = (int)edge.Start.X, Y1 = (int)edge.Start.Y,
               X2 = (int)edge.End.X, Y2 = (int)edge.End.Y,
               Stroke = new SolidColorBrush(Colors.Red),
               StrokeThickness = 2,
               StrokeDashCap = PenLineCap.Round,
               StrokeStartLineCap = PenLineCap.Round,
               StrokeEndLineCap = PenLineCap.Round
        };

            return line;

        }
    }
}
