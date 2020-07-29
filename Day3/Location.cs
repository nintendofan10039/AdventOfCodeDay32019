using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCodeDay3
{
    public class Location : IComparable<Location>
    {
        int x;
        int y;
        int runningStepCount;

        public int X
        {
            get { return this.x; }
        }

        public int Y
        {
            get { return this.y; }
        }

        public int StepCount { get { return this.runningStepCount; } }

        public Location(int x, int y, int runningStepCount)
        {
            this.x = x;
            this.y = y;
            this.runningStepCount = runningStepCount;
        }

        public int CompareTo(Location other)
        {
            if (other.x == this.x && other.y == this.y)
                return 1;
            else
                return 0;
        }
    }
}
