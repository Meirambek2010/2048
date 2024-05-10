// See https://aka.ms/new-console-template for more information
namespace f2048
{
    class f2048
    {
        //size
        static int width = 12;
        static int height = 12;
        //squar
        static int squar_len_x = 3;
        static int squar_len_y = 3;
        //map size
        static int map_len_x = width / squar_len_x;
        static int map_len_y = height / squar_len_y;
        static bool Comparison_arrays(int[,] a)
        {
            bool varriable = true;
            for(int i = 0; i < map_len_y; i++)
            {
                for(int j = 0; j < map_len_x; j++)
                {
                    if(a[i, j] != 0)
                    {
                        varriable = false;
                        break;
                    }
                }
                if(varriable == false)
                {
                    break;
                }
            }
            return varriable;
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //map
            int[,] map = new int[map_len_y, map_len_x];
            int[,] new_map = new int[map_len_y, map_len_x];
            //direction
            int[,] direction_x = new int [map_len_y, map_len_x];
            int[,] direction_y = new int [map_len_y, map_len_x];
            int[,] new_direction_x = new int [map_len_y, map_len_x];
            int[,] new_direction_y = new int [map_len_y, map_len_x];
            //squar
            int[,] squar_delay = new int[map_len_y, map_len_x];
            int squar_ap = 0;
            //free cell
            var free_cell_x = new List<int>();
            var free_cell_y = new List<int>();
            //squar add
            for(int i = 0; i < map_len_y; i++)
            {
                for(int j = 0; j < map_len_x; j++)
                {
                    free_cell_x.Add(j);
                    free_cell_y.Add(i);
                }
            }
            for(int i = 0; i < 2; i++)
            {
                int r = rnd.Next(0, free_cell_x.Count);
                new_map[free_cell_y[r], free_cell_x[r]] = rnd.Next(1, 3) * 2;
                free_cell_x.RemoveAt(r);
                free_cell_y.RemoveAt(r);
            }
            //while
            bool game = true;
            int fps = 25;
            while(game)
            {
                for(int i = 0; i < map_len_y; i++)
                {
                    for(int j = 0; j < map_len_x; j++)
                    {
                        if(map[i, j] == 0)
                        {
                            free_cell_x.Add(j);
                            free_cell_y.Add(i);
                        }
                    }
                }
                //write
                for(int i = 0; i < height; i++)
                {
                    for(int j = 0; j < width; j++)
                    {
                        if(map[i / squar_len_y, j / squar_len_x] != 0)
                        {
                            if(map[i / squar_len_y, j / squar_len_x] == 2)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                            }
                            else if(map[i / squar_len_y, j / squar_len_x] == 4)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            else if(map[i / squar_len_y, j / squar_len_x] == 8)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                            }
                            else if(map[i / squar_len_y, j / squar_len_x] < 1000)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                            }
                            else if(map[i / squar_len_y, j / squar_len_x] < 10000)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                            }
                            if((i + 2) % 3 == 0 & (j + 2) % 3 == 0)
                            {
                                if(map[i / squar_len_y, j / squar_len_x] < 10)
                                {
                                    Console.Write(map[i / squar_len_y, j / squar_len_x] + " ");
                                }
                                else
                                {
                                    Console.Write(map[i / squar_len_y, j / squar_len_x]);
                                }
                            }
                            else
                            {
                                if(map[i / squar_len_y, j / squar_len_x] > 100 & (i + 2) % 3 == 0 & (j + 1) % 3 == 0)
                                {
                                    if(map[i / squar_len_y, j / squar_len_x] < 1000)
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                else
                                {
                                    Console.Write("  ");
                                }
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("  ");
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        if(map[i / squar_len_y, j / squar_len_x] != 0)
                        {
                            if(new_map[i / squar_len_y, j / squar_len_x] == 0)
                            {
                                new_map[i /squar_len_y, j / squar_len_x] = map[i / squar_len_y, j / squar_len_x];
                            }
                            new_direction_x[i / squar_len_y, j / squar_len_x] = direction_x[i / squar_len_y, j / squar_len_x];
                            new_direction_y[i / squar_len_y, j / squar_len_x] = direction_y[i / squar_len_y, j / squar_len_x];
                        }
                        //move
                        if(map[i / squar_len_x, j / squar_len_x] != 0 & (direction_x[i / squar_len_y, j / squar_len_x] != 0 || direction_y[i / squar_len_y, j / squar_len_x] != 0))
                        {
                            if(i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x] < map_len_y & i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x] > -1 & j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x] < map_len_x & j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x] > -1)
                            {
                                if(map[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] == 0)
                                {
                                    new_map[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] = map[i / squar_len_y, j / squar_len_x];
                                    new_map[i / squar_len_y, j / squar_len_x] = 0;
                                    new_direction_x[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] = direction_x[i / squar_len_y, j / squar_len_x]; 
                                    new_direction_y[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] = direction_y[i / squar_len_y, j / squar_len_x];
                                    squar_ap = 1;
                                }
                                else if(map[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] == map[i / squar_len_y, j / squar_len_x])
                                {
                                    new_map[i / squar_len_y + direction_y[i / squar_len_y, j / squar_len_x], j / squar_len_x + direction_x[i / squar_len_y, j / squar_len_x]] = map[i / squar_len_y, j / squar_len_x] * 2;
                                    new_map[i / squar_len_y, j / squar_len_x] = 0;
                                    squar_ap = 1;
                                }
                                else
                                {
                                    if(squar_delay[i / squar_len_y, j / squar_len_x] >= width)
                                    {
                                        new_direction_x[i / squar_len_y, j / squar_len_x] = 0;
                                        new_direction_y[i / squar_len_y, j / squar_len_x] = 0;
                                        squar_delay[i / squar_len_y, j / squar_len_x] = 0;
                                    }
                                    else
                                    {
                                        squar_delay[i / squar_len_y, j / squar_len_x] += 1;
                                    }
                                }
                            }
                            else
                            {
                                new_direction_x[i / squar_len_y, j / squar_len_x] = 0;
                                new_direction_y[i / squar_len_y, j / squar_len_x] = 0;
                            }
                        }
                    }
                    Console.WriteLine(" ");
                }
                System.Console.WriteLine("fps:" + fps);
                //squar_ap
                if(Comparison_arrays(direction_x) & Comparison_arrays(direction_y))
                {
                    if(squar_ap == 1)
                    {
                        int r = rnd.Next(0, free_cell_x.Count);
                        new_map[free_cell_y[r], free_cell_x[r]] = rnd.Next(1, 3) * 2;
                        squar_ap = 0;
                    }
                }
                //key
                if(Console.KeyAvailable & Comparison_arrays(direction_x) & Comparison_arrays(direction_y))
                {
                    var v = Console.ReadKey(true);
                    if(v.Key == ConsoleKey.Q)
                    {
                        game = false;
                    }
                    else if(v.Key == ConsoleKey.D)
                    {
                        for(int i = 0; i < map_len_y; i++)
                        {
                            for(int j = 0; j < map_len_x; j++)
                            {
                                new_direction_x[i, j] = 1;
                            }
                        }
                    }
                    else if(v.Key == ConsoleKey.A)
                    {
                        for(int i = 0; i < map_len_y; i++)
                        {
                            for(int j = 0; j < map_len_x; j++)
                            {
                                new_direction_x[i, j] = -1;
                            }
                        }
                    }
                    else if(v.Key == ConsoleKey.W)
                    {
                        for(int i = 0; i < map_len_y; i++)
                        {
                            for(int j = 0; j < map_len_x; j++)
                            {
                                new_direction_y[i, j] = -1;
                            }
                        }
                    }
                    else if(v.Key == ConsoleKey.S)
                    {
                        for(int i = 0; i < map_len_y; i++)
                        {
                            for(int j = 0; j < map_len_x; j++)
                            {
                                new_direction_y[i, j] = 1;
                            }
                        }
                    }
                }
                //time
                Thread.Sleep(1000 / fps);
                Console.Clear();
                map = new_map;
                new_map = new int[map_len_y, map_len_x];
                direction_x = new_direction_x;
                direction_y = new_direction_y;
                new_direction_x = new int[map_len_y, map_len_x];
                new_direction_y = new int[map_len_y, map_len_x];
                free_cell_x.Clear();
                free_cell_y.Clear();
            }
        }
    }
}