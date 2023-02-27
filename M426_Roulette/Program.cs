using System;
using System.Collections.Generic;

namespace Roulette
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the numbers on the roulette wheel and their corresponding colors
            Dictionary<int, string> wheel = new Dictionary<int, string>()
            {
                { 0, "green" },
                { 32, "red" },
                { 15, "black" },
                { 19, "red" },
                { 4, "black" },
                { 21, "red" },
                { 2, "black" },
                { 25, "red" },
                { 17, "black" },
                { 34, "red" },
                { 6, "black" },
                { 27, "red" },
                { 13, "black" },
                { 36, "red" },
                { 11, "black" },
                { 30, "red" },
                { 8, "black" },
                { 23, "red" },
                { 10, "black" },
                { 5, "red" },
                { 24, "black" },
                { 16, "red" },
                { 33, "black" },
                { 1, "red" },
                { 20, "black" },
                { 14, "red" },
                { 31, "black" },
                { 9, "red" },
                { 22, "black" },
                { 18, "red" },
                { 29, "black" },
                { 7, "red" },
                { 28, "black" },
                { 12, "red" },
                { 35, "black" },
                { 3, "red" },
                { 26, "black" }
            };

            // Define the payout ratios for each type of bet
            Dictionary<string, double> payoutRatios = new Dictionary<string, double>()
            {
                { "single", 35 },
                { "color", 1 },
                { "even", 1 },
                { "odd", 1 },
            };

            // Define the available betting options and their corresponding payout ratios
            Dictionary<string, List<int>> bettingOptions = new Dictionary<string, List<int>>()
            {
                { "single", new List<int>() { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 } },
                { "color", new List<int>() { 0 } },
                { "even", new List<int>() { 0 } },
                { "odd", new List<int>() { 0 } },
            };

            // Ask the user for their starting balance
            Console.Write("Enter your starting balance: ");
            double balance = double.Parse(Console.ReadLine());

            // Start the game loop
            while (true)
            {
                // Print the current balance
                Console.WriteLine("Your current balance is");
                                Console.WriteLine(balance);

                // Ask the user for their bet type and amount
                Console.Write("Enter your bet type (single/color/even/odd): ");
                string betType = Console.ReadLine();
                Console.Write("Enter your bet amount: ");
                double betAmount = double.Parse(Console.ReadLine());

                // Check if the user's bet amount is within their balance
                if (betAmount > balance)
                {
                    Console.WriteLine("You don't have enough balance to make this bet.");
                    continue;
                }

                // Ask the user for their bet selection
                List<int> validNumbers = bettingOptions[betType];
                Console.Write($"Enter your {betType} bet ({string.Join("/", validNumbers)}): ");
                int betSelection = int.Parse(Console.ReadLine());

                // Check if the user's bet selection is valid
                if (!validNumbers.Contains(betSelection))
                {
                    Console.WriteLine("Invalid bet selection.");
                    continue;
                }

                // Spin the roulette wheel and determine the winning number and color
                Random random = new Random();
                int winningNumber = random.Next(0, 37);
                string winningColor = wheel[winningNumber];

                // Determine the user's payout based on their bet type and selection
                double payoutRatio = payoutRatios[betType];
                double payoutAmount = 0;
                if (betType == "single" && betSelection == winningNumber)
                {
                    payoutAmount = betAmount * payoutRatio;
                }
                else if (betType == "color" && betSelection == 0 && wheel[winningNumber] == "green")
                {
                    payoutAmount = betAmount * payoutRatio;
                }
                else if (betType == "color" && betSelection != 0 && (wheel[winningNumber] == "red" || wheel[winningNumber] == "black"))
                {
                    string selectedColor = betSelection == 1 ? "red" : "black";
                    if (selectedColor == wheel[winningNumber])
                    {
                        payoutAmount = betAmount * payoutRatio;
                    }
                }

                else if (betType == "even" && betSelection == 0 && winningNumber % 2 == 0 && winningNumber != 0)
                {
                    payoutAmount = betAmount * payoutRatio;
                }
                else if (betType == "odd" && betSelection == 0 && winningNumber % 2 == 1)
                {
                    payoutAmount = betAmount * payoutRatio;
                }

                // Update the user's balance
                balance += payoutAmount - betAmount;

                // Print the results of the spin
                Console.WriteLine($"The winning number is {winningNumber} ({winningColor}).");
                if (payoutAmount > 0)
                {
                    Console.WriteLine($"You won {payoutAmount}!");
                }
                else
                {
                    Console.WriteLine("Sorry, you lost.");
                }

                // Ask the user if they want to continue playing
                Console.Write("Do you want to continue playing? (y/n) ");
                string answer = Console.ReadLine();
                if (answer.ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}
