using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp_
{
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
    class RecipeSteps
    {
        public String Descripion { get; set; }

        public RecipeSteps(String descripion)
        {
            Descripion = descripion;
        }
    }
    class Application
    {
        private static List<Ingredients> ingredients = new List<Ingredients>();
        private static List<RecipeSteps> recipeSteps = new List<RecipeSteps>();
        private static List<Ingredients> original = new List<Ingredients>();

        public static void Recipe() {

            int NumberOfIngredients;
            Console.Write("Enter the Number of Ingredients:");
            if(!int.TryParse(Console.ReadLine(), out NumberOfIngredients) ||  NumberOfIngredients <= 0) {
            
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ingredient number. please enter a valid ingredient number.");
                Console.ResetColor();
            }
            try
            {
                
                for(int i = 0; i < NumberOfIngredients; i++)
                {
                    string Name;
                    Console.Write("Enter name of ingredient:");
                    Name = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(Name))
                    {
                        Console.WriteLine("Invalid input. please enter a valid input");
                    }

                    double Quantity;
                    Console.Write("Enter quantity:");
                    if(!double.TryParse(Console.ReadLine(),out Quantity) || Quantity <=0) {

                        Console.WriteLine("Invalid input. please enter a valid number");
                    }
                    string UnitOfMeasurement;
                    Console.Write("Choose option:\n" +
                    "1. Tablespoon\n" +
                    "2. Teaspoon\n" +
                    "3. Cups\n Option: ");

                    int input;
                    if(int.TryParse(Console.ReadLine(), out input) && input >= 1 && input <= 3) {
                    
                    switch(input)
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
                                Console.WriteLine("Invalid Option");
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invilid input. please pick option 1, 2 or 3");
                        UnitOfMeasurement = null;

                    }
                    ingredients.Add(new Ingredients(Name, Quantity, UnitOfMeasurement));

                    original.Add(new Ingredients(Name, Quantity, UnitOfMeasurement));

                }

                int NumberofStep;
                Console.WriteLine("Enter Number of steps:");
                if(!int.TryParse(Console.ReadLine(),out NumberofStep) || NumberofStep <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invaild number of steps. please enter a valid number of Steps");
                    Console.ResetColor();
                }

                for(int i = 0; i < NumberofStep;i++)
                {
                    string Description;
                    Console.Write("Enter the description:");
                    Description = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(Description))
                    {
                        Console.WriteLine("Invalid input. please eneter a valid input");
                    }
                    recipeSteps.Add(new RecipeSteps(Description));
                    Console.WriteLine("Step" + (i + 1) + "Has been saved");
                }
            }catch (FormatException ) {

                Console.WriteLine("Invalid input");
            }

            
        }

        public static void DisplayRecipe() { }

        public static void ScaleRecipe() { }

        public static void ResetQuantity() { }

        public static void ClearAllData() { }
    }
}
