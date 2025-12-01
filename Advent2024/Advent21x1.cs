using AdventOfCode.Utils;
using System.Numerics;
using System.Xml.Linq;

namespace AdventOfCode.Advent2024
{
    internal class Advent21x1
    {
        public class Controller
        {
            public (int, int) Cursor { get; set; } = (2, 0);

            public void MoveCursor(char direction)
            {
                switch (direction)
                {
                    case '^':
                        Cursor = (Cursor.Item1, Cursor.Item2 - 1);
                        break;
                    case 'v':
                        Cursor = (Cursor.Item1, Cursor.Item2 + 1);
                        break;
                    case '<':
                        Cursor = (Cursor.Item1 - 1, Cursor.Item2);
                        break;
                    case '>':
                        Cursor = (Cursor.Item1 + 1, Cursor.Item2);
                        break;
                }
            }

            public char Activate()
            {
                return Keypad[Cursor.Item2][Cursor.Item1];
            }

            public char[][] Keypad { get; set; } = new char[][]
            {
                new char[] {'X', '^', 'A'},
                new char[] {'<', 'v', '>'},
            };

            public (int, int) DiffBetweenCursorAndChar(char c)
            {
                for (int i = 0; i < Keypad.Length; i++)
                {
                    for (int j = 0; j < Keypad[i].Length; j++)
                    {
                        if (Keypad[i][j] == c)
                        {
                            return (j - Cursor.Item1, i - Cursor.Item2);
                        }
                    }
                }
                return (0, 0);
            }

            public void Print()
            {
                for (int i = 0; i < Keypad.Length; i++)
                {
                    for (int j = 0; j < Keypad[i].Length; j++)
                    {
                        if (Cursor.Item1 == j && Cursor.Item2 == i)
                        {
                            Console.Write('Q');
                        }
                        else
                        {
                            Console.Write(Keypad[i][j]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        public class Keypad
        {
            public (int, int) Cursor { get; set; } = (2, 3);

            public void Activate()
            {
                Console.Write(Controller[Cursor.Item2][Cursor.Item1]);
            }

            public void MoveCursor(char direction)
            {
                switch (direction)
                {
                    case '^':
                        Cursor = (Cursor.Item1, Cursor.Item2 - 1);
                        break;
                    case 'v':
                        Cursor = (Cursor.Item1, Cursor.Item2 + 1);
                        break;
                    case '<':
                        Cursor = (Cursor.Item1 - 1, Cursor.Item2);
                        break;
                    case '>':
                        Cursor = (Cursor.Item1 + 1, Cursor.Item2);
                        break;
                }
            }

            public char[][] Controller { get; set; } = new char[][]
            {
                new char[] {'7', '8', '9'},
                new char[] {'4', '5', '6'},
                new char[] {'1', '2', '3'},
                new char[] {'X', '0', 'A'},
            };

            public (int, int) DiffBetweenCursorAndChar(char c)
            {
                for (int i = 0; i < Controller.Length; i++)
                {
                    for (int j = 0; j < Controller[i].Length; j++)
                    {
                        if (Controller[i][j] == c)
                        {
                            return (j - Cursor.Item1, i - Cursor.Item2);
                        }
                    }
                }
                return (0, 0);
            }

            public void Print()
            {
                for (int i = 0; i < Controller.Length; i++) {
                    for (int j = 0; j < Controller[i].Length; j++)
                    {
                        if (Cursor.Item1 == j && Cursor.Item2 == i)
                        {
                            Console.Write('Q');
                        }
                        else
                        {
                            Console.Write(Controller[i][j]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        public static string Run()
        {
            var stringArr = FileReader.ReadFile("Advent21.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var finalLayers = new List<string>();
            foreach (var code in stringArr)
            {
                var controller1 = new Controller();
                var controller2 = new Controller();
                var keypad1 = new Keypad();

                var minLayer1 = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
                var minLayer2 = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
                var minLayer3 = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";


              

                var rand = new Random(1290);

                for (int k = 0; k < 3000; k++)
                {
                    var layer1 = "";
                    for (int i = 0; i < code.Length; i++)
                    {
                        var bogus = false;
                        var diff = keypad1.DiffBetweenCursorAndChar(code[i]);

                        var xMovesLeft = Math.Abs(diff.Item1);
                        var yMovesLeft = Math.Abs(diff.Item2);

                        var xMove = diff.Item1 > 0 ? '>' : '<';
                        var yMove = diff.Item2 > 0 ? 'v' : '^';

                        var initCursor = keypad1.Cursor;

                        var templayer1 = "";
                        // randomize the order of the moves
                        var order = new List<char>();

                        for (int j = 0; j < xMovesLeft; j++)
                        {
                            order.Add(xMove);
                        }
                        for (int j = 0; j < yMovesLeft; j++)
                        {
                            order.Add(yMove);
                        }
                        order = order.OrderBy(_ => rand.Next()).ToList();

                        templayer1 += order.Aggregate("", (agg, dir) => agg += dir);

                        foreach (var c in templayer1)
                        {
                            var tempDiff = (0, 0);
                            switch (c)
                            {
                                case '^':
                                    tempDiff = (0, -1);
                                    break;
                                case 'v':
                                    tempDiff = (0, 1);
                                    break;
                                case '<':
                                    tempDiff = (-1, 0);
                                    break;
                                case '>':
                                    tempDiff = (1, 0);
                                    break;
                            }

                            keypad1.Cursor = (keypad1.Cursor.Item1 + tempDiff.Item1, keypad1.Cursor.Item2 + tempDiff.Item2);
                            if (keypad1.Controller[keypad1.Cursor.Item2][keypad1.Cursor.Item1] == 'X')
                            {
                                bogus = true;
                            }
                        }

                        if (!bogus)
                        {
                            layer1 += templayer1;
                            layer1 += "A";
                        }
                        else
                        {
                            i--;
                            keypad1.Cursor = initCursor;
                        }
                    }

                    var layer2 = "";
                    for (int i = 0; i < layer1.Length; i++)
                    {
                        var bogus = false;
                        var tempLayer2 = "";
                        var diff = controller2.DiffBetweenCursorAndChar(layer1[i]);
                        var xMovesLeft = Math.Abs(diff.Item1);
                        var yMovesLeft = Math.Abs(diff.Item2);
                        var initCursor = controller2.Cursor;

                        var xMove = diff.Item1 > 0 ? '>' : '<';
                        var yMove = diff.Item2 > 0 ? 'v' : '^';

                        // randomize the order of the moves
                        var order = new List<char>();

                        for (int j = 0; j < xMovesLeft; j++)
                        {
                            order.Add(xMove);
                        }
                        for (int j = 0; j < yMovesLeft; j++)
                        {
                            order.Add(yMove);
                        }
                        order = order.OrderBy(_ => rand.Next()).ToList();

                        tempLayer2 += order.Aggregate("", (agg, dir) => agg += dir);

                        foreach (var c in tempLayer2)
                        {
                            var tempDiff = (0, 0);
                            switch (c)
                            {
                                case '^':
                                    tempDiff = (0, -1);
                                    break;
                                case 'v':
                                    tempDiff = (0, 1);
                                    break;
                                case '<':
                                    tempDiff = (-1, 0);
                                    break;
                                case '>':
                                    tempDiff = (1, 0);
                                    break;
                            }

                            controller2.Cursor = (controller2.Cursor.Item1 + tempDiff.Item1, controller2.Cursor.Item2 + tempDiff.Item2);
                            if (controller2.Keypad[controller2.Cursor.Item2][controller2.Cursor.Item1] == 'X')
                            {
                                bogus = true;
                            }
                                    
                        }

                        if (!bogus)
                        {
                            layer2 += tempLayer2;
                            layer2 += "A";
                        }
                        else
                        {
                            i--;
                            controller2.Cursor = initCursor;
                        }
                    }
                    
                    var layer3 = "";
                    for (int i = 0; i < layer2.Length; i++)
                    {
                        var bogus = false;
                        var diff = controller1.DiffBetweenCursorAndChar(layer2[i]);
                        var xMovesLeft = Math.Abs(diff.Item1);
                        var yMovesLeft = Math.Abs(diff.Item2);
                        var initCursor = controller1.Cursor;

                        var xMove = diff.Item1 > 0 ? '>' : '<';
                        var yMove = diff.Item2 > 0 ? 'v' : '^';

                        var tempLayer3 = "";

                        // randomize the order of the moves
                        var order = new List<char>();

                        for (int j = 0; j < xMovesLeft; j++)
                        {
                            order.Add(xMove);
                        }
                        for (int j = 0; j < yMovesLeft; j++)
                        {
                            order.Add(yMove);
                        }
                        order = order.OrderBy(_ => rand.Next()).ToList();

                        tempLayer3 += order.Aggregate("", (agg, dir) => agg += dir);

                        foreach (var c in tempLayer3)
                        {
                            var tempDiff = (0, 0);
                            switch (c)
                            {
                                case '^':
                                    tempDiff = (0, -1);
                                    break;
                                case 'v':
                                    tempDiff = (0, 1);
                                    break;
                                case '<':
                                    tempDiff = (-1, 0);
                                    break;
                                case '>':
                                    tempDiff = (1, 0);
                                    break;
                            }

                            controller1.Cursor = (controller1.Cursor.Item1 + tempDiff.Item1, controller1.Cursor.Item2 + tempDiff.Item2);
                            if (controller1.Keypad[controller1.Cursor.Item2][controller1.Cursor.Item1] == 'X')
                            {
                                bogus = true;
                            }

                        }
                        if (!bogus)
                        {
                            layer3 += tempLayer3;
                            layer3 += "A";
                        }
                        else
                        {
                            i--;
                            controller1.Cursor = initCursor;
                        }
                    }

                    if (layer3.Length < minLayer3.Length)
                    {
                        minLayer1 = layer1;
                        minLayer2 = layer2;
                        minLayer3 = layer3;
                    }

                }

                Console.WriteLine(minLayer1);
                Console.WriteLine(minLayer2);
                Console.WriteLine(minLayer3);
                finalLayers.Add(minLayer3);
            }


            foreach (var layer in finalLayers)
            {
                var controller1 = new Controller();
                var controller2 = new Controller();
                var keypad1 = new Keypad();

                foreach (var c in layer)
                {
                    if (c == 'A')
                    {
                        var result = controller1.Activate();

                        if (result == 'A')
                        {
                            var result2 = controller2.Activate();
                            if (result2 == 'A')
                            {
                                keypad1.Activate();
                            }
                            else
                            {
                                keypad1.MoveCursor(result2);
                            }
                        }
                        else
                        {
                            controller2.MoveCursor(result);
                        }
                    }
                    else
                    {
                        controller1.MoveCursor(c);
                    }

                }
                Console.WriteLine();
            }

            var nums = new List<int>();
            nums.Add(803);
            nums.Add(528);
            nums.Add(586);
            nums.Add(341);
            nums.Add(319);
            for (int i = 0; i < finalLayers.Count; i++)
            {
                Console.WriteLine(finalLayers[i].Length);
                ans += nums[i] * finalLayers[i].Length;
            }

            return ans.ToString();
        }
    }
}
