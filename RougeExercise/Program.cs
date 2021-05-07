using System;
using System.Threading;

namespace RougeExercise
{
    class MainClass
    {
        public static void Main(string[] args)

            /* Add different difficulties
             * Add rooms
             * Add check for walls blocking off areas of the map
             * Add weapons and health (requires rework to how monster/user interactions work)
             *
             */
        {
            int width = 21;
            int x = 10;
            int y = 10;
            int treasureCount = 10;
            string treasure = "$";
            int trapCount = 30;
            string trap = "*";
            int walls = 100;
            string wall = "|";
            int monsters = 30;
            int[] mX = new int[monsters];
            int[] mY = new int[monsters];
            int lives = 5;
            int milliseconds = 5000;
            string rules = "";
            string[,] board = new string[width, width];
            Random rnd = new Random();

            //defines the board
            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    board[row, column] = ".";
                }
            }
            
            //Places our character
            board[y, x] = "+";

            //Placing Monsters
            MonsterRandomizer(monsters, rnd, width, board, mX, mY);
            //Placing treasure
            Randomizer(treasureCount, rnd, width, board, treasure);
            //Placing Traps
            Randomizer(trapCount, rnd, width, board, trap);
            //Placing Walls
            Randomizer(walls, rnd, width, board, wall);


            Console.WriteLine("Hi there adventurer! To move your character, please use the keys W, A, S, or D! You can also use the arrow keys!");
            Console.WriteLine("There are lost treasures in the area and it is you're job to find and recover them!");
            Console.WriteLine("But watchout for traps! as they are everywhere in this jungle");
            Console.WriteLine("Are you ready to begin?");
            Console.WriteLine("Y/N");
            string readyKey = Console.ReadKey(true).Key.ToString();
            if (readyKey == "N")
            {
                Console.WriteLine("To bad!");
                Rules(rules);
                Thread.Sleep(milliseconds);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true).Key.ToString();

            }
            else if (readyKey == "Y")
            {
                Console.WriteLine("Lets go adventurer!");
                Rules(rules);
                Thread.Sleep(milliseconds);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true).Key.ToString();
            }
            else
            {
                Console.WriteLine("You have actally failed to give one of the two basic responses I asked for. Now you don't get to see the rules!");
                Thread.Sleep(milliseconds);
            }

            while (treasureCount != 0 && lives != 0)
            {
                board[y, x] = "+";
                Console.Clear();
                //drawBoard(board);
                //drawBoardFog(board);
                drawBoardFog3x3(board, x, y);
                drawBoardFog5x5(board, x, y);


                Console.WriteLine("Where do you want to move?");
                if (treasureCount == 1)
                {
                    Console.WriteLine($"There is {treasureCount} more piece of treasure remaining!");
                }
                else
                    Console.WriteLine($"There are {treasureCount} more pieces of treasure remaining!");
                if (lives == 1)
                {
                    Console.WriteLine($"You only have {lives} more life remaining, remaining be careful!");
                }
                else
                    Console.WriteLine($"You have {lives} more lives remaining!");

                string userTypeKey = Console.ReadKey(true).Key.ToString();

                if ((userTypeKey == "W" || userTypeKey == "UpArrow") && y > 0 && board[(y-1), x] != "|")
                {
                    board[y, x] = ".";
                    y--;
                    if (board[y, x] == "$")
                    {
                        treasureCount--;
                        Console.WriteLine("You found a piece of treasure!");
                    }
                    if (board[y, x] == "*")
                    {
                        trapCount--;
                        lives--;
                        Console.WriteLine("You hit a trap!");
                    }
                    if (board[y, x] == "M")
                    {
                        lives = 0;
                        Console.WriteLine("You ran into a monster");
                    }
                }
                else if ((userTypeKey == "A" || userTypeKey == "LeftArrow" ) && x > 0 && board[y, (x - 1)] != "|")
                {
                    board[y, x] = ".";
                    x--;
                    if (board[y, x] == "$")
                    {
                        treasureCount--;
                        Console.WriteLine("You found a piece of treasure!");
                    }
                    if (board[y, x] == "*")
                    {
                        trapCount--;
                        lives--;
                        Console.WriteLine("You hit a trap!");
                    }
                    if (board[y, x] == "M")
                    {
                        lives = 0;
                        Console.WriteLine("You ran into a monster");
                    }
                }
                else if ((userTypeKey == "S" || userTypeKey == "DownArrow") && y < width - 1 && board[(y + 1), x] != "|")
                {
                    board[y, x] = ".";
                    y++;
                    if (board[y, x] == "$")
                    {
                        treasureCount--;
                        Console.WriteLine("You found a piece of treasure!");
                    }
                    if (board[y, x] == "*")
                    {
                        trapCount--;
                        lives--;
                        Console.WriteLine("You hit a trap!");
                    }
                    if (board[y, x] == "M")
                    {
                        lives = 0;
                        Console.WriteLine("You ran into a monster");
                    }
                }
                else if ((userTypeKey == "D" || userTypeKey == "RightArrow") && x < width - 1 && board[y, (x + 1)] != "|")
                {
                    board[y, x] = ".";
                    x++;
                    if (board[y, x] == "$")
                    {
                        treasureCount--;
                        Console.WriteLine("You found a piece of treasure!");
                    }
                    if (board[y, x] == "*")
                    {
                        trapCount--;
                        lives--;
                        Console.WriteLine("You hit a trap!");
                    }
                    if (board[y, x] == "M")
                    {
                        lives = 0;
                        Console.WriteLine("You ran into a monster");
                    }
                }
                else
                    Console.WriteLine("Please try again");
                board[y, x] = "+";

                //Handles Monster movement and rules for movement
                for (int i = 0; i < monsters; i++)
                {
                    board[mY[i], mX[i]] = ".";
                    int monsterMove = rnd.Next(1, 5);
                    if (monsterMove == 1 && mY[i] > 0 && board[(mY[i] - 1), mX[i]] != "M" && board[(mY[i] - 1), mX[i]] != "*" && board[(mY[i] - 1), mX[i]] != "$" && board[(mY[i] - 1), mX[i]] != "|")
                    {
                        mY[i]--;
                    }
                    if (monsterMove == 2 && mY[i] < width - 1 && board[(mY[i] + 1), mX[i]] != "M" && board[(mY[i] + 1), mX[i]] != "*" && board[(mY[i] + 1), mX[i]] != "$" && board[(mY[i] + 1), mX[i]] != "|")
                    {
                        mY[i]++;
                    }
                    if (monsterMove == 3 && mX[i] > 0 && board[mY[i], (mX[i] - 1)] != "M" && board[mY[i], (mX[i] - 1)] != "*" && board[mY[i], (mX[i] - 1)] != "$" && board[mY[i], (mX[i] - 1)] != "|")
                    {
                        mX[i]--;
                    }
                    if (monsterMove == 4 && mX[i] < width - 1 && board[mY[i], (mX[i] + 1)] != "M" && board[mY[i], (mX[i] + 1)] != "*" && board[mY[i], (mX[i] + 1)] != "$" && board[mY[i], (mX[i] + 1)] != "|")
                    {
                        mX[i]++;
                    }
                    if (board[mY[i], mX[i]] == "+")
                    {
                        board[mY[i], mX[i]] = "M";
                        Console.WriteLine("The monster caught you!");
                        lives = 0;
                    }
                    else
                        board[mY[i], mX[i]] = "M";
                }

            }

            //drawBoard(board);
            //drawBoardFog(board);
            Console.Clear();
            drawBoard(board);
            if (treasureCount == 0)
            {
                Console.WriteLine("Congrats! You have found all of the treasure!");
            }
            else
                Console.WriteLine("You ran out of lives! Better luck next time adventurer!");
        }

        ////Checks to see if the move is valid, can then be used to make user input a new move that is valid without allowing "non-moves"
        //static int[] isMoveValid(string[,] board, int xPos, int yPos, string inputDirection,char[] blockCharacters)
        //{
        //    int xMove = 0;
        //    int yMove = 0;

        //    int[] newPos = new int[2];
        //    if (inputDirection == "A") { xMove--; } // Left
        //    else if (inputDirection == "D") { xMove++; }
        //    else if (inputDirection == "S") { yMove--; }
        //    else if (inputDirection == "W") { yMove++; }

        //    if (board[yPos + yMove, xPos + xMove] != "|")
        //    {
        //        newPos[0] = xPos + xMove;
        //        newPos[1] = yPos + yMove;

        //    }

        //    return newPos;

        //}

        static void drawBoard(string[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    Console.Write(board[row, column] + " ");
                }
                Console.WriteLine();
            }
        }
        static void drawBoardFog(string[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] == "$")
                    {
                        Console.Write("- ");
                    }
                    else if (board[row, column] == "*")
                    {
                        Console.Write("- ");
                    }
                    else if (board[row, column] == "0")
                    {
                        Console.Write("- ");
                    }
                    else
                        Console.Write(board[row, column] + " ");
                }
                Console.WriteLine();
            }
        }
        static void drawBoardFog3x3(string[,] board, int x, int y)
        {
            int width = board.GetLength(0);
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1) && board[(row + 1), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if (row > 0 && board[(row - 1), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column < width - 1) && board[(row + 1), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column > 0) && board[(row + 1), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column > 0) && board[(row - 1), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column < width - 1) && board[(row - 1), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column < width - 1) && board[row, (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column > 0) && board[row, (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }
        static void drawBoardFog5x5(string[,] board, int x, int y)
        {
            int width = board.GetLength(0);
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1) && board[(row + 1), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 2) && board[(row + 2), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if (row > 0 && board[(row - 1), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if (row > 1 && board[(row - 2), column] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column < width - 1) && board[(row + 1), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 2 && column < width - 1) && board[(row + 2), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column < width - 2) && board[(row + 1), (column + 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 2 && column < width - 2) && board[(row + 2), (column + 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column > 0) && board[(row + 1), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 2 && column > 0) && board[(row + 2), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 1 && column > 1) && board[(row + 1), (column - 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row < width - 2 && column > 1) && board[(row + 2), (column - 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column > 0) && board[(row - 1), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 1 && column > 0) && board[(row - 2), (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column > 1) && board[(row - 1), (column - 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 1 && column > 1) && board[(row - 2), (column - 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column < width - 1) && board[(row - 1), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 1 && column < width - 1) && board[(row - 2), (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 0 && column < width - 2) && board[(row - 1), (column + 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((row > 1 && column < width - 2) && board[(row - 2), (column + 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column < width - 1) && board[row, (column + 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column < width - 2) && board[row, (column + 2)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column > 0) && board[row, (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else if ((column > 0) && board[row, (column - 1)] == board[y, x])
                    {
                        Console.Write(board[row, column] + " ");
                    }
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }
        static string Rules(string rules)
        {
            Console.WriteLine("Here are some rules you might want to know:");
            Console.WriteLine("1. There are a few symbols in this game used to display this game, here is a quick guide explaining what they all mean:");
            Console.WriteLine("   Your Character = \"+\" // Areas you can see = \".\" // Areas you can't see = \"-\" // \n   Treasures = \"$\" // Traps = \"*\" // Monsters = \"M\" // Wall = \"|\"");
            Console.WriteLine("2. There is a fog around your character at all times, so be careful where you step and watch out for traps!");
            Console.WriteLine("3. If you step on a trap you lose a life");
            Console.WriteLine("4. if you are caught by a monster you lose");
            Console.WriteLine("5. The game ends when you find all the treasure");
            return rules;
        }

        static void Randomizer (int amount, Random rnd, int width, string[,] board, string symbol)
        {
            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int x2 = rnd.Next(0, width);
                    int y2 = rnd.Next(0, width);

                    if (board[x2, y2] == "$")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "M")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "*")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "|")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "+")
                    {
                        continue;
                    }

                    board[x2, y2] = symbol;
                    break;
                }
            }
        }
        static void MonsterRandomizer(int monsters, Random rnd, int width, string[,] board, int[] mX, int[] mY)
        {
            for (int i = 0; i < monsters; i++)
            {
                while (true)
                {
                    int x2 = rnd.Next(0, width);
                    int y2 = rnd.Next(0, width);

                    if (board[x2, y2] == "$")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "M")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "*")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "|")
                    {
                        continue;
                    }
                    if (board[x2, y2] == "+")
                    {
                        continue;
                    }

                    board[y2, x2] = "M";
                    mX[i] = x2;
                    mY[i] = y2;
                    break;
                }
            }
        }
    }
}
