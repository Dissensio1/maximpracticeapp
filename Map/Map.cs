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
        for(int i = 0; i < map.xLen; i++)
        {
            Console.Write($"{i}| ");
            for(int j = 0; j < map.yLen; j++)
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
        this.map[y, x] = value;
    }

    public static List<Driver> FindNearestDriversSort(Order order, Driver[] drivers, int k = 5)
    {
        List<Driver> driversSorted = new List<Driver>();
        Driver[] driversCopy = new Driver[drivers.Length];
        drivers.CopyTo(driversCopy, 0);

        if (drivers == null || drivers.Length == 0) return new List<Driver>();
        k = Math.Min(k, drivers.Length);

        Array.Sort(driversCopy, (d1, d2) => d1.Distance.CompareTo(d2.Distance));

        foreach(var driver in driversCopy)
        {
            if(driversSorted.Count < k){
                driversSorted.Add(driver);
            }
        }
        
        return driversSorted;
    }

    public static List<Driver> FindNearestDriversHeap(Order order, Driver[] drivers, int k = 5)
    {
        if (drivers == null || drivers.Length == 0)
            return new List<Driver>();
        
        k = Math.Min(k, drivers.Length);

        var maxHeap = new SortedSet<(double distance, Driver driver)>(Comparer<(double distance, Driver driver)>.Create((a, b) => 
        {
            int cmp = b.distance.CompareTo(a.distance);
            if (cmp == 0) return a.driver.GetHashCode().CompareTo(b.driver.GetHashCode());
            return cmp;
        }));

        foreach (var driver in drivers)
        {
            double dist = Math.Pow(driver.XCord - order.XCord, 2) + Math.Pow(driver.YCord - order.YCord, 2);
            
            if (maxHeap.Count < k)
            {
                maxHeap.Add((dist, driver));
            }
            else
            {
                var farthest = maxHeap.First();
                if (dist < farthest.distance)
                {
                    maxHeap.Remove(farthest);
                    maxHeap.Add((dist, driver));
                }
            }
        }

        return maxHeap.Select(x => x.driver).ToList();
    }

    public static List<Driver> FindNearestDriversIncr(Order order, Driver[] drivers, int k = 5)
    {
        if (drivers == null || drivers.Length == 0)
            return new List<Driver>();
        
        k = Math.Min(k, drivers.Length);

        var nearestDrivers = new List<(double distance, Driver driver)>();
        double maxDistance = double.MaxValue;

        foreach (var driver in drivers)
        {
            double dist = Math.Pow(driver.XCord - order.XCord, 2) + Math.Pow(driver.YCord - order.YCord, 2);
            
            if (nearestDrivers.Count < k)
            {
                nearestDrivers.Add((dist, driver));
                if (nearestDrivers.Count == k)
                {
                    maxDistance = nearestDrivers.Max(x => x.distance);
                }
            }
            else
            {
                if (dist < maxDistance)
                {
                    int maxIndex = -1;
                    double currentMax = double.MinValue;
                    
                    for (int i = 0; i < nearestDrivers.Count; i++)
                    {
                        if (nearestDrivers[i].distance > currentMax)
                        {
                            currentMax = nearestDrivers[i].distance;
                            maxIndex = i;
                        }
                    }
                    
                    if (maxIndex >= 0)
                    {
                        nearestDrivers.RemoveAt(maxIndex);
                        nearestDrivers.Add((dist, driver));
                        
                        maxDistance = nearestDrivers.Max(x => x.distance);
                    }
                }
            }
        }
        return nearestDrivers.OrderBy(x => x.distance).Select(x => x.driver).ToList();
    }
}