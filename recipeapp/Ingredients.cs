using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp_
{
    //added getter and setters
    internal class Ingredients
    {
        public String Name {  get; set; }
        public double Quantity { get; set; }
        public String UnitofMeasurement { get; set; }

        public Ingredients(String name, double quantity, String unitofmeasurement) { 
        
            Name = name;
            Quantity = quantity;
            UnitofMeasurement = unitofmeasurement;
        }
    }
    //added getter and setter for this class
    class RecipeSteps
    {
        public String Descripion { get; set; }

        public RecipeSteps(String descripion)
        {
            Descripion = descripion;
        }
    }
    //array list
    class Application
    {
        private static List<Ingredients> ingredients = new List<Ingredients>();
        private static List<RecipeSteps> recipeSteps = new List<RecipeSteps>();
        private static List<Ingredients> original = new List<Ingredients>();

        //prompt user to enter recipe details
        public static void Recipe() {

            //prompts user to input the number of ingredients that will be used
            int NumberOfIngredients;
            do
            {
                Console.Write("Enter the Number of Ingredients:");
                if (!int.TryParse(Console.ReadLine(), out NumberOfIngredients) || NumberOfIngredients <= 0)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ingredient number. please enter a valid ingredient number.");
                    Console.ResetColor();
                }
            } while (NumberOfIngredients <= 0);
            
            try
            {
                
                //for loop to store ingredients after being input by the user
                for(int i = 0; i < NumberOfIngredients; i++)
                {
                    string Name;
                    do
                    {
                        Console.Write("Enter name of ingredient:");
                        Name = Console.ReadLine();
                        //if statement
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. please enter a valid input");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrWhiteSpace(Name));
                   

                    double Quantity;
                    do
                    {
                        Console.Write("Enter quantity:");
                        if (!double.TryParse(Console.ReadLine(), out Quantity) || Quantity <= 0)
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine("Invalid input. please enter a valid number");
                            Console.ResetColor();
                        }
                    }while(Quantity <= 0);
                    
                    //options to choose 
                    string UnitOfMeasurement;
                    do
                    {
                        Console.Write("Choose option:\n" +
                                            "1. Tablespoon\n" +
                                            "2. Teaspoon\n" +
                                            "3. Cups\nOption: ");

                        int input;
                        if (int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 3)
                        {
                            //added the switch
                            switch (input)
                            {
                                case 1:
                                    UnitOfMeasurement = "Tablespoon";
                                    break;
                                case 2:
                                    UnitOfMeasurement = "Teaspoon";
                                    break;
                                case 3:
                                    UnitOfMeasurement = "Cups";
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red; 
                                    Console.WriteLine("Invalid Option");
                                    Console.ResetColor();
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invilid input. please pick option 1, 2 or 3");
                            Console.ResetColor ();
                            UnitOfMeasurement = null;

                        }
                    } while (UnitOfMeasurement == null);

                    //array list
                    ingredients.Add(new Ingredients(Name, Quantity, UnitOfMeasurement));
                    original.Add(new Ingredients(Name, Quantity, UnitOfMeasurement));

                }
                //prompts user to input the number of steps that will be used
                int NumberofStep;
                do
                {
                    Console.Write("Enter Number of steps:");
                    if (!int.TryParse(Console.ReadLine(), out NumberofStep) || NumberofStep <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invaild number of steps. please enter a valid number of Steps");
                        Console.ResetColor();
                    }
                }while(NumberofStep <= 0);
               

                //for loop to store steps after being input by theuser
                for(int i = 0; i < NumberofStep;i++)
                {
                    string Description;
                    do
                    {
                        Console.Write("Enter the description:");
                        Description = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(Description))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. please eneter a valid input");
                            Console.ResetColor();
                        }
                    } while (string.IsNullOrWhiteSpace(Description));
                   
                    recipeSteps.Add(new RecipeSteps(Description));
                    Console.WriteLine("Step" + (i + 1) + " Has been saved");
                }
            }catch (FormatException ) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input");
                Console.ResetColor();
            }

            
        }

        public static void DisplayRecipe() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------");
            
            //for loop to display recipe after being input by the user
            Console.WriteLine("Recipe:");
            foreach (Ingredients ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitofMeasurement} of {ingredient.Name}");

            }
            //for loop to display recipe after being input by the user
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < recipeSteps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeSteps[i].Descripion}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.ResetColor();
        

    }
        //method scales recipe by the factor 0.5, 2 or 3
        public static void ScaleRecipe() {
            //option to choose for scaling
            Console.Write("Choose a scaling option:\n" +
                    "1. Half\n" +
                    "2. Double\n" +
                    "3. Triple\n" +
                    "4. Back\nOption: ");

            int input;
            double Factor;
            if (int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 4)
            {
                //added the switch
                switch (input)
                {
                    case 1:
                        Factor = 0.5;
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine($"Recipe scaled by {Factor}.");
                        Console.ResetColor();
                        break;
                    case 2:
                        Factor = 2.0;
                        Console.ForegroundColor= ConsoleColor.Green;    
                        Console.WriteLine($"Recipe scaled by {Factor}.");
                        Console.ResetColor();
                        break;
                    case 3:
                       Factor  = 3.0;
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine($"Recipe scaled by {Factor}.");
                        Console.ResetColor();
                        break;
                    case 4:
                        Factor = 1.0;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Option");
                        Console.ResetColor();
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invilid input. please enter a valid input.");
                Console.ResetColor();
               Factor = 1.0;

            }
            //for loop
            foreach(Ingredients ingredient in ingredients)
            {
                if(ingredient.UnitofMeasurement == "Teaspoon")
                {
                    ingredient.Quantity *= Factor;
                    if(Factor >= 2.0 && ingredient.Quantity >= 3.0)
                    {
                        ingredient.Quantity /= 3.0;
                        ingredient.UnitofMeasurement = "Tablespoon";
                    }
                }
                else if(ingredient.UnitofMeasurement == "Tablespoon")
                {
                    ingredient.Quantity *= Factor;
                    if(Factor >= 2.0 && ingredient.Quantity >= 16.0)
                    {
                        ingredient.Quantity /= 16.0;
                        ingredient.UnitofMeasurement = "Cups";
                    }
                    else if(Factor == 0.5 &&  ingredient.Quantity < 1.0)
                    {
                        ingredient.Quantity *= 3.0;
                        ingredient.UnitofMeasurement = "Teaspoon";
                    }
                }
                else if(ingredient.UnitofMeasurement == "Cups")
                {
                    ingredient.Quantity *= Factor;
                    if(Factor == 0.5 && ingredient.Quantity < 1.0)
                    {
                        ingredient.Quantity *= 16.0;
                        ingredient.UnitofMeasurement = "Tablespoon";
                    }
                }
                else
                {
                    ingredient.Quantity *= Factor;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe has been scaled");
            Console.ResetColor();
            Console.WriteLine();
        }
//method to reset ingredient quantity back to their original values after being scaled
        public static void ResetQuantity() {
            //for loop 
            for(int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity = original[i].Quantity;
                ingredients[i].UnitofMeasurement = original[i].UnitofMeasurement;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Has been reset");
            Console.ResetColor();

        }
//method which clears recipe that is enterd by the user
        public static void ClearAllData() {
            //option that the user will choose to reset the recipe
            Console.Write("Are you sure you want to reset:\n" +
                    "1. Yes\n" +
                    "2. No\n" +
                    "Option: ");

            int input;
            if (int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 2)
            {
                //added switch
                switch (input)
                {
                    case 1:
                        recipeSteps.Clear();
                        original.Clear();
                        ingredients.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Recipe cleared.");
                        Console.ResetColor();
                        break;
                    case 2:

                        break;
                    default:
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Invalid Option");
                        Console.ResetColor();
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invilid input. please enter valid input.");
                Console.ResetColor();

            }
        }
    }
}
