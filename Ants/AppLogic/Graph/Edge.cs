using System;

namespace Ants
{
    public class Edge
    {
        public AntPoint Start { get; set; }
        public AntPoint End { get; set; }
        public double Length { get; set; }
        public double Pheromone { get; set; }
        public double Weight { get; set; }

        public Edge() { }

        public Edge(AntPoint start, AntPoint end)
        {
            Start = start;
            End = end;
            Length = Math.Round(Start.DistanceTo(End));
        }
    }
}
