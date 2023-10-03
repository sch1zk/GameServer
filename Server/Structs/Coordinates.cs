namespace Game.Server.Structs;

public struct Coordinates
{
    public Coordinates(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }

    public double Y { get; set; }

    public static double CalculateDistance(Coordinates a, Coordinates b)
    {
        double dx = b.X - a.X;
        double dy = b.Y - a.Y;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }
}