using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            #region vars
            int[] xPosition = new int[50];
            xPosition[0] = 40;
            int[] yPosition = new int[50];
            yPosition[0] = 15;
            decimal gamespeed = 200m;

            decimal score = 0;
            int lvl = 1;
            int keycount = 0;


            int applexdimension = 10;
            int appleydimension = 10;
            int appleeaten = 0;
            string useraction = "";
            string username = "";
            bool isgameon = true;
            bool issatyingmenu = true;
            bool iswallhit = false;
            bool isappleeaten = false;
            Random rnd = new Random();
            Console.CursorVisible = false;
            #endregion
           

            do
            {
                Console.Clear();

                buildwall();
                displayline(28, "Green");
                displaysnake();
                ShowGameName();
                showmenu(out useraction);
                xPosition[0] = 40;
                yPosition[0] = 15;
                isgameon = true;
                gamespeed = 200;

                switch (useraction)
                {
                    #region case directions
                    //give player options to read instructions
                    case "1":
                    case "d":
                    case "directions":
                        Console.Clear();
                        buildwall();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(5, 5);
                        Console.WriteLine("1) Resize the console window so you can see all");
                        Console.SetCursorPosition(5, 6);
                        Console.WriteLine("   4 sides of playing field border");
                        Console.SetCursorPosition(5, 7);
                        Console.WriteLine("2) Use the arrow keys to move the snake around the field.");
                        Console.SetCursorPosition(5, 8);
                        Console.WriteLine("3) The snake will die if it runs into the wall");
                        Console.SetCursorPosition(5, 9);
                        Console.WriteLine("4) You gain points by eating the apple.");
                        Console.SetCursorPosition(5, 10);
                        Console.WriteLine("   but your snake will go faster and will be longer");
                        Console.SetCursorPosition(5, 11);
                        Console.WriteLine("Press enter to return the main menu");
                        Console.ReadLine();
                        Console.Clear();
                        //                        showmenu(out useraction);
                        break;
                    #endregion
                    #region case play
                    case "2":
                    case "p":
                    case "play":
                        askusername(out username);
                        Console.Clear();
                        #region game setup
                        buildwall();
                        displayline(28, "Green");
                        displayscore(score, keycount, lvl, username);

                        //get the snake to appear on screen
                        paintsnake(appleeaten, xPosition, yPosition, out xPosition, out yPosition);
                        //set apple on screen
                        setapplepositiononscreen(rnd, out applexdimension, out appleydimension);
                        paintapple(applexdimension, appleydimension);

                        //build boundry

                        ConsoleKey command = Console.ReadKey().Key;

                        keycount++;


                        #endregion
                        do
                        {

                            #region change directions
                            switch (command)
                            {
                                case ConsoleKey.LeftArrow:
                                    Console.SetCursorPosition(xPosition[0], yPosition[0]);
                                    Console.Write(" ");
                                    xPosition[0]--;

                                    break;
                                case ConsoleKey.RightArrow:
                                    Console.SetCursorPosition(xPosition[0], yPosition[0]);
                                    Console.Write(" ");
                                    xPosition[0]++;

                                    break;
                                case ConsoleKey.UpArrow:
                                    Console.SetCursorPosition(xPosition[0], yPosition[0]);
                                    Console.Write(" ");
                                    yPosition[0]--;

                                    break;
                                case ConsoleKey.DownArrow:
                                    Console.SetCursorPosition(xPosition[0], yPosition[0]);
                                    Console.Write(" ");
                                    yPosition[0]++;
                                    break;
                            }

                            #endregion
                            #region playing game
                            //paint the snake,make snake longer
                            paintsnake(appleeaten, xPosition, yPosition, out xPosition, out yPosition);

                            iswallhit = didsnakehitwall(xPosition[0], yPosition[0]);
                            //detects when snake hits boundry 
                            if (iswallhit)
                            {
                                isgameon = false;
                                Console.SetCursorPosition(25, 20);
                                Console.WriteLine("The snake hit the wall and died");
                                Console.ForegroundColor = ConsoleColor.Red;
                                //show score
                                Console.SetCursorPosition(15, 21);
                                // Console.Write("your score is " + appleeaten * 100 + "!");
                                Console.Write("your score is " + score + "!");
                                Console.SetCursorPosition(15, 22);
                                Console.WriteLine("Press Enter to continue");
                                appleeaten = 0;
                                keycount = 0;
                                score = 0;
                                lvl = 1;

                                xPosition[0] = 40;
                                yPosition[0] = 15;
                                xPosition[1] = 41;
                                yPosition[1] = 15;

                                Console.ReadLine();
                                Console.Clear();


                            }
                            //detects when apple is eaten
                            isappleeaten = determineifapplewaseaten(xPosition[0], yPosition[0], applexdimension, appleydimension);
                            //place apple on board randomly
                            if (Console.KeyAvailable)
                            {
                                command = Console.ReadKey().Key;
                                keycount++;
                                displayscore(score, keycount, lvl, username);

                            }
                            if (isappleeaten)
                            {

                                setapplepositiononscreen(rnd, out applexdimension, out appleydimension);
                                paintapple(applexdimension, appleydimension);
                                //keep tracks of how many apples were eaten
                                //make snake longer
                                appleeaten++;
                                if (appleeaten % 4 == 0)
                                {
                                    lvl++;
                                    gamespeed *= .925m;
                                }
                                if (keycount < 10)
                                {
                                    score = score + 100 + ((10 - keycount) * 10);
                                }
                                else
                                {
                                    score = score + 100;

                                }

                                keycount = 0;
                                displayscore(score, keycount, lvl, username);


                                //make snake faster
                            }

                            //slow game down
                            System.Threading.Thread.Sleep((Convert.ToInt32(gamespeed)));


                            #endregion

                        }
                        while (isgameon);
                        break;
                    #endregion
                    case "3":
                    case "e":
                    case "exit":
                        issatyingmenu = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("your input was not understood. Please press enter..");
                        Console.ReadLine();
                        Console.Clear();
                        showmenu(out useraction);
                        break;
                }
            }
            while (issatyingmenu);

            //give player options to restart

            Console.ReadKey();
        }
        #region methods

        #region menu
        public static void displaysnake()
        {
            int xx = 0;
            int yy = 0;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 18; i > 1; i--)
            {

                if (i > 10)
                {
                    xx = 18 - i + 3;
                    yy = i + 10;
                }
                else
                {
                    xx = i + 1;
                    yy = i + 10;
                }

                Console.SetCursorPosition(yy, xx - 1);
                Console.WriteLine("                        ");
                Console.SetCursorPosition(yy, xx);
                Console.WriteLine("      ▒▓▒▒▒▓▒    ");
                Console.SetCursorPosition(yy, xx + 1);
                Console.WriteLine("   ░░░░▓░░░▓░░░░                       ");
                Console.SetCursorPosition(yy, xx + 2);
                Console.WriteLine("▒▒▒▒▒▒▒▓▒▒▒▓▒▒▒▓▒▒               ▒▒                        ");
                Console.SetCursorPosition(yy, xx + 3);
                Console.WriteLine("        ░░░▓░░░▓░░░▓░       ░░░▓░░░▓░░░              ░░           ");
                Console.SetCursorPosition(yy, xx + 4);
                Console.WriteLine("           ▓▒▒▒▓▒▒▒▓▒▒▒▓▒▒▒▓▒▒▒▓▒▒▒▓▒▒▒▓▒▒▒▓     ▒▒▓▒▒▒▓▒▒             ");
                Console.SetCursorPosition(yy, xx + 5);
                Console.WriteLine("              ░▓░░░▓░░░▓░░░     ░░░▓░░░▓░░░▓░░░▓░░░▓░░░▓░░░▓░░░         ");
                Console.SetCursorPosition(yy, xx + 6);
                Console.WriteLine("                 ▒▒▓▒▒▒▓▒▒▒         ▒▒▒▓▒▒▒▓▒▒          ▒▒▒▓▒▒▒▓▒▒▒▓▒▒      ");
                Console.SetCursorPosition(yy, xx + 7);
                Console.WriteLine("                    ░░░▓░░             ▓░░░▓                      ░▓░░░▓░░░       ");
                Console.SetCursorPosition(yy, xx + 8);
                Console.WriteLine("                                                                                  ");
                System.Threading.Thread.Sleep(020 + i * 10);


            }
        }
        public static void displayscore(decimal score, int keycount, int lvl, string username)
        {
            Console.SetCursorPosition(2, 29);
            Console.WriteLine(username);

            Console.SetCursorPosition(32, 29);
            Console.WriteLine("Score: " + Convert.ToString(score).PadLeft(5, '0'));
            Console.SetCursorPosition(61, 29);
            Console.WriteLine("Level: " + Convert.ToString(lvl).PadLeft(3, '0'));
            Console.SetCursorPosition(90, 29);
            Console.WriteLine("Kies press: " + Convert.ToString(keycount).PadLeft(3, ' '));

        }
        public static void showmenu(out string useraction)
        {
            displayline(17, "Green");

            Console.SetCursorPosition(45, 13);

            Console.WriteLine(" 1) Directions");
            Console.SetCursorPosition(45, 14);
            Console.WriteLine(" 2) Play      ");
            Console.SetCursorPosition(45, 15);
            Console.WriteLine(" 3) Exit      ");

            Console.SetCursorPosition(45, 16);
            Console.WriteLine(" Write your Option number: ");
            Console.SetCursorPosition(71, 16);

            Console.CursorVisible = true;
            useraction = Console.ReadLine().ToLower();
            Console.WriteLine();


        }

        public static void askusername(out string username)
        {
            displayline(17, "Green");

            Console.SetCursorPosition(50, 13);
            Console.WriteLine("                 ");
            Console.SetCursorPosition(50, 14);
            Console.WriteLine("Enter your Name :");
            Console.SetCursorPosition(50, 15);
            Console.WriteLine("                 ");
            Console.SetCursorPosition(54, 16);
            Console.WriteLine("                 ");

            Console.SetCursorPosition(68, 14);


            username = Console.ReadLine().ToLower();



        }

        public static void displayline(int xpos, String color)
        {

            ConsoleColor x = ConsoleColor.White;

            x = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);

            Console.ForegroundColor = x;
            Console.SetCursorPosition(1, xpos);

            Console.Write((char)(9567));

            for (int i = 1; i < 109; i++)
            {
                Console.Write((char)(9472));

            }

            Console.Write((char)(9570));


            Console.ForegroundColor = x;
            Console.SetCursorPosition(1, xpos);

            Console.Write((char)(9567));

            for (int i = 1; i < 109; i++)
            {
                Console.Write((char)(9472));

            }

            Console.Write((char)(9570));

            Console.SetCursorPosition(54, 16);

        }


        #endregion
        private static void paintsnake(int appleeaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionout, out int[] yPositionout)
        {
            //paint the head
            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((char)210);
            //paint the body
            for (int i = 1; i < appleeaten + 1; i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine((char)9617);
            }
            //erase last part of the snake
            if (xPositionIn[appleeaten + 1] != 0)
            {
                Console.SetCursorPosition(xPositionIn[appleeaten + 1], yPositionIn[appleeaten + 1]);

                Console.WriteLine(" ");
            }
            //record the location of each body part
            for (int i = appleeaten + 1; i > 0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }
            //return the new array
            xPositionout = xPositionIn;
            yPositionout = yPositionIn;
        }

        public static bool didsnakehitwall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 110 || yPosition == 1 || yPosition == 28)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void buildwall()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(1, 1);
            Console.Write((char)(9556));
            Console.SetCursorPosition(1, 30);
            Console.Write((char)(9562));
            Console.CursorVisible=false;
            for (int i = 2; i < 110; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write((char)(9552));
                Console.SetCursorPosition(i, 30);
                Console.Write((char)(9552));
            }

            for (int i = 2; i < 30; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write((char)(9553));
                Console.SetCursorPosition(110, i);
                Console.Write((char)(9553));
            }
            Console.SetCursorPosition(110, 1);
            Console.Write((char)(9559));
            Console.SetCursorPosition(110, 30);
            Console.Write((char)(9565));

        }
        public static void setapplepositiononscreen(Random rnd, out int applexdimension, out int appleydimension)
        {
            applexdimension = rnd.Next(0 + 2, 110 - 4);
            appleydimension = rnd.Next(0 + 2, 30 - 3);
        }
        public static void paintapple(int applexdimension, int appleydimension)
        {
            Console.SetCursorPosition(applexdimension, appleydimension);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)243);
        }
        public static bool determineifapplewaseaten(int xPosition, int yPosition, int applexdimension, int appleydimension)
        {
            if (xPosition == applexdimension && yPosition == appleydimension) return true; return false;

        }
        public static void ShowGameName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int xx = 18;
            int yy = 3;
            Console.SetCursorPosition(yy, xx);
            Console.WriteLine("  ▒▒▒▒▒▒  ▒▒    ▒▒     ▒▒▒     ▒▒  ▒▒  ▒▒▒▒▒▒       ░░░░░░   ░░░░░░  ░░      ░░  ░░░░  ░░    ░░  ░░░░░░");
            Console.SetCursorPosition(yy, xx + 1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  ░░      ░░░░  ░░    ░░ ░░    ░░ ░░   ░░           ▒▒   ▒▒  ▒▒      ▒▒      ▒▒   ▒▒   ▒▒▒▒  ▒▒  ▒▒   ▒▒");
            Console.SetCursorPosition(yy, xx + 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ▒▒▒▒▒▒  ▒▒ ▒▒ ▒▒   ▒▒▒▒▒▒▒   ▒▒▒▒    ▒▒▒▒▒▒       ░░░░░░░  ░░░░░░  ░░  ░░  ░░   ░░   ░░ ░░ ░░  ░░   ░░");
            Console.SetCursorPosition(yy, xx + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("      ░░  ░░  ░░░░  ░░     ░░  ░░ ░░   ░░           ▒▒ ▒▒    ▒▒      ▒▒ ▒▒▒▒ ▒▒   ▒▒   ▒▒  ▒▒▒▒  ▒▒   ▒▒");
            Console.SetCursorPosition(yy, xx + 4);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  ▒▒▒▒▒▒  ▒▒   ▒▒▒  ▒▒     ▒▒  ▒▒  ▒▒  ▒▒▒▒▒▒       ░░  ░░   ░░░░░░  ░░░    ░░░  ░░░░  ░░   ░░░  ░░░░░░ ");

        }
        #endregion

    }

}

