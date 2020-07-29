using NUnit.Framework;
using AdventOfCodeDay3;

namespace Day3Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ManhattanDistanceTest1()
        {
            Location node1 = new Location(6,6);
            Assert.AreEqual(12,Day3.CalculateManhattanDistance(node1));
        }

        [Test]
        public void ManhattanDistanceTest2()
        {
            Location node1 = new Location(3, 3);
            Assert.AreEqual(6, Day3.CalculateManhattanDistance(node1));
        }
    }
}