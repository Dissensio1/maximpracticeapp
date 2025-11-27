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

    public static Order GetOrderById(int id, List<Order> list)
    {
        Order order = list.FirstOrDefault(o => o.identifier == id);
        return order;
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
}