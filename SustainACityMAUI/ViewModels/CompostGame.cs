using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace SustainACityMAUI.ViewModels
{
class Compost
    {
        public enum WasteType { Fruit, Vegetable, Coffee, Tea, Grass, Leaves, Eggshell, Paper }
        private Dictionary<WasteType, int> waste;
        public Compost()
        {
            waste = new Dictionary<WasteType, int>();
        }

        public void AddWaste(WasteType type, int quantity)
        {
        
            if (waste.ContainsKey(type))
            {
                waste[type] += quantity;
            }
        
            else
            {
                waste.Add(type, quantity);
            }
        }

    
        public int GetQuality()
        {
        
            int quality = 0;
            foreach (var pair in waste)
            {
            
                WasteType type = pair.Key;
                int quantity = pair.Value;

            
                switch (type)
                {
                    case WasteType.Fruit:
                    
                        quality += 2 * quantity;
                        break;

                    case WasteType.Vegetable:
                    
                        quality += 3 * quantity;
                        break;

                    case WasteType.Coffee:
                    
                        quality += 4 * quantity;
                        break;

                    case WasteType.Tea:
                    
                        quality += 2 * quantity;
                        break;

                    case WasteType.Grass:
                    
                        quality -= 1 * quantity;
                        break;

                    case WasteType.Leaves:
                    
                        quality += 1 * quantity;
                        break;

                    case WasteType.Eggshell:
                    
                        quality += 5 * quantity;
                        break;

                    case WasteType.Paper:
                    
                        quality -= 2 * quantity;
                        break;
                }
            }
            return quality;
    
        }
    
        public bool HasBeneficialCreatures()
        {
        
            bool hasBeneficialCreatures = false;

        
            if (waste.ContainsKey(WasteType.Fruit) && waste[WasteType.Fruit] > 0 ||
                waste.ContainsKey(WasteType.Vegetable) && waste[WasteType.Vegetable] > 0 ||
                waste.ContainsKey(WasteType.Coffee) && waste[WasteType.Coffee] > 0 ||
                waste.ContainsKey(WasteType.Tea) && waste[WasteType.Tea] > 0)
            {
                hasBeneficialCreatures = true;
            }

            return hasBeneficialCreatures;
        }

        public bool HasPests()
        {
            bool hasPests = false;

            if (waste.ContainsKey(WasteType.Grass) && waste[WasteType.Grass] > 10 ||
                waste.ContainsKey(WasteType.Paper) && waste[WasteType.Paper] > 10)
            {
                hasPests = true;
            }

            return hasPests;
        }
    }

class Game

{

    private static Random random = new Random();

    private static List<string> sources = new List<string> { "home", "cafe", "park" };

    private static List<Compost.WasteType> wasteTypes = new List<Compost.WasteType>
    {
        Compost.WasteType.Fruit,
        Compost.WasteType.Vegetable,
        Compost.WasteType.Coffee,
        Compost.WasteType.Tea,
        Compost.WasteType.Grass,
        Compost.WasteType.Leaves,
        Compost.WasteType.Eggshell,
        Compost.WasteType.Paper
    };

    private static string GetRandomSource()
    {
        int index = random.Next(sources.Count);

        return sources[index];
    }

    private static Compost.WasteType GetRandomWasteType()
    {
        int index = random.Next(wasteTypes.Count);

        return wasteTypes[index];
    }

    private static int GetRandomQuantity()
    {
        return random.Next(1, 6);
    }

    private static void PrintMessage(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);

            System.Threading.Thread.Sleep(50);
        }

        Console.WriteLine();
    }

    public static void Start()
    {
        PrintMessage("Welcome to the composting mini game!");
        PrintMessage("In this game you will try to create compost.");
        PrintMessage("Your goal is to accumulate the highest compost quality while managing the presence of beneficial creatures and avoiding pests.");
        PrintMessage("You can find different types of waste from various sources, such as homes, cafes, and parks. Each waste type has a different impact on your compost quality.");
        PrintMessage("Adding fruit and vegetable waste increases compost quality, while coffee and tea waste attract beneficial creatures. However, be cautious with grass and paper waste, as they may lead to pests.");
        PrintMessage("Your compost quality is determined by the types and quantities of waste you add. The more diverse and balanced your compost, the higher the quality.");
        PrintMessage("You have 6 options, answer yes(y) or no(n) to add your collected waste and create compost.");
        PrintMessage("At the end of the game, your score will be based on the overall quality of your compost and whether it attracted beneficial creatures or pests.");
        PrintMessage("Good luck!");

        Compost compost = new Compost();

        bool gameOver = false;

        int score = 0;
        int choice = 0;
        int choiceLimit = 6;

        while (!gameOver)
        {
            PrintMessage($"Choice:{choice}/{choiceLimit}");
            PrintMessage($"Score: {score} points");

            string source = GetRandomSource();

            Compost.WasteType wasteType = GetRandomWasteType();

            int quantity = GetRandomQuantity();

            PrintMessage($"You have found {quantity} unit(s) of {wasteType} waste from a {source}.");

            PrintMessage("Do you want to add it to your compost pile? (y/n)");

            bool choose = false;
            while(choose == false)
            {
                string input = Console.ReadLine()!.ToLower();
                if (input == "y")
                {
                    compost.AddWaste(wasteType, quantity);
                    
                    PrintMessage($"You have added {quantity} unit(s) of {wasteType} waste to your compost pile.");

                    choose = true;

                }
                else if (input == "n")
                {

                    PrintMessage("You have skipped this waste.");

                    choose = true;

                }
                else
                {
                    PrintMessage("Invalid input. Please enter y or n.");


                }

            }

            choice++;

            if (choice>=choiceLimit)
            {
                gameOver = true;
            }
            
        }

        PrintMessage("The game is over!");

        int quality = compost.GetQuality();

        PrintMessage($"Your compost has a quality of {quality}.");

        if (compost.HasBeneficialCreatures())
        {
            PrintMessage("Your compost pile attracts beneficial creatures like worms and insects!");

            score += 10;
        }

        if (compost.HasPests())
        {
            PrintMessage("Your compost pile attracts pests like rats and flies!");

            score -= 10;
        }

            PrintMessage($"Your final score is {score} points.");

            PrintMessage("Thank you for playing the composting mini game!");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
    }
    public static void Main(string[] args)
    {
        Start();
    }

}
}



