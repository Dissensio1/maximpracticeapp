namespace Program;
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
        Console.Write("Введите количество водителей: ");
        int size = int.Parse(Console.ReadLine());

        if(size > m * n)
        {
            size = rand.Next(1, m + n - 1);
        }

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

        var nearest = Map.FindNearestDriversSort(newOrder, driversArr, 5);

        Console.WriteLine("Алгоритм 1 (сортировка):");
        for(int i = 0; i < nearest.Count; i++)
        {
            Console.Write($"id: {nearest[i].Identifier}");
            Console.WriteLine($" distance: {nearest[i].Distance}");
        }

        nearest = Map.FindNearestDriversHeap(newOrder, driversArr, 5);
        
        Console.WriteLine("Алгоритм 2 (куча):");
        for (int i = 0; i < nearest.Count; i++)
        {
            Console.Write($"id: {nearest[i].Identifier}");
            Console.WriteLine($" distance: {nearest[i].Distance}");
        }

        nearest = Map.FindNearestDriversIncr(newOrder, driversArr, 5);
        
        Console.WriteLine("Алгоритм 3 (инкрементальный):");
        for (int i = 0; i < nearest.Count; i++)
        {
            Console.Write($"id: {nearest[i].Identifier}");
            Console.WriteLine($" distance: {nearest[i].Distance}");
        }
    }
}