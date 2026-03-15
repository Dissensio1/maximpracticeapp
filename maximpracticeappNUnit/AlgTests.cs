namespace maximpracticeappNUnit
{
    [TestFixture]
    public class Tests
    {
        private Order newOrder;
        private Driver[] driversArr;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driversArr = new Driver[]{new Driver(1, 10, 11), new Driver(2, 5, 10), 
                new Driver(3, 14, 56), new Driver(4, 55, 12), new Driver(5, 34, 10), 
                new Driver(6, 10, 89), new Driver(7, 80, 80), new Driver(8, 15, 90), 
                new Driver(9, 53, 23), new Driver(10, 78, 45)};
            newOrder = new Order(1, 10, 46);
            
            int NumberOfDrivers = driversArr.Length;

            for (int i = 0; i < NumberOfDrivers; i++)
            {
                driversArr[i].Distance = Math.Pow(driversArr[i].XCord - newOrder.XCord, 2) + Math.Pow(driversArr[i].YCord - newOrder.YCord, 2);
            }
        }

        [Test]
        public void SortAlg_ReturnsCorrectList()
        {
            List<Driver> driversList;
            string expected = "id:3 dist:116, id:1 dist:1225, id:2 dist:1321, id:6 dist:1849, id:5 dist:1872";

            driversList = Map.FindNearestDriversSort(newOrder, driversArr, 5);
            string result = string.Join(", ", driversList.Select(d => $"id:{d.Identifier} dist:{d.Distance}"));

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void HeapAlg_ReturnsCorrectList()
        {
            List<Driver> driversList;
            string expected = "id:5 dist:1872, id:6 dist:1849, id:2 dist:1321, id:1 dist:1225, id:3 dist:116";

            driversList = Map.FindNearestDriversHeap(newOrder, driversArr, 5);
            string result = string.Join(", ", driversList.Select(d => $"id:{d.Identifier} dist:{d.Distance}"));

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void IncrAlg_ReturnsCorrectList()
        {
            List<Driver> driversList;
            string expected = "id:3 dist:116, id:1 dist:1225, id:2 dist:1321, id:6 dist:1849, id:5 dist:1872";

            driversList = Map.FindNearestDriversIncr(newOrder, driversArr, 5);
            string result = string.Join(", ", driversList.Select(d => $"id:{d.Identifier} dist:{d.Distance}"));

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}