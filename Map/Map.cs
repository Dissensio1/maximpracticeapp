public class Map
{
    private readonly int xLen;
    private readonly int yLen;

    private readonly object[,] map;

    public Map(int x, int y)
    {
        this.xLen = x;
        this.yLen = y;
        this.map = new object[y, x];
    }

    public static void mapVisualisation(Map map)
    {
        for(int i = 0; i < map.yLen; i++)
        {
            Console.Write($"{i}| ");
            for(int j = 0; j < map.xLen; j++)
            {
                if(map.map[j, i] == null)
                {
                    Console.Write("X ");
                }
                else if(map.map[j, i] is Driver driver)
                {
                    Console.Write(driver.Identifier + " ");
                }
                else if(map.map[j, i] is Order)
                {
                    Console.Write("OO ");
                }
            }
            Console.Write("|");
            Console.WriteLine();
        }
    }

    public void setMapValue(int x, int y, object value)
    {
        this.map[x, y] = value;
    }
}