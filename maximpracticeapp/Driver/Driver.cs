using System.Data.Common;
public class Driver
{
    private int identifier;
    private int xCord;
    private int yCord;
    private double distance;

    public Driver(int x, int y)
    {
        this.xCord = x;
        this.yCord = y;
    }
    public Driver(int id, int x, int y)
    {
        this.identifier = id;
        this.xCord = x;
        this.yCord = y;
    }

    public int Identifier
    {
        get { return this.identifier; }
 
        set { this.identifier = value; }
    }

    public int XCord
    {
        get { return this.xCord; }
 
        set { this.xCord = value; }
    }

    public int YCord
    {
        get { return this.yCord; }
 
        set { this.yCord = value; }
    }

    public double Distance
    {
        get { return this.distance; }
 
        set { this.distance = value; }
    }
}