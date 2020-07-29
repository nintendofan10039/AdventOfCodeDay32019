using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

/* Manhattan distance: xdist + ydist,
 * xdist = absolute value(x1-x2)
 * ydist = absolute value(y1-y2)
 * wires do not cross at central port and a wire doesn't cross with itself
 * an O marks the central port
 * commas separate directions(U = up, D = down, L = left, R = right)
 * 
 */
namespace AdventOfCodeDay3
{
    public class Day3
    {
        static void Main(string[] args)
        {
            string filePath = "D:\\Mykola\\Pictures\\inputDay3.txt";
            string[] unformattedText = FileRead(filePath);
            List<Instruction> instructions1 = MakeInstructions(unformattedText[0]);
            List<Instruction> instructions2 = MakeInstructions(unformattedText[1]);
            /*int stepsUp = GetMax(instructions1, instructions2, directions.UP);
            int stepsDown = GetMax(instructions1, instructions2, directions.DOWN);
            int stepsLeft = GetMax(instructions1, instructions2, directions.LEFT);
            int stepsRight = GetMax(instructions1, instructions2, directions.RIGHT);
            int height1 = CompareDirections(instructions1, instructions2, directions.UP, directions.DOWN);
            int height2 = CompareDirections(instructions1, instructions2, directions.DOWN, directions.UP);
            int width1 = CompareDirections(instructions1, instructions2, directions.LEFT, directions.RIGHT);
            int width2 = CompareDirections(instructions1, instructions2, directions.RIGHT, directions.LEFT);
            char[,] map = new char[(width1 + width2), (height1 + height2)];*/
            int originX = 0, originY = 0;
            List<Location> wire1 = DrawWire(instructions1, originX, originY);
            List<Location> wire2 = DrawWire(instructions2, originX, originY);
            int distance = CompareWireLocations(wire1, wire2);
            Console.WriteLine(distance);
            //originX = stepsLeft;
            //originY = stepsUp;
            //map = PopulateMap(map, originX, originY);
            //map = DrawWire(instructions1, map, originX, originY);

            /*Console.WriteLine("Steps up: " + stepsUp);
            Console.WriteLine("Steps down: " + stepsDown);
            Console.WriteLine("Steps left: " + stepsLeft);
            Console.WriteLine("Steps right: " + stepsRight);
            Console.WriteLine("Height1: " + height1);
            Console.WriteLine("Height2: " + height2);
            Console.WriteLine("Width1: " + width1);
            Console.WriteLine("Width2: " + width2);*/
            /*int originX = 0;
            int originY = 0;
            int endPoint1X = 0;
            int endPoint1Y = 0;
            int endPoint2X = 0;
            int endPoint2Y = 0;

            foreach (Instruction instruction in instructions1)
            {
                switch (instruction.direction)
                {
                    case directions.UP:
                        endPoint1Y += instruction.steps;
                        break;
                    case directions.DOWN:
                        endPoint1Y -= instruction.steps;
                        break;
                    case directions.LEFT:
                        endPoint1X -= instruction.steps;
                        break;
                    case directions.RIGHT:
                        endPoint1X += instruction.steps;
                        break;
                    default:
                        break;
                }
            }

            foreach (Instruction instruction in instructions2)
            {
                switch (instruction.direction)
                {
                    case directions.UP:
                        endPoint1Y += instruction.steps;
                        break;
                    case directions.DOWN:
                        endPoint1Y -= instruction.steps;
                        break;
                    case directions.LEFT:
                        endPoint1X -= instruction.steps;
                        break;
                    case directions.RIGHT:
                        endPoint1X += instruction.steps;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(endPoint1X + " " + endPoint1Y);
            Console.WriteLine(endPoint2X + " " + endPoint2Y);*/

        }

        static int CompareWireLocations(List<Location> wire1, List<Location> wire2)
        {
            int distance = int.MaxValue;
            //int manhattanDist;
            for (int i = 0; i < wire1.Count; i++)
            {
                for (int j = 0; j < wire2.Count; j++)
                if (wire1[i].CompareTo(wire2[j]) == 1)
                {
                        //manhattanDist = CalculateManhattanDistance(wire1[i]);
                        //if (distance > manhattanDist)
                        //distance = manhattanDist;
                        if (distance > (wire1[i].StepCount + wire2[j].StepCount))
                            distance = wire1[i].StepCount + wire2[j].StepCount;
                }
                
            }
            
            if (distance == int.MaxValue)
                return 0;
            return distance;
        }

        static int GetShortestDistance(List<Location> crossedWires)
        {
            int distance = int.MaxValue;
            for (int i = 0; i < crossedWires.Count; i++)
            {
                int manhattanDist = CalculateManhattanDistance(crossedWires[i]);
                if (distance > manhattanDist)
                    distance = manhattanDist;
            }
            return distance;
        }

        static List<Location> DrawWire(List<Instruction> instructions, int originX, int originY)
        {
            int currentPositionX = originX;
            int currentPositionY = originY;
            int steps = 0;
            List<Location> locations = new List<Location>();

            foreach(Instruction instruction in instructions)
            {
                //Console.WriteLine("Origin:" + currentPositionX + " " + currentPositionY);
                //Console.WriteLine(instruction.steps);
                switch (instruction.direction)
                {
                    case directions.UP:
                        for (int i = 0; i < instruction.steps; i++)
                        {
                            currentPositionY--;
                            steps++;
                            locations.Add(new Location(currentPositionX, currentPositionY, steps));
                            /*if (i == instruction.steps - 1 && map[currentPositionX, currentPositionY] != '|' && map[currentPositionX, currentPositionY] != '-')
                            {
                                map[currentPositionX, currentPositionY] = '+';
                            }
                            else if (map[currentPositionX, currentPositionY] == '.')
                            {
                                map[currentPositionX, currentPositionY] = '|';
                            }
                            else
                            {
                                map[currentPositionX, currentPositionY] = 'X';
                            }*/
                        }
                        break;
                    case directions.DOWN:
                        for (int i = 0; i < instruction.steps; i++)
                        {
                            currentPositionY++;
                            steps++;
                            locations.Add(new Location(currentPositionX, currentPositionY, steps));
                            /*if (i == instruction.steps - 1 && map[currentPositionX, currentPositionY] != '|' && map[currentPositionX, currentPositionY] != '-')
                            {
                                map[currentPositionX, currentPositionY] = '+';
                            }
                            else if (map[currentPositionX, currentPositionY] == '.')
                            {
                                map[currentPositionX, currentPositionY] = '|';
                            }
                            else
                            {
                                map[currentPositionX, currentPositionY] = 'X';
                            }*/
                        }
                        //currentPositionY += instruction.steps;
                        break;
                    case directions.LEFT:
                        for (int i = 0; i < instruction.steps; i++)
                        {
                            currentPositionX--;
                            steps++;
                            locations.Add(new Location(currentPositionX, currentPositionY,steps));
                            /*if (i == instruction.steps - 1 && map[currentPositionX, currentPositionY] != '|' && map[currentPositionX, currentPositionY] != '-')
                            {
                                map[currentPositionX, currentPositionY] = '+';
                            }
                            else if (map[currentPositionX, currentPositionY] == '.')
                            {
                                map[currentPositionX, currentPositionY] = '-';
                            }
                            else
                            {
                                map[currentPositionX, currentPositionY] = 'X';
                            }*/
                        }
                        break;
                    case directions.RIGHT:
                        for (int i = 0; i < instruction.steps; i++)
                        {
                            currentPositionX++;
                            steps++;
                            locations.Add(new Location(currentPositionX, currentPositionY,steps));
                            /*if (i == instruction.steps - 1 && map[currentPositionX, currentPositionY] != '|' && map[currentPositionX, currentPositionY] != '-')
                            {
                                map[currentPositionX, currentPositionY] = '+';
                            }
                            else if (map[currentPositionX, currentPositionY] == '.')
                            {
                                map[currentPositionX, currentPositionY] = '-';
                            }
                            else
                            {
                                map[currentPositionX, currentPositionY] = 'X';
                            }*/
                        }
                        break;
                    default:
                        break;
                }

            }

            //return map;
            return locations;
        }

        static char[,] PopulateMap(char[,] mapToPopulate, int originX, int originY)
        {
            for (int i = 0; i < mapToPopulate.GetLength(0); i++)
                for (int j = 0; j < mapToPopulate.GetLength(1); j++)
                    mapToPopulate[i, j] = '.';
            mapToPopulate[originX, originY] = 'O';
            return mapToPopulate;
        }

        static int GetMax(List<Instruction> instructionSet1, List<Instruction> instructionSet2, directions direction)
        {
            int totalStepsInDirection = 0;
            
            foreach(Instruction instruction in instructionSet1)
            {
                if (instruction.direction == direction)
                {
                    totalStepsInDirection += instruction.steps;
                }
            }

            foreach(Instruction instruction in instructionSet2)
            {
                if (instruction.direction == direction)
                {
                    totalStepsInDirection += instruction.steps;
                }
            }
            return totalStepsInDirection;
        }

        static int CompareDirections(List<Instruction> instructionSet1, List<Instruction> instructionSet2, directions direction, directions oppositeDirection)
        {
            int totalStepsInDirection1 = 0;
            int totalStepsInDirection2 = 0;

            foreach (Instruction instruction in instructionSet1)
            {
                if (instruction.direction == direction)
                {
                    totalStepsInDirection1 += instruction.steps;
                }
                else if(instruction.direction == oppositeDirection)
                {
                    totalStepsInDirection1 -= instruction.steps;
                }
            }

            foreach (Instruction instruction in instructionSet2)
            {
                if (instruction.direction == direction)
                {
                    totalStepsInDirection2 += instruction.steps;
                }
                else if(instruction.direction == oppositeDirection)
                {
                    totalStepsInDirection2 -= instruction.steps;
                }
            }

            if (totalStepsInDirection1 >= totalStepsInDirection2)
                return totalStepsInDirection1;
            else
                return totalStepsInDirection2;
        }

        static int GetWidth(List<Instruction> instructions)
        {
            int width = 0;
            foreach(Instruction instruction in instructions)
            {
                switch (instruction.direction)
                {
                    case directions.LEFT:
                        width -= instruction.steps;
                        break;
                    case directions.RIGHT:
                        width += instruction.steps;
                        break;
                    default:
                        break;
                }
            }
            return Math.Abs(width);
        }

        static int GetHeight(List<Instruction> instructions)
        {
            int height = 0;
            foreach(Instruction instruction in instructions)
            {
                switch (instruction.direction)
                {
                    case directions.UP:
                        height += instruction.steps;
                        break;
                    case directions.DOWN:
                        height -= instruction.steps;
                        break;
                    default:
                        break;
                }
            }
            return Math.Abs(height);
        }

        static string[] FileRead(string filePath)
        {
            //Letter -> Number -> ,
            //Letter -> Number -> Letter
            string text = File.ReadAllText(filePath);
            string[] textArray = new string[2];
            //text = text.Replace(",", "");
            int newLineLocation = text.IndexOf('\n');
            textArray[0] = text.Substring(0, newLineLocation);
            textArray[1] = text.Substring(newLineLocation, text.Length-textArray[0].Length);
            //Console.WriteLine(textArray[0]);
            //Console.WriteLine(textArray[1]);
            //bool isLetter = false, isNumber = false, isComma = false;
            //string number = "0";
            //for(int i = 0; i < text.Length; i++)
            //{    
                /*if (text[i] == ',')
                {
                    if (isNumber)
                        isComma = true;
                    isNumber = false;
                    isLetter = false;
                }
                else if (char.IsLetter(text[i]))
                {
                    if (int.Parse(number) == 83)
                        Console.WriteLine(i);
                    else
                        number = "";
                    if (isComma || !(isLetter && isNumber && isComma))
                        isLetter = true;
                    else
                        Console.WriteLine(i);
                    isNumber = false;
                    isComma = false;
                }
                else if (char.IsDigit(text[i]))
                {
                    number += text[i];
                    if (isLetter || isNumber)
                        isNumber = true;
                    isComma = false;
                    isLetter = false;
                }
                else if (text[i] == '\n')
                {
                    Console.WriteLine("NEW LINE AT: " + i);
                    Console.WriteLine(text.Substring(0, i));
                }*/
                
            //}
            
            textArray[0] = textArray[0].Replace("\n", "");
            textArray[1] = textArray[1].Replace("\n", "");
            textArray[0] = textArray[0].Replace(",", "");
            textArray[1] = textArray[1].Replace(",", "");
            return textArray;
        }

        static List<Instruction> MakeInstructions(string unformattedText)
        {
            List<Instruction> instructions = new List<Instruction>();
            string lengthOfSection = "";
            for(int i = 1; i < unformattedText.Length; i++)
            {
                if (char.IsLetter(unformattedText[i]))
                {
                    Instruction newInstruction = new Instruction(
                        GetDirection(unformattedText[i - lengthOfSection.Length - 1]),
                        int.Parse(lengthOfSection)
                    );
                    instructions.Add(newInstruction);
                    lengthOfSection = "";
                }
                else
                {
                    lengthOfSection += unformattedText[i];
                }
            }
            Instruction lastInstruction = new Instruction();
            lastInstruction.direction = GetDirection(unformattedText[unformattedText.Length - lengthOfSection.Length - 1]);
            lastInstruction.steps = int.Parse(lengthOfSection);
            instructions.Add(lastInstruction);
            return instructions;
        }

        public static directions GetDirection(char direction)
        {
            switch (direction)
            {
                case 'R':
                    return directions.RIGHT;
                case 'L':
                    return directions.LEFT;
                case 'U':
                    return directions.UP;
                case 'D':
                    return directions.DOWN;
                default:
                    return 0;
            }
        }

        public static int CalculateManhattanDistance(Location pos)
        {
            return Math.Abs(pos.X - 0) + Math.Abs(pos.Y - 0);
        }
    }
}
