using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace maximpracticeappBenchmark
{
    [Config(typeof(Config))]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class MapBenchmark
    {
        private Random rand;
        private Driver[] driversArr;
        private Order newOrder;

        [Params(100, 1000, 10000)]
        public int NumberOfDrivers { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            rand = new Random();
            int newX;
            int newY;

            int xSize = 1000;
            int ySize = 1000;

            var uniqueDrivers = new HashSet<Driver>();

            while (uniqueDrivers.Count < NumberOfDrivers)
            {
                newX = rand.Next(0, xSize);
                newY = rand.Next(0, ySize);

                var newDriver = new Driver(newX, newY);
                uniqueDrivers.Add(newDriver);
            }

            driversArr = uniqueDrivers.ToArray();
            newOrder = new Order(1, rand.Next(0, xSize), rand.Next(0, ySize));

            for (int i = 0; i < NumberOfDrivers; i++)
            {
                driversArr[i].Identifier = i + 1;
                driversArr[i].Distance = Math.Pow(driversArr[i].XCord - newOrder.XCord, 2) + Math.Pow(driversArr[i].YCord - newOrder.YCord, 2);
            }
        }

        [Benchmark(Baseline = true)]
        public List<Driver> SortAlg()
        {
            return Map.FindNearestDriversSort(newOrder, driversArr, 5);
        }
        [Benchmark]
        public List<Driver> HeapAlg()
        {
            return Map.FindNearestDriversHeap(newOrder, driversArr, 5);
        }
        [Benchmark]
        public List<Driver> IncrAlg()
        {
            return Map.FindNearestDriversIncr(newOrder, driversArr, 5);
        }
    }
    public class Config:ManualConfig
    {
        public Config()
        {
            AddExporter(HtmlExporter.Default);
            AddExporter(CsvExporter.Default);
            
            AddColumn(StatisticColumn.Median);
        }
    }
}