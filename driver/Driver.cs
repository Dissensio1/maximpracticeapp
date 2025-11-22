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

    public Driver changeLocation(int x, int y)
    {
        this.xCord = x;
        this.yCord = y;
        return this;
    }

    public static Driver getDriverById(int id, List<Driver> list)
    {
        Driver driver = list.FirstOrDefault(d => d.identifier == id);
        return driver;
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