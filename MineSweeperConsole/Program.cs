using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Minesweeper
{
    class Program
    {
        static void Setting(out int width, out int height)
        {
            string line;
            bool isValid;
            //Choosing dimensions
            Console.Write("\nEnter width from "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write("5 "); Console.ResetColor(); Console.Write("to "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("36"); Console.ResetColor(); Console.Write(" only:");
            line = Console.ReadLine();
            isValid = int.TryParse(line, out width);
            while (!isValid || width > 36 || width < 5)
            {
                Console.Write("\nPlease, enter numbers from "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write("5 "); Console.ResetColor(); Console.Write("to "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("36"); Console.ResetColor(); Console.Write(" only!");
                Console.WriteLine("\nReenter width:");
                line = Console.ReadLine();
                isValid = int.TryParse(line, out width);
            }
            Console.Write("\nEnter height from "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write("5 "); Console.ResetColor(); Console.Write("to "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("10"); Console.ResetColor(); Console.Write(" only:");
            line = Console.ReadLine();
            isValid = int.TryParse(line, out height);
            while (!isValid || height > 10 || height < 5)
            {
                Console.Write("\nPlease, enter numbers from "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write("5 "); Console.ResetColor(); Console.Write("to "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("10"); Console.ResetColor(); Console.Write(" only!");
                Console.WriteLine("\nReenter height:");
                line = Console.ReadLine();
                isValid = int.TryParse(line, out height);
            }
            Console.Clear();
        }
        static int[,,] BombPreset(int[,,] Grid)
        {
            Random rnd = new Random();
            int ydimSpot, xdimSpot, zdimSpot;
            //setting mines
            for (int MineSpread = 0; MineSpread < (Grid.GetLength(0) * Grid.GetLength(1)) / 3; MineSpread++)
            {
                ydimSpot = rnd.Next(0, Grid.GetLength(0));
                xdimSpot = rnd.Next(0, Grid.GetLength(1));
                zdimSpot = 0;
                Grid[ydimSpot, xdimSpot, zdimSpot] = 9;
            }
            //setting clues
            for (int ydim = 0; ydim < Grid.GetLength(0); ydim++)
            {
                for (int xdim = 0; xdim < Grid.GetLength(1); xdim++)
                {
                    for (int zdim = 0; zdim < 1; zdim++)
                    {
                        //inner zone
                        if (ydim >= 1 && ydim < (Grid.GetLength(0) - 1) && xdim >= 1 && xdim < (Grid.GetLength(1) - 1) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent squares
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim + 1, zdim] != 9) { Grid[ydim + 1, xdim + 1, zdim]++; }
                            if (Grid[ydim + 1, xdim - 1, zdim] != 9) { Grid[ydim + 1, xdim - 1, zdim]++; }
                            if (Grid[ydim - 1, xdim + 1, zdim] != 9) { Grid[ydim - 1, xdim + 1, zdim]++; }
                            if (Grid[ydim - 1, xdim - 1, zdim] != 9) { Grid[ydim - 1, xdim - 1, zdim]++; }
                        }
                        //LUCorner
                        if (ydim == 0 && xdim == 0 && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim + 1, zdim] != 9) { Grid[ydim + 1, xdim + 1, zdim]++; }
                        }
                        //RUCorner
                        if (ydim == 0 && xdim == (Grid.GetLength(1) - 1) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim - 1, zdim] != 9) { Grid[ydim + 1, xdim - 1, zdim]++; }
                        }
                        //LDCorner
                        if (ydim == (Grid.GetLength(0) - 1) && xdim == 0 && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            //diagonals
                            if (Grid[ydim - 1, xdim + 1, zdim] != 9) { Grid[ydim - 1, xdim + 1, zdim]++; }
                        }
                        //RDCorner
                        if (ydim == (Grid.GetLength(0) - 1) && xdim == (Grid.GetLength(1) - 1) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            //diagonals
                            if (Grid[ydim - 1, xdim - 1, zdim] != 9) { Grid[ydim - 1, xdim - 1, zdim]++; }
                        }
                        //LColumn
                        if ((ydim != 0) && (xdim == 0) && (ydim != (Grid.GetLength(0) - 1)) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim + 1, zdim] != 9) { Grid[ydim + 1, xdim + 1, zdim]++; }
                            if (Grid[ydim - 1, xdim + 1, zdim] != 9) { Grid[ydim - 1, xdim + 1, zdim]++; }
                        }
                        //URow
                        if ((ydim == 0) && (xdim != 0) && (xdim != (Grid.GetLength(1) - 1)) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim + 1, zdim] != 9) { Grid[ydim + 1, xdim + 1, zdim]++; }
                            if (Grid[ydim + 1, xdim - 1, zdim] != 9) { Grid[ydim + 1, xdim - 1, zdim]++; }
                        }
                        //RColumn
                        if ((ydim != 0) && (ydim != (Grid.GetLength(0) - 1)) && (xdim == (Grid.GetLength(1) - 1)) && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim + 1, xdim, zdim] != 9) { Grid[ydim + 1, xdim, zdim]++; }
                            //diagonals
                            if (Grid[ydim + 1, xdim - 1, zdim] != 9) { Grid[ydim + 1, xdim - 1, zdim]++; }
                            if (Grid[ydim - 1, xdim - 1, zdim] != 9) { Grid[ydim - 1, xdim - 1, zdim]++; }
                        }
                        //DRow
                        if (ydim == (Grid.GetLength(0) - 1) && xdim < (Grid.GetLength(1) - 1) && xdim != 0 && Grid[ydim, xdim, zdim] == 9)
                        {
                            //adjacent
                            if (Grid[ydim - 1, xdim, zdim] != 9) { Grid[ydim - 1, xdim, zdim]++; }
                            if (Grid[ydim, xdim - 1, zdim] != 9) { Grid[ydim, xdim - 1, zdim]++; }
                            if (Grid[ydim, xdim + 1, zdim] != 9) { Grid[ydim, xdim + 1, zdim]++; }
                            //diagonals
                            if (Grid[ydim - 1, xdim + 1, zdim] != 9) { Grid[ydim - 1, xdim + 1, zdim]++; }
                            if (Grid[ydim - 1, xdim - 1, zdim] != 9) { Grid[ydim - 1, xdim - 1, zdim]++; }
                        }
                    }
                }
            }
            return Grid;
        }
        static int[,,] CursorDraw(int[,,] Grid, int ycurs, int xcurs)
        {
            //resetting cursor stance
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Grid[i, j, 2] = 0;
                }
            }
            //setting cursor
            Grid[ycurs, xcurs, 2] = 1;
            return Grid;
        }
        static int[,,] FlagDraw(int[,,] Grid, int yclk, int xclk)
        {
            //setting flag
            if (Grid[yclk, xclk, 1] == 0 && Grid[yclk, xclk, 3] != 1)
            {
                Grid[yclk, xclk, 3] = 1;
            }
            else if (Grid[yclk, xclk, 1] == 0 && Grid[yclk, xclk, 3] == 1)
            {
                Grid[yclk, xclk, 3] = 0;
            }
            else Console.WriteLine("There is no point in marking the empty field!");
            return Grid;
        }
        static char[,,] Table(int height, int width, int depth)
        {
            int Fheight = height * 2 + 1; //Odd (Even from 0)
            int Fwidth = width * 2 + 1; //quantity of table elements: ever Odd (Even from 0)
            int Fdepth = 4; //cell stances: 0 - closed, 1 - opened, 2 - under cursor, 3 - flagged
            char[,,] TableFrames = new char[Fheight, Fwidth, Fdepth];
            //Field array assignment
            for (int i = 0; i < TableFrames.GetLength(0); i++)
            {
                for (int j = 0; j < TableFrames.GetLength(1); j++)
                {
                    if (i == 0 && j == 0) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u250C'; } //LUCorner
                    if (i % 2 == 0 && j % 2 == 1) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2500'; } //HorizontalBorders
                    if (i == 0 && j != 0 && j % 2 == 0 && j != (TableFrames.GetLength(1) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u252C'; } //UpperMidT
                    if (i == 0 && j == (TableFrames.GetLength(1) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2510'; } //RUCorner
                    if (i % 2 == 1 && j % 2 == 0) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2502'; } //VerticalBorders
                    if (i % 2 == 0 && i != 0 && j == 0 && i != (TableFrames.GetLength(0) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u251C'; } //LeftMidT
                    if (i % 2 == 0 && i != 0 && j != 0 && j % 2 == 0 && j != (TableFrames.GetLength(1) - 1) && i != (TableFrames.GetLength(0) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u253C'; } //MidX-Crossings
                    if (i % 2 == 0 && i != 0 && j == (TableFrames.GetLength(1) - 1) && i != (TableFrames.GetLength(0) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2524'; } //RightMidT
                    if (i == (TableFrames.GetLength(0) - 1) && j == 0) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2514'; } //LDCorner
                    if (i == (TableFrames.GetLength(0) - 1) && j != 0 && j % 2 == 0 && j != (TableFrames.GetLength(1) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2534'; } //DownMidT
                    if (i == (TableFrames.GetLength(0) - 1) && j == (TableFrames.GetLength(1) - 1)) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = '\u2518'; } //RDCorner
                    if (i % 2 == 1 && j % 2 == 1) { TableFrames[i, j, 0] = TableFrames[i, j, 1] = ' '; } //Cells
                }
            }
            return TableFrames;
            /*char[,] tabexample = new char[,]
        {
            {'\u250C', '\u2500', '\u252C', '\u2500', '\u2510'},
            {'\u2502', ' ', '\u2502', ' ', '\u2502'},
            {'\u251C', '\u2500', '\u253C', '\u2500', '\u2524'},
            {'\u2502', ' ', '\u2502', ' ', '\u2502'},
            {'\u2514', '\u2500', '\u2534', '\u2500', '\u2518'}
        };*/
        }
        //opening
        static int[,,] Opener(int[,,] Grid, int yclk, int xclk, out bool inGame, out bool lost, out bool won)
        {
            //surround area
            inGame = true; lost = false; won = false;
            if (Grid[yclk, xclk, 1] == 0)
            {
                Grid[yclk, xclk, 1] = Grid[yclk, xclk, 0];
                if (Grid[yclk, xclk, 1] == 9) { Console.WriteLine("You've lost!"); inGame = false; lost = true; } //mine
                else if (Grid[yclk, xclk, 3] != 0) { Console.WriteLine("Are you sure, you want to step on a marked field?"); } //on flag
                else if (yclk != (Grid.GetLength(0) - 1) && xclk != (Grid.GetLength(1) - 1) && Grid[yclk + 1, xclk + 1, 0] != 9)
                {//DR
                    Grid[yclk + 1, xclk + 1, 1] = Grid[yclk + 1, xclk + 1, 0];
                    if (Grid[yclk + 1, xclk + 1, 1] == 0 && Grid[yclk + 2, xclk + 2, 1] != 9 && yclk + 1 != (Grid.GetLength(0) - 1) && xclk + 1 != (Grid.GetLength(1) - 1)) { Grid[yclk + 2, xclk + 2, 1] = Grid[yclk + 2, xclk + 2, 0]; }
                }
                else if (yclk != 0 && xclk != 0 && Grid[yclk - 1, xclk - 1, 0] != 9)
                {//UL
                    Grid[yclk - 1, xclk - 1, 1] = Grid[yclk - 1, xclk - 1, 0];
                    if (yclk - 1 != 0 && xclk - 1 != 0 && Grid[yclk - 1, xclk - 1, 1] == 0 && Grid[yclk - 2, xclk - 2, 1] != 9) { Grid[yclk - 2, xclk - 2, 1] = Grid[yclk - 2, xclk - 2, 0]; }
                }
                else if (yclk != (Grid.GetLength(0) - 1) && xclk != 0 && Grid[yclk + 1, xclk - 1, 0] != 9)
                {//DL
                    Grid[yclk + 1, xclk - 1, 1] = Grid[yclk + 1, xclk - 1, 0];
                    if (yclk + 1 != (Grid.GetLength(0) - 1) && xclk - 1 != 0 && Grid[yclk + 1, xclk - 1, 1] == 0 && Grid[yclk + 2, xclk - 2, 1] != 9) { Grid[yclk + 2, xclk - 2, 1] = Grid[yclk + 2, xclk - 2, 0]; }
                }
                else if (yclk != 0 && Grid[yclk - 1, xclk + 1, 0] != 9 && xclk != (Grid.GetLength(1) - 1))
                {//UR
                    Grid[yclk - 1, xclk + 1, 1] = Grid[yclk - 1, xclk + 1, 0];
                    if (Grid[yclk - 1, xclk + 1, 1] == 0 && Grid[yclk - 2, xclk + 2, 1] != 9 && yclk - 1 != 0 && xclk + 1 != (Grid.GetLength(1) - 1)) { Grid[yclk - 2, xclk + 2, 1] = Grid[yclk - 2, xclk + 2, 0]; }
                }
                else if (yclk != 0 && Grid[yclk - 1, xclk, 0] != 9)
                {//U
                    Grid[yclk - 1, xclk, 1] = Grid[yclk - 1, xclk, 0];
                    if (yclk - 1 != 0 && Grid[yclk - 1, xclk, 1] == 0 && Grid[yclk - 2, xclk, 1] != 9) { Grid[yclk - 2, xclk, 1] = Grid[yclk - 2, xclk, 0]; }
                }
                else if (yclk != (Grid.GetLength(0) - 1) && Grid[yclk + 1, xclk, 0] != 9)
                {//D
                    Grid[yclk + 1, xclk, 1] = Grid[yclk + 1, xclk, 0];
                    if (Grid[yclk + 1, xclk, 1] == 0 && Grid[yclk + 2, xclk, 1] != 9 && yclk + 1 != (Grid.GetLength(0) - 1)) { Grid[yclk + 2, xclk, 1] = Grid[yclk + 2, xclk, 0]; }
                }
                else if (xclk != 0 && Grid[yclk, xclk - 1, 0] != 9)
                {//L
                    Grid[yclk, xclk - 1, 1] = Grid[yclk, xclk - 1, 0];
                    if (xclk - 1 != 0 && Grid[yclk, xclk - 1, 1] == 0 && Grid[yclk, xclk - 2, 1] != 9) { Grid[yclk, xclk - 2, 1] = Grid[yclk, xclk - 2, 0]; }
                }
                else if (xclk != (Grid.GetLength(1) - 1) && Grid[yclk, xclk + 1, 0] != 9)
                {//R
                    Grid[yclk, xclk + 1, 1] = Grid[yclk, xclk + 1, 0];
                    if (xclk + 1 != (Grid.GetLength(1) - 1) && Grid[yclk, xclk + 1, 1] == 0 && Grid[yclk, xclk + 2, 1] != 9) { Grid[yclk, xclk + 2, 1] = Grid[yclk, xclk + 2, 0]; }
                }
            }
            return Grid;
        }
        //Merge Arrays of opened Mines and Table
        static char[,,] TableArrayMerge(int[,,] Grid, char[,,] TableFramez)
        {
            int k = 0, l = 0;
            for (int i = 0; i < TableFramez.GetLength(0); i++)
            {
                for (int j = 0; j < TableFramez.GetLength(1); j++)
                {
                    if (i % 2 == 1 && j % 2 == 1 && k == (Grid.GetLength(0) - 1) && l == (Grid.GetLength(1) - 1))
                    {
                        TableFramez[i, j, 1] = (char)(Grid[k, l, 1] + 48);
                        break;
                    }
                    if (i % 2 == 1 && j % 2 == 1 && k < Grid.GetLength(0) && l < Grid.GetLength(1) && l != (Grid.GetLength(1) - 1))
                    {
                        TableFramez[i, j, 1] = (char)(Grid[k, l, 1] + 48);
                        l++;
                    }
                    if (i % 2 == 1 && j % 2 == 1 && k < Grid.GetLength(0) && k != (Grid.GetLength(0) - 1) && l == (Grid.GetLength(1) - 1))
                    {
                        if (j != TableFramez.GetLength(1) - 1) { j += 2; }
                        if (j == TableFramez.GetLength(1) - 1) { i += 2; j = 1; }
                        TableFramez[i, j, 1] = (char)(Grid[k, l, 1] + 48);
                        l = 0;
                        k++;
                    }
                }
            }
            int m = 0, n = 0;
            for (int i = 1; i < TableFramez.GetLength(0); i += 2)
            {
                for (int j = 1; j < TableFramez.GetLength(1); j += 2)
                {
                    if (i % 2 == 1 && j % 2 == 1 && m == (Grid.GetLength(0) - 1) && n == (Grid.GetLength(1) - 1))
                    {
                        //cursor
                        if (Grid[m, n, 2] == 1)
                        {
                            TableFramez[i, j, 2] = (char)(1 + 48);
                        }
                        //flag
                        if (Grid[m, n, 3] == 1)
                        {
                            TableFramez[i, j, 3] = (char)(1 + 48);
                            TableFramez[i, j, 1] = '\u0078'; //'x'
                        }
                        break;
                    }
                    else if (i % 2 == 1 && j % 2 == 1 && m < Grid.GetLength(0) && n < (Grid.GetLength(1) - 1))
                    {
                        //cursor
                        if (Grid[m, n, 2] == 1)
                        {
                            TableFramez[i, j, 2] = (char)(1 + 48);
                        }
                        //flag
                        if (Grid[m, n, 3] == 1)
                        {
                            TableFramez[i, j, 3] = (char)(1 + 48);
                            TableFramez[i, j, 1] = '\u0078'; //'x'
                        }
                        n++;
                    }
                    else if (n == (Grid.GetLength(1) - 1) && m < (Grid.GetLength(0) - 1) && i % 2 == 1 && j % 2 == 1)
                    {
                        //cursor
                        if (Grid[m, n, 2] == 1)
                        {
                            TableFramez[i, j, 2] = (char)(1 + 48);
                        }
                        //flag
                        if (Grid[m, n, 3] == 1)
                        {
                            TableFramez[i, j, 3] = (char)(1 + 48);
                            TableFramez[i, j, 1] = '\u0078'; //'x'
                        }
                        n = 0;
                        m++;
                    }
                }
            }
            return TableFramez;
        }
        //Draw colored Table
        static void FullDraw(char[,,] Field)
        {
            Dictionary<int, KeyValuePair<string, ConsoleColor>> ColoredClues = new Dictionary<int, KeyValuePair<string, ConsoleColor>>();
            ColoredClues.Add(1, new KeyValuePair<string, ConsoleColor>(".", ConsoleColor.DarkYellow));
            ColoredClues.Add(2, new KeyValuePair<string, ConsoleColor>("?", ConsoleColor.DarkGray));
            ColoredClues.Add(3, new KeyValuePair<string, ConsoleColor>("\u00A4", ConsoleColor.DarkRed));
            ColoredClues.Add(4, new KeyValuePair<string, ConsoleColor>("8", ConsoleColor.Red));
            ColoredClues.Add(5, new KeyValuePair<string, ConsoleColor>("7", ConsoleColor.DarkMagenta));
            ColoredClues.Add(6, new KeyValuePair<string, ConsoleColor>("6", ConsoleColor.Magenta));
            ColoredClues.Add(7, new KeyValuePair<string, ConsoleColor>("5", ConsoleColor.Yellow));
            ColoredClues.Add(8, new KeyValuePair<string, ConsoleColor>("4", ConsoleColor.Green));
            ColoredClues.Add(9, new KeyValuePair<string, ConsoleColor>("3", ConsoleColor.Cyan));
            ColoredClues.Add(10, new KeyValuePair<string, ConsoleColor>("2", ConsoleColor.Blue));
            ColoredClues.Add(11, new KeyValuePair<string, ConsoleColor>("1", ConsoleColor.DarkBlue));
            ColoredClues.Add(12, new KeyValuePair<string, ConsoleColor>("\u00B6", ConsoleColor.White));
            //assigning clues' params to dictionary
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    //no cursor
                    if (Field[i, j, 2] == '0' || Field[i, j, 2] == '\0')
                    {
                        switch (Field[i, j, 1])
                        {
                            case '1':
                                ConsoleColor color = ColoredClues[11].Value;
                                Field[i, j, 1] = char.Parse(ColoredClues[11].Key);
                                Console.ForegroundColor = color;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '2':
                                Field[i, j, 1] = char.Parse(ColoredClues[10].Key);
                                Console.ForegroundColor = ColoredClues[10].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '3':
                                Field[i, j, 1] = char.Parse(ColoredClues[9].Key);
                                Console.ForegroundColor = ColoredClues[9].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '4':
                                Field[i, j, 1] = char.Parse(ColoredClues[8].Key);
                                Console.ForegroundColor = ColoredClues[8].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '5':
                                Field[i, j, 1] = char.Parse(ColoredClues[7].Key);
                                Console.ForegroundColor = ColoredClues[7].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '6':
                                Field[i, j, 1] = char.Parse(ColoredClues[6].Key);
                                Console.ForegroundColor = ColoredClues[6].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '7':
                                Field[i, j, 1] = char.Parse(ColoredClues[5].Key);
                                Console.ForegroundColor = ColoredClues[5].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '8':
                                Field[i, j, 1] = char.Parse(ColoredClues[4].Key);
                                Console.ForegroundColor = ColoredClues[4].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '9':
                                Field[i, j, 1] = char.Parse(ColoredClues[3].Key);
                                Console.ForegroundColor = ColoredClues[3].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '0':
                                Console.Write(' ');
                                break;
                            //flag
                            case 'x':
                                Field[i, j, 1] = char.Parse(ColoredClues[12].Key);
                                Console.ForegroundColor = ColoredClues[12].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            default:
                                Console.Write(Field[i, j, 1]);
                                break;
                        }
                    }
                    //under cursor
                    else
                    {
                        switch (Field[i, j, 1])
                        {
                            case '1':
                                ConsoleColor color = ColoredClues[11].Value;
                                Field[i, j, 1] = char.Parse(ColoredClues[11].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = color;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '2':
                                Field[i, j, 1] = char.Parse(ColoredClues[10].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[10].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '3':
                                Field[i, j, 1] = char.Parse(ColoredClues[9].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[9].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '4':
                                Field[i, j, 1] = char.Parse(ColoredClues[8].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[8].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '5':
                                Field[i, j, 1] = char.Parse(ColoredClues[7].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[7].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '6':
                                Field[i, j, 1] = char.Parse(ColoredClues[6].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[6].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '7':
                                Field[i, j, 1] = char.Parse(ColoredClues[5].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[5].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '8':
                                Field[i, j, 1] = char.Parse(ColoredClues[4].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[4].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '9':
                                Field[i, j, 1] = char.Parse(ColoredClues[3].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[3].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            case '0':
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.Write('\u2592');
                                Console.ResetColor();
                                break;
                            //flag
                            case 'x':
                                Field[i, j, 1] = char.Parse(ColoredClues[12].Key);
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ColoredClues[12].Value;
                                Console.Write(Field[i, j, 1]);
                                Console.ResetColor();
                                break;
                            default:
                                Console.Write(Field[i, j, 1]);
                                break;
                        }
                        Field[i, j, 2] = (char)(0 + 48); //resetting cursor
                    }
                }
                Console.WriteLine();
            }
        }
        static int[,,] Explode(int[,,] Grid)
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j, 0] == 9)
                    {
                        Grid[i, j, 1] = Grid[i, j, 0];
                    }
                }
            }
            Note[] Boom = { new Note(Tone.g, Duration.WHOLE) };
            foreach (Note n in Boom)
            {
                Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
            return Grid;
        }
        static void Listener()
        {
            int width, height;
            int depth = 4;
            int ycurs = 0, xcurs = 0;
            bool inGame, lost = false, won = false;
            Setting(out width, out height);
            int[,,] Grid = new int[height, width, depth];
            char[,,] TableFrames = Table(height, width, depth);
            BombPreset(Grid);
            CursorDraw(Grid, ycurs, xcurs);
            char[,,] Field = TableArrayMerge(Grid, TableFrames);
            FullDraw(Field);
            inGame = true;
            Console.WriteLine("Press NUM digits to move a cursor, NUM5 to step, NUM0 to mark a field, escape to quit");
            do
            {
                ConsoleKeyInfo pressed = Console.ReadKey();
                switch (pressed.Key)
                {
                    case ConsoleKey.Escape:
                        {
                            Console.WriteLine("You want to exit? (\"y\" for quit):");
                            char sure = char.Parse(Console.ReadLine());
                            if (sure == 'y' || sure == 'Y')
                            {
                                inGame = false;
                                Credits();
                                break;
                            }
                            else break;
                        }
                    case ConsoleKey.NumPad6:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(1) - 1)
                            {
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad4:
                        {
                            Console.Clear();
                            if (xcurs != 0)
                            {
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad1:
                        {
                            Console.Clear();
                            if (xcurs != 0 && ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad3:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(1) - 1 && ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad2:
                        {
                            Console.Clear();
                            if (ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad7:
                        {
                            Console.Clear();
                            if (xcurs != 0 && ycurs != 0)
                            {
                                ycurs -= 1;
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad8:
                        {
                            Console.Clear();
                            if (ycurs != 0)
                            {
                                ycurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad9:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(0) - 1 && ycurs != 0)
                            {
                                ycurs -= 1;
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    //Step
                    case ConsoleKey.NumPad5:
                        {
                            Console.Clear();
                            Opener(Grid, ycurs, xcurs, out inGame, out lost, out won);
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    //Flag
                    case ConsoleKey.NumPad0:
                        {
                            Console.Clear();
                            FlagDraw(Grid, ycurs, xcurs);
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                }
            }
            while (inGame);
            if (lost)
            {
                Console.Clear();
                Explode(Grid);
                TableArrayMerge(Grid, TableFrames);
                FullDraw(Field);
                Console.WriteLine("Do you want to play again? (y/n):");
                char restarto;
                string line = Console.ReadLine();
                bool isValid = char.TryParse(line, out restarto);
                while (!isValid)
                {
                    Console.WriteLine("\nPress Y or N:");
                    line = Console.ReadLine();
                    isValid = char.TryParse(line, out restarto);
                }
                if (restarto == 'y' || restarto == 'Y')
                {
                    Console.Clear();
                    Listener();
                }
                else if (restarto == 'n' || restarto == 'N')
                {
                    Credits();
                }
                else isValid = false;
            }
        }
        public static void Credits()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(";                 !!zzzz..                 :\n;             ::wwBBBBBB##ff               ;  SSSS  TTTTTTTT   A     SSSS \n:             UUBBDDDDDDUUDDee             ; S    S    TT     A A   S    S\n:           nnDDww!!..!!!!zzBBee           ; S         TT     A A   S     \n:           UUww!!;;;;;;;;!!nn##::         :  SSS      TT     A A    SSS  \n:         !!UUzz;;;;;;;;....!!wwff         :     S     TT    A   A      S \n:         zzww!!;;;;;;;;;;..!!UUee         :      S    TT    AAAAA       S\n:         eewwff;;;;    ::....DDUU         : S    S    TT   A     A S    S\n:         eewwff!!..;;::..ff!!UUUU         :  SSSS     TT   A     A  SSSS \n:       ::wwww..;;::....;;::;;eeww         :\n:       ..wwww..::  ..::;;  ::UUDD         :\n:       zzwwUUzz    ..;;;;  ::BBBB::       :\n:       nnwwUUww;;ffff,,nnff,,MMBB!!       :\n:     ..eewwUUBBeeee!!;;!!eeUUMMBBzz       :\n:     zzwwwwwwMMUU,,;;!!;;ff##BBDDee       :\n:   ;;nnUUwwee####UUzznnnnDD##BBDDww;;     :\n: ::zzeewwwweeBB##DDBBBBDDBBeeBBBBUUUUff   :\n;;ffeeUUUUwweeeeww##BBBBBBDDnnzzeeeewwww.. :\nwwUUUUwwUUUUeeff!!DDDDDDUUUUffzznnzzeeeeee::\nzzzzUUUUwweezznnnnnnzzzzzzffffzzffzzzzeeee!!\n!!ffeeeeeezzffff  ffff!!  nneeee!!ffffnnnnzz\n           ****  *     * *****  *\n           *   *  *   *  *      *\n           ****    * *   *****  *\n           *   *    *    *       \n           ****     *    *****  *");
            //gC gab e, ea gfg c, cd def fga bCD; gE DCD g, gC bab e, ea gfg c, cC bag
            Note[] Anthem =
            {
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.C, Duration.HALF),
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.a, Duration.QUARTER),
                new Note(Tone.b, Duration.HALF),
                new Note(Tone.e, Duration.HALF),
                new Note(Tone.e, Duration.QUARTER),
                new Note(Tone.a, Duration.HALF),
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.f, Duration.QUARTER),
                new Note(Tone.g, Duration.HALF),
                new Note(Tone.c, Duration.HALF),
                new Note(Tone.c, Duration.QUARTER),
                new Note(Tone.d, Duration.HALF),
                new Note(Tone.d, Duration.QUARTER),
                new Note(Tone.e, Duration.QUARTER),
                new Note(Tone.f, Duration.HALF),
                new Note(Tone.f, Duration.QUARTER),
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.a, Duration.HALF),
                new Note(Tone.b, Duration.QUARTER),
                new Note(Tone.C, Duration.QUARTER),
                new Note(Tone.D, Duration.WHOLE),

                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.E, Duration.HALF),
                new Note(Tone.D, Duration.QUARTER),
                new Note(Tone.C, Duration.QUARTER),
                new Note(Tone.D, Duration.HALF),
                new Note(Tone.g, Duration.HALF),
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.C, Duration.HALF),
                new Note(Tone.b, Duration.QUARTER),
                new Note(Tone.a, Duration.QUARTER),
                new Note(Tone.b, Duration.HALF),
                new Note(Tone.e, Duration.HALF),
                new Note(Tone.e, Duration.QUARTER),
                new Note(Tone.a, Duration.HALF),
                new Note(Tone.g, Duration.QUARTER),
                new Note(Tone.f, Duration.QUARTER),
                new Note(Tone.g, Duration.HALF),
                new Note(Tone.c, Duration.QUARTER),
                new Note(Tone.c, Duration.QUARTER),
                new Note(Tone.C, Duration.HALF),
                new Note(Tone.b, Duration.QUARTER),
                new Note(Tone.a, Duration.QUARTER),
                new Note(Tone.g, Duration.HALF),
            };
            // Play the notes in a song.
            foreach (Note n in Anthem)
            {
                if (n.NoteTone == Tone.REST) { Thread.Sleep((int)n.NoteDuration); }
                else Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }
        // Frequencies of notes
        protected enum Tone
        { REST = 0, c = 523, d = 587, e = 659, f = 698, g = 784, a = 880, b = 988, C = 1047, D = 1175, E = 1319 }
        // Duration of a note in ms
        protected enum Duration
        { WHOLE = 800, HALF = WHOLE / 2, QUARTER = HALF / 2 }
        // Define a note as (FREQ,TIME)
        protected struct Note
        {
            Tone toneVal;
            Duration durVal;
            // Constructor to create a note
            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }
            // Define properties to return the note's tone and duration.
            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }
        //The Main
        static void Main(string[] args)
        {
            int width, height; //quantity of the cells
            int depth = 4; //stances
            int ycurs = 0, xcurs = 0; //cursor position
            bool inGame, lost = false, won = false;
            Setting(out width, out height);
            int[,,] Grid = new int[height, width, depth];
            char[,,] TableFrames = Table(height, width, depth);
            BombPreset(Grid);
            CursorDraw(Grid, ycurs, xcurs);
            char[,,] Field = TableArrayMerge(Grid, TableFrames);
            FullDraw(Field);
            inGame = true;
            Console.WriteLine("Press NUM digits to move a cursor, NUM5 to step, NUM0 to mark a field, escape to quit");
            do
            {
                ConsoleKeyInfo pressed = Console.ReadKey();
                switch (pressed.Key)
                {
                    case ConsoleKey.Escape:
                        {
                            Console.WriteLine("You want to exit? (\"y\" for quit):");
                            char sure = char.Parse(Console.ReadLine());
                            if (sure == 'y' || sure == 'Y')
                            {
                                inGame = false;
                                Credits();
                                break;
                            }
                            else break;
                        }
                    case ConsoleKey.NumPad6:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(1) - 1)
                            {
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad4:
                        {
                            Console.Clear();
                            if (xcurs != 0)
                            {
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad1:
                        {
                            Console.Clear();
                            if (xcurs != 0 && ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad3:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(1) - 1 && ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad2:
                        {
                            Console.Clear();
                            if (ycurs != Grid.GetLength(0) - 1)
                            {
                                ycurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad7:
                        {
                            Console.Clear();
                            if (xcurs != 0 && ycurs != 0)
                            {
                                ycurs -= 1;
                                xcurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad8:
                        {
                            Console.Clear();
                            if (ycurs != 0)
                            {
                                ycurs -= 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    case ConsoleKey.NumPad9:
                        {
                            Console.Clear();
                            if (xcurs != Grid.GetLength(0) - 1 && ycurs != 0)
                            {
                                ycurs -= 1;
                                xcurs += 1;
                            }
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    //Step
                    case ConsoleKey.NumPad5:
                        {
                            Console.Clear();
                            Opener(Grid, ycurs, xcurs, out inGame, out lost, out won);
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                    //Flag
                    case ConsoleKey.NumPad0:
                        {
                            Console.Clear();
                            FlagDraw(Grid, ycurs, xcurs);
                            CursorDraw(Grid, ycurs, xcurs);
                            TableArrayMerge(Grid, TableFrames);
                            FullDraw(Field);
                            break;
                        }
                }
            }
            while (inGame);
            if (lost)
            {
                Console.Clear();
                Explode(Grid);
                TableArrayMerge(Grid, TableFrames);
                FullDraw(Field);
                Console.WriteLine("Do you want to play again? (y/n):");
                char restarto;
                string line = Console.ReadLine();
                bool isValid = char.TryParse(line, out restarto);
                while (!isValid)
                {
                    Console.WriteLine("\nPress Y or N:");
                    line = Console.ReadLine();
                    isValid = char.TryParse(line, out restarto);
                }
                if (restarto == 'y' || restarto == 'Y')
                {
                    Console.Clear();
                    Listener();
                }
                else if (restarto == 'n' || restarto == 'N')
                {
                    Credits();
                }
                else isValid = false;
            }
            Console.ReadKey();
        }
    }
}