using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mastermind{

    class Program{

        public static int nmbstoint = 0;
        public static char[] stringchars;

        static void Main(){
            Console.Clear();
            Console.WriteLine("Do you wanna play some Mastermind?\n\nY/N");
            string input =Console.ReadLine();
            if (input.ToLower() == "y")
                settings();

            else if (input.ToLower() == "n")
                Environment.Exit(1);

            else
                Main();
        }

        static void settings()
        {
            Console.Clear();
            Console.WriteLine("How many numbers do you want? (min 3, max 10!)");
            string nmbs = Console.ReadLine();
            try{
                nmbstoint = int.Parse(nmbs);
                if (nmbstoint >= 3 && nmbstoint <= 10){
                    //combination generation
                    var chars = "0123456789";
                    stringchars = new char[nmbstoint];
                    Random rand = new Random();
                    for (int i = 0; i < stringchars.Length; i++)
                        stringchars[i] = chars[rand.Next(chars.Length)];                   
                    Game();
                }
                else{
                    Console.WriteLine("Input out of range!\nPress enter to try again");
                    Console.ReadLine();
                    settings();
                }
            }
            catch (Exception){
                Console.WriteLine("Invalid input!");
            }
        }

        static void Game(){
            Console.Clear();
            bool playing = true;
            bool validinput;
            int tries = 9;

            while(playing){
                Console.WriteLine("Please enter "+ nmbstoint +" digits\nYou currently have "+tries+" lives left!\n");
                string input = Console.ReadLine();

                //first line of error checking. Making sure the input is only 4 numbers
                if (input.Length != nmbstoint){
                    Console.WriteLine("Input was not valid!\n");
                    validinput = false;
                }
                else
                    validinput = true;

                //if the input length is 4, continue
                if (validinput){
                    try{

                        //checking to see if input is just numbers
                        int toints = int.Parse(input);

                        //converting our input back to string then chararray
                        char[] intarray = toints.ToString().ToCharArray();
                        int x = 0;
                        StringBuilder sb = new StringBuilder("Correct numbers in your guess: ");
                        StringWriter sw = new StringWriter(sb);

                        //going through each char that was created in the beginning and comparing them with user input
                        foreach (char ch in stringchars){
                            if (ch == intarray[x]){
                                Console.WriteLine(intarray[x] + " Was correct!");
                                sw.Write(intarray[x]);
                            }
                            else if (ch != intarray[x]){
                                Console.WriteLine(intarray[x] + " Was not correct!");
                                sw.Write("x");
                            }
                            x++;
                        }
                        Console.WriteLine(sb);

                        //creating a bool that is true if both arrays are the same
                        bool equal = stringchars.SequenceEqual(intarray);
                        Console.WriteLine("Input {0} equal", equal ? "are" : "are not");

                        //if arrays are equal, end game
                        if (equal == true){
                            playing = false;
                            Console.WriteLine("You found the correct answer!\nYou have won the game!");
                            Console.ReadLine();
                        }

                        //subtracting a try each time, if you run out of tries, end game.
                        tries--;
                        if (tries == 0){
                            playing = false;
                            Console.WriteLine("You ran out of lives!");
                            Console.ReadLine();
                        }
                    }

                    //error handling for the parsing, just to make sure it only contained numbers
                    catch (Exception ex){
                        //Console.WriteLine(ex);
                        Console.WriteLine("Input was not valid!");
                        continue;
                    }
                }             
            }
        }
    }
}
