using AdventOfCode.Utils;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace AdventOfCode.Advent2024
{
    internal class Advent21x2
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
                for (int i = 0; i < Controller.Length; i++)
                {
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

            var numberLayers = 11;

            var finalLayers = new List<string>();
            var memo = new Dictionary<((int, int), (int, int)), string>();
            foreach (var code in stringArr)
            {
                var minLayers = new string[numberLayers];

                var controllers = new List<Controller>();
                for (int i = 0; i < numberLayers-1; i++)
                {
                    controllers.Add(new Controller());
                }

                var layers = new string[numberLayers];

                var keypad1 = new Keypad();

                var rand = new Random(1290);

                for (int k = 0; k < 30; k++)
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

                        var tempLayer1 = "";
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

                        tempLayer1 += order.Aggregate("", (agg, dir) => agg += dir);

                        foreach (var c in tempLayer1)
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
                            layer1 += tempLayer1;
                            layer1 += "A";
                        }
                        else
                        {
                            i--;
                            keypad1.Cursor = initCursor;
                        }
                    }
                    layers[0] = layer1;


                    var layerCount = 0;
                    foreach (var controller in controllers)
                    {
                        Console.WriteLine($"Layer: {layerCount}");
                        layerCount++;
                        var previousLayer = layers[controllers.IndexOf(controller)];
                        var myLayer = new StringBuilder();
                        var tempSb = new StringBuilder();
                        for (int i = 0; i < previousLayer.Length; i++)
                        {

                            var bogus = false;
                            var tempLayer = "";
                            var diff = controller.DiffBetweenCursorAndChar(previousLayer[i]);
                            var xMovesLeft = Math.Abs(diff.Item1);
                            var yMovesLeft = Math.Abs(diff.Item2);
                            var initCursor = controller.Cursor;

                            //if (memo.TryGetValue((controller.Cursor, diff), out string val))
                            //{
                            //    myLayer.Append(val);
                            //    myLayer.Append('A');
                            //    continue;
                            //}

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

                            tempSb = new StringBuilder();
                            foreach (var item in order)
                            {
                                tempSb.Append(item);
                            }
                            tempLayer = tempSb.ToString();
                            tempSb.Clear();

                            foreach (var c in tempLayer)
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

                                controller.Cursor = (controller.Cursor.Item1 + tempDiff.Item1, controller.Cursor.Item2 + tempDiff.Item2);
                                if (controller.Keypad[controller.Cursor.Item2][controller.Cursor.Item1] == 'X')
                                {
                                    bogus = true;
                                }
                            }

                            if (!bogus)
                            {
                                myLayer.Append(tempLayer);
                                myLayer.Append('A');
                                if (memo.ContainsKey((initCursor, diff)))
                                {
                                    if (memo[(initCursor, diff)].Length > myLayer.Length)
                                    {
                                    }
                                }
                                else
                                {
                                    memo[(initCursor, diff)] = tempLayer;
                                }
                            }
                            else
                            {
                                i--;
                                controller.Cursor = initCursor;
                            }
                        }
                        layers[controllers.IndexOf(controller) + 1] = myLayer.ToString();
                        myLayer.Clear();
                    }

                    if (minLayers[numberLayers-1] == null || layers[numberLayers-1].Length < minLayers[numberLayers-1].Length)
                    {
                        Array.Copy(layers, minLayers, numberLayers);

                        foreach (var str in minLayers)
                        {
                            Console.WriteLine(str.Length);
                        }
                    }
                }

                finalLayers.Add(minLayers[numberLayers-1]);
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
