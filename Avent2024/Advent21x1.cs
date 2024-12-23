using AdventOfCode.Utils;
using System.Numerics;

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
            var stringArr = FileReader.ReadFile("Advent21Example.txt").Select((str) => str[0]).ToList();

            BigInteger ans = 0;

            var finalLayers = new List<string>();
            foreach (var code in stringArr)
            {
                var controller1 = new Controller();
                var controller2 = new Controller();
                var keypad1 = new Keypad();

                var layer1 = "";
                var layer2 = "";
                var layer3 = "";

                for (int i = 0; i < code.Length; i++)
                {
                    var diff = keypad1.DiffBetweenCursorAndChar(code[i]);

                    var xMovesLeft = Math.Abs(diff.Item1);
                    var yMovesLeft = Math.Abs(diff.Item2);

                    while (xMovesLeft > 0 || yMovesLeft > 0)
                    {
                        
                        if (diff.Item1 < 0)
                        {
                            if (keypad1.Controller[keypad1.Cursor.Item2][keypad1.Cursor.Item1 - 1] != 'X')
                            {
                                layer1 += "<";
                                xMovesLeft--;
                            }
                        }
                        else
                        {
                            if (keypad1.Controller[keypad1.Cursor.Item2][keypad1.Cursor.Item1 + 1] != 'X')
                            {
                                layer1 += ">";
                                xMovesLeft--;
                            }
                        }
                        
                        if (diff.Item2 < 0)
                        {
                            if (keypad1.Controller[keypad1.Cursor.Item2 - 1][keypad1.Cursor.Item1] != 'X')
                            {
                                layer1 += "^";
                            }
                            yMovesLeft--;

                        }
                        else
                        {
                            if (keypad1.Controller[keypad1.Cursor.Item2 + 1][keypad1.Cursor.Item1] != 'X')
                            {
                                layer1 += "v";
                            }
                            yMovesLeft--;
                        }
                    }

                    layer1 += "A";
                    keypad1.Cursor = (keypad1.Cursor.Item1 + diff.Item1, keypad1.Cursor.Item2 + diff.Item2);
                }

                for (int i = 0; i < layer1.Length; i++)
                {
                    var diff = controller2.DiffBetweenCursorAndChar(layer1[i]);
                    var xMovesLeft = Math.Abs(diff.Item1);
                    var yMovesLeft = Math.Abs(diff.Item2);

                    while (xMovesLeft > 0 || yMovesLeft > 0)
                    {
                        if (diff.Item1 < 0)
                        {
                            if (controller2.Keypad[controller2.Cursor.Item2][controller2.Cursor.Item1 - 1] != 'X')
                            {
                                layer1 += "<";
                                xMovesLeft--;
                            }
                        }
                        else
                        {
                            if (controller2.Keypad[controller2.Cursor.Item2][controller2.Cursor.Item1 + 1] != 'X')
                            {
                                layer1 += ">";
                                xMovesLeft--;
                            }
                        }

                        if (diff.Item2 < 0)
                        {
                            if (controller2.Keypad[controller2.Cursor.Item2 - 1][controller2.Cursor.Item1] != 'X')
                            {
                                layer1 += "^";
                            }
                            yMovesLeft--;

                        }
                        else
                        {
                            if (controller2.Keypad[controller2.Cursor.Item2 + 1][controller2.Cursor.Item1] != 'X')
                            {
                                layer1 += "v";
                            }
                            yMovesLeft--;
                        }
                    }
                    layer2 += "A";
                    controller2.Cursor = (controller2.Cursor.Item1 + diff.Item1, controller2.Cursor.Item2 + diff.Item2);
                }

                for (int i = 0; i < layer2.Length; i++)
                {
                    var diff = controller1.DiffBetweenCursorAndChar(layer2[i]);
                    var xMovesLeft = Math.Abs(diff.Item1);
                    var yMovesLeft = Math.Abs(diff.Item2);

                    while (xMovesLeft > 0 || yMovesLeft > 0)
                    {
                        if (diff.Item1 < 0)
                        {
                            if (controller1.Keypad[controller1.Cursor.Item2][controller1.Cursor.Item1 - 1] != 'X')
                            {
                                layer1 += "<";
                                xMovesLeft--;
                            }
                        }
                        else
                        {
                            if (controller1.Keypad[controller1.Cursor.Item2][controller1.Cursor.Item1 + 1] != 'X')
                            {
                                layer1 += ">";
                                xMovesLeft--;
                            }
                        }

                        if (diff.Item2 < 0)
                        {
                            if (controller1.Keypad[controller1.Cursor.Item2 - 1][controller1.Cursor.Item1] != 'X')
                            {
                                layer1 += "^";
                            }
                            yMovesLeft--;

                        }
                        else
                        {
                            if (controller1.Keypad[controller1.Cursor.Item2 + 1][controller1.Cursor.Item1] != 'X')
                            {
                                layer1 += "v";
                            }
                            yMovesLeft--;
                        }
                    }
                    layer3 += "A";
                    controller1.Cursor = (controller1.Cursor.Item1 + diff.Item1, controller1.Cursor.Item2 + diff.Item2);
                }

                finalLayers.Add(layer3);
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

            for (int i = 0; i < finalLayers.Count; i++)
            {
                Console.WriteLine(finalLayers[i].Length);
            }

            return ans.ToString();
        }
    }
}
