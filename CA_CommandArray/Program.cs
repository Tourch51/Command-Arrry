﻿using FinchAPI;
using System;

namespace CommandArray
{
    // *************************************************************
    // Name: Cheung Connor
    // Date: 4/3/19
    // Program: Command Array
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;

            int delayDuration = 0;
            int numberOfCommands = 0;
            int motorSpeed = 0;
            int LEDBrightness = 0;
            FinchCommand[] commands = null;

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) Get Finch Robot Commands");
                Console.WriteLine("\t3) Display Finch Robot Commands");
                Console.WriteLine("\t4) Execute Finch Robot Commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        commands = DisplayGetFinchCommands(numberOfCommands);
                        break;
                    case "3":
                        DisplayFinchCommands(numberOfCommands, commands);
                        break;
                    case "4":
                        DisplayExecuteFinchCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid menu option");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        static void DisplayExecuteFinchCommands(Finch myFinch, FinchCommand[] commands, int motorSpeed, int lEDBrightness, int delayDuration)
        {
            DisplayHeader("Execute Finch Commands");

            Console.WriteLine("Press any key when ready to execute commands.");
            DisplayContinuePrompt();

            for (int index = 0; index < commands.Length; index++)
            {
                Console.WriteLine($"Command: {commands[index]}");

                switch (commands[index])
                {
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        myFinch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        myFinch.wait(delayDuration);
                        break;
                    case FinchCommand.TURNRIGHT:
                        myFinch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        myFinch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        myFinch.setLED(lEDBrightness, lEDBrightness, lEDBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        myFinch.setLED(0, 0, 0);
                        break;
                    default:

                        break;
                }
            }

            DisplayContinuePrompt();
        }

        static FinchCommand[] DisplayGetFinchCommands(int numberOfCommands)
        {
            FinchCommand[] commands = new FinchCommand[numberOfCommands];
            DisplayHeader("Get Finch Commands");


            for (int index = 0; index < numberOfCommands; index++)
            {
                Console.Write($"Command #{index + 1}:");


                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out commands[index]))
                {
                    Console.WriteLine("Please enter a valid command");
                    index = index - 1;
                }
            }

            Console.WriteLine();
            Console.WriteLine("The commands:");
            for (int index = 0; index < numberOfCommands; index++)
            {
                Console.WriteLine($"Command #{index + 1}: {commands[index]}");
            }


            DisplayContinuePrompt();

            return commands;
        }

        static FinchCommand[] DisplayFinchCommands(int numberOfCommands, FinchCommand[] commands)
        {
            DisplayHeader("Display Finch Commands");

            if (commands != null)
            {
                Console.WriteLine("The commands:");
                for (int index = 0; index < numberOfCommands; index++)
                {
                    Console.WriteLine($"Command #{index + 1}: {commands[index]}");
                }
            }
            else
            {
                Console.WriteLine("Please go back and enter commands.");
            }




            DisplayContinuePrompt();

            return commands;
        }

        static int DisplayGetDelayDuration()
        {
            bool validResponse = true;
            int delayDuration;
            string userResponse;

            DisplayHeader("Length of Delay");

            do
            {
                Console.Write("Enter length of delay (milliseconds): ");
                userResponse = Console.ReadLine();
                

                if (!int.TryParse(userResponse, out delayDuration))
                {
                    Console.WriteLine("Please enter a valid response");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);




            DisplayContinuePrompt();

            return delayDuration;
        }

        /// <summary>
        /// get the number of commands from the user
        /// </summary>
        /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
        {
            bool validResponse = true;
            int numberOfCommands;
            string userResponse;

            DisplayHeader("Number of Commands");

            do
            {
                Console.Write("Enter the number of commands:");
                userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out numberOfCommands))
                {
                    Console.WriteLine("Please enter a valid response.");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);




            return numberOfCommands;
        }

        static int DisplayGetMotorSpeed()
        {
            bool validResponse = true;
            int motorSpeed;
            string userResponse;

            DisplayHeader("Motor Speed");



            do
            {
                Console.Write("Enter the motor speed [1-255]:");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out motorSpeed);

                if (!int.TryParse(userResponse, out motorSpeed))
                {
                    Console.WriteLine("Please enter a valid response");
                    validResponse = false;
                }
                if (motorSpeed > 255 || motorSpeed < 1)
                {
                    Console.WriteLine("Please enter a number within the given range.");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);




            return motorSpeed;
        }

        static int DisplayGetLEDBrightness()
        {
            bool validResponse = true;
            int LEDBrightness;
            string userResponse;

            DisplayHeader("LED Brightness");


            do
            {
                Console.Write("Enter the LED Brightness [1-255]:");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out LEDBrightness);

                if (!int.TryParse(userResponse, out LEDBrightness))
                {
                    Console.WriteLine("Please enter a valid response");
                    validResponse = false;
                }
                if (LEDBrightness > 255 || LEDBrightness < 1)
                {
                    Console.WriteLine("Please enter a number within the given range.");
                    validResponse = false;
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            return LEDBrightness;
        }

        /// <summary>
        /// initialize and confirm the finch connects
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
        {
            DisplayHeader("Initialize the Finch");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            while (!myFinch.connect())
            {
                Console.WriteLine("Please confirm the Finch Robot is connect");
                DisplayContinuePrompt();
            }

            FinchConnectedAlert(myFinch);
            Console.WriteLine("Your Finch Robot is now connected");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// audio notification that the finch is connected
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
        {
            myFinch.setLED(0, 255, 0);

            for (int frequency = 17000; frequency > 100; frequency -= 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(10);
            }

            myFinch.noteOff();
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\tProgram Your Finch");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen and disconnect finch robot
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tThank You!");
            Console.WriteLine();

            myFinch.disConnect();

            DisplayContinuePrompt();
        }

        #region HELPER  METHODS

        /// <summary>
        /// display header
        /// </summary>
        /// <param name="header"></param>
        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        /// <summary>
        /// display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        #endregion
    }
}
