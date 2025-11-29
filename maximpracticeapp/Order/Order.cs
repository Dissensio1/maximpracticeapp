public class Order
{
    private int identifier;
    private int xCord;
    private int yCord;

    public Order(int id, int x, int y)
    {
        this.identifier = id;
        this.xCord = x;
        this.yCord = y;
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
}