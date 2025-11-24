namespace Program;
public static class IncrementalAlgorithm
{
    public static List<Driver> FindNearestPoints(Order order, Driver[] drivers, int k = 5)
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
                // Если еще не набрали k точек, просто добавляем
                nearestDrivers.Add((dist, driver));
                if (nearestDrivers.Count == k)
                {
                    // Находим текущее максимальное расстояние
                    maxDistance = nearestDrivers.Max(x => x.distance);
                }
            }
            else
            {
                // Если нашли точку ближе, чем самая дальняя в текущем списке
                if (dist < maxDistance)
                {
                    // Находим индекс самой дальней точки
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
                        // Удаляем самую дальнюю точку и добавляем новую
                        nearestDrivers.RemoveAt(maxIndex);
                        nearestDrivers.Add((dist, driver));
                        
                        // Обновляем максимальное расстояние
                        maxDistance = nearestDrivers.Max(x => x.distance);
                    }
                }
            }
        }
        return nearestDrivers.OrderBy(x => x.distance).Select(x => x.driver).ToList();
    }
}
public static class HeapAlgorithm
{
    public static List<Driver> FindNearestPoints(Order order, Driver[] drivers, int k = 5)
    {
        if (drivers == null || drivers.Length == 0)
            return new List<Driver>();
        
        k = Math.Min(k, drivers.Length);

        // Используем SortedSet как max-heap (самая дальняя точка будет первой)
        var maxHeap = new SortedSet<(double distance, Driver driver)>(Comparer<(double distance, Driver driver)>.Create((a, b) => 
        {
            int cmp = b.distance.CompareTo(a.distance); // Обратный порядок для max-heap
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
                // Если текущая точка ближе, чем самая дальняя в куче
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
}
internal static class Program
{
    private static void Main(string[] args)
    {
        var rand = new Random();
        int newX;
        int newY;

        Console.Write("Введите n: ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("Введите m: ");
        int m = int.Parse(Console.ReadLine());

        int size = 10;
        Driver[] driversArr;
        Order newOrder;
        var uniqueDrivers = new HashSet<Driver>();

        while (uniqueDrivers.Count < size)
        {
            newX = rand.Next(0, n);
            newY = rand.Next(0, m);

            var newDriver = new Driver(newX, newY);
            uniqueDrivers.Add(newDriver);
        }

        driversArr = uniqueDrivers.ToArray();
        newOrder = new Order(1, rand.Next(0, n), rand.Next(0, m));

        for (int i = 0; i < size; i++)
        {
            driversArr[i].Identifier = i + 1;
            driversArr[i].Distance = Math.Pow(driversArr[i].XCord - newOrder.XCord, 2) + Math.Pow(driversArr[i].YCord - newOrder.YCord, 2);
        }

        Map mapp = new Map(n, m);
        for (int i = 0; i < size; i++)
        {
            newX = driversArr[i].XCord;
            newY = driversArr[i].YCord;
            mapp.setMapValue(newX, newY, driversArr[i]);
        }
        mapp.setMapValue(newOrder.XCord, newOrder.YCord, newOrder);
        Map.mapVisualisation(mapp);
        
        Console.WriteLine("drivers:");
        for(int i = 0; i < size; i++)
        {
            Console.Write("id:" + driversArr[i].Identifier);
            Console.WriteLine(" distance:" + driversArr[i].Distance);
        }

        Array.Sort(driversArr, (d1, d2) => d1.Distance.CompareTo(d2.Distance));
        Console.WriteLine("Алгоритм 1 (сортировка):");
        for(int i = 0; i < 5; i++)
        {
            Console.Write($"id: {driversArr[i].Identifier}");
            Console.WriteLine($" distance: {driversArr[i].Distance}");
        }

        var nearest = HeapAlgorithm.FindNearestPoints(newOrder, driversArr, 5);
        
        Console.WriteLine("Алгоритм 2 (куча):");
        for (int i = 0; i < nearest.Count; i++)
        {
            Console.Write($"id: {nearest[i].Identifier}");
            Console.WriteLine($" distance: {nearest[i].Distance}");
        }

        nearest = IncrementalAlgorithm.FindNearestPoints(newOrder, driversArr, 5);
        
        Console.WriteLine("Алгоритм 4 (инкрементальный):");
        for (int i = 0; i < nearest.Count; i++)
        {
            Console.Write($"id: {nearest[i].Identifier}");
            Console.WriteLine($" distance: {nearest[i].Distance}");
        }
    }
}