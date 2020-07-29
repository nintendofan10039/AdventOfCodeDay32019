using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay3
{
    public enum directions{
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    class Instruction
    {
        public Instruction()
        {

        }

        public Instruction(directions direction, int steps)
        {
            this.direction = direction;
            this.steps = steps;
        }

        public directions direction;
        public int steps;
    }
}
