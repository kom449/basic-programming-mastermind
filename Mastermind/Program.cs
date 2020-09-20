using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mastermind{

    class Program{

        //variables that is used betweens methods
        public static int nmbstoint = 0;
        public static char[] stringchars;

        static void Main(){
            Console.Clear();
            Console.WriteLine("Do you wanna play some Mastermind?\n\nY/N");
            string input =Console.ReadLine();
            if (input.ToLower() == "y")
                settings();

            //if user doesn't wanna play, commit suduku
            else if (input.ToLower() == "n")
                Environment.Exit(1);
            
            //if the input is anything else than y/n, rerun the program
            else
                Main();
        }

        //settings for the game. Might add more options later but CBA...
        static void settings(){
            Console.Clear();
            Console.WriteLine("How many numbers do you want? (min 3, max 10!)");
            string nmbs = Console.ReadLine();
            try{
                nmbstoint = int.Parse(nmbs);
                if (nmbstoint >= 3 && nmbstoint <= 10){
                    //combination generation
                    var chars = "123456789";
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
            catch{
                Console.WriteLine("Invalid input!");
            }
        }

        //game logic
        static void Game(){
            Console.Clear();
            bool playing = true;
            bool validinput;
            int lives = 9;

            //run the game in a loop so it doesn't end unless user wins or runs out of lives
            while(playing){
                Console.WriteLine("\n\nPlease enter "+ nmbstoint +" digits\nYou currently have "+lives+" lives left!\n");
                string input = Console.ReadLine();
                //first line of error checking. Making sure the input is only 4 numbers
                if (input.Length != nmbstoint){
                    Console.WriteLine("Input was not valid!\n");
                    validinput = false;
                }
                else
                    validinput = true;

                //if the input length is nmbstoint, continue
                if (validinput){
                    try{
                        //checking to see if input is just numbers
                        int toints = int.Parse(input);

                        //converting our input back to string then chararray
                        char[] intarray = toints.ToString().ToCharArray();

                        //int x for counting in our char array
                        int x = 0;

                        //easier way of writing chars to a string
                        StringBuilder sbcorrect = new StringBuilder("Correct combination in your guess: ");
                        StringWriter swcorrect = new StringWriter(sbcorrect);
                        StringWriter sw = new StringWriter();
                        string result = "";

                        //going through each char that was created in the beginning and comparing them with user input
                        foreach (char ch in stringchars){
                            if (ch == intarray[x]){
                                Console.WriteLine(intarray[x] + " Was correct!");
                                swcorrect.Write(intarray[x]);
                            }
                            else if (ch != intarray[x]){
                                Console.WriteLine(intarray[x] + " Was not correct!");
                                swcorrect.Write("x");
                            }
                            //if our generated combination contains one of the numbers that the user typed, write it to a string, convert string to array
                            //remove duplicates and convert back to string
                            if(stringchars.Contains(intarray[x])){
                                sw.Write(intarray[x]);
                                string unique = sw.ToString();
                                var uniquearray = unique.ToCharArray().Distinct().ToArray();
                                result = new string(uniquearray);
                            }
                            x++;
                        }
                        Console.WriteLine(sbcorrect);
                        Console.WriteLine("Numbers in the combination that were correct: "+result);

                        //creating a bool that is true if both arrays are the same
                        bool equal = stringchars.SequenceEqual(intarray);
                        //Console.WriteLine("Input {0} equal", equal ? "are" : "are not");

                        //if arrays are equal, end game
                        if (equal == true){
                            playing = false;
                            Console.WriteLine("You found the correct answer!\nYou have won the game!");
                            Console.ReadLine();
                        }

                        //subtracting a try each time, if you run out of tries, end game.
                        lives--;
                        if (lives == 0){
                            playing = false;
                            Console.WriteLine("You ran out of lives!");
                            Console.ReadLine();
                        }
                    }
                    //error handling for the parsing, just to make sure it only contained numbers
                    catch{
                        Console.WriteLine("Input was not valid!");
                        continue;
                    }
                }             
            }
        }
    }
}
