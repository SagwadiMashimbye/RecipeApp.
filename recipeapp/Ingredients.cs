using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp_
{
    //added getter and setters
    public class Ingredients
    {
        public String Name { get; set; }
        public double Quantity { get; set; }
        public String UnitOfMeasurement { get; set; }
        public double Calories { get; set; }
        public String FoodGroup { get; set; }


        public Ingredients(String name, double quantity, String unitofmeasurement, double calories, string foodGroup)
        {

            Name = name;
            Quantity = quantity;
            UnitOfMeasurement = unitofmeasurement;
            FoodGroup = foodGroup;
            Calories = calories;

        }

    }
        //added getter and setter for this class
        public class RecipeSteps
        {
            public String Descripion { get; set; }
            

            public RecipeSteps(String descripion)
            {
                Descripion = descripion;
                

            }
        }
        public class Recipe
        {
            public String Name { get; set; }
            public List<Ingredients> Ingredients { get; set;}
            public List<RecipeSteps> RecipeSteps { get; set; }

            public double CalculateTotalCalories()
            {
                double totalCalories = 0;
                foreach (var ingredient in Ingredients)
                {
                    totalCalories += ingredient.Calories;
                }
                return totalCalories;
            }
            public Recipe()
            {
                Ingredients = new List<Ingredients>();
                RecipeSteps = new List<RecipeSteps>();
            }
        }

        //array list
        public class Application
        {
            private static List<Ingredients> ingredients = new List<Ingredients>();
            private static List<RecipeSteps> recipeSteps = new List<RecipeSteps>();
            private static List<Ingredients> original = new List<Ingredients>();
            private static List<Recipe> recipes = new List<Recipe>();


            //prompt user to enter recipe details
            public static void Recipe()
            {
                Recipe recipe = new Recipe();

                //prompt user to enter the name of the recipe
                Console.Write("Enter the name of the recipe: ");
                recipe.Name = Console.ReadLine();


                //prompts user to input the number of ingredients that will be used
                int NumberOfIngredients;
                do
                {
                    Console.Write("Enter the Number of Ingredients: ");
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
                    for (int i = 0; i < NumberOfIngredients; i++)
                    {
                        //Input ingredient details
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. please enter a valid number");
                                Console.ResetColor();
                            }
                        } while (Quantity <= 0);

                        //Prompt for calories
                        Console.Write("Enter number of calories: ");
                        Console.WriteLine();
                        int Calories;
                        while(!int.TryParse(Console.ReadLine(), out Calories) || Calories < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. Please enter a valid number of calories.");
                            Console.ResetColor();
                            Console.Write("Enter number of calories: ");
                        }
                       

                        // check the total calories calculated in step 1 against the threshold of 300
                        // display a notification if the total calories exceed 300
                        double totalCalories = recipe.CalculateTotalCalories();

                        if (totalCalories > 300)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Warning: Total calories exceed 300!");
                            Console.ResetColor();
                        }

                    //prompt for food group
                     string FoodGroup;
                    do
                    {
                        Console.Write("please select a food group:" +
                            "\n1. Starchy food" +
                            "\n2. Vegatables and Fruits" +
                            "\n3. Dry beans, Peas, Lentils and Soya" +
                            "\n4. Chicken, Fish, Meat and Eggs" +
                            "\n5. Milk and Daily products" +
                            "\n6. Fats and Oils" +
                            "\n7. Water" +
                            "\nOption: ");
                        Console.WriteLine();
                        int foodGroup;
                        if (int.TryParse(Console.ReadLine(), out foodGroup) && foodGroup >= 1 && foodGroup <= 7)
                        {
                            
                            switch(foodGroup)
                            {
                                case 1:
                                    FoodGroup = "Starchy food";
                                    break;
                                case 2:
                                    FoodGroup = "Vegatibales amd Fruits";
                                    break;
                                case 3:
                                    FoodGroup = "Dry beans, Peas, Lentils and Soya";
                                    break;
                                case 4:
                                    FoodGroup = "Chicken, Fish, Meat and Eggs";
                                    break;
                                case 5:
                                    FoodGroup = "Milk and Daily products";
                                    break;
                                case 6:
                                    FoodGroup = " Fats and Oils";
                                    break;
                                case 7:
                                    FoodGroup = "Water";
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
                            Console.WriteLine("Invilid input. please pick option 1, 2, 3, 4, 5, 6 or 7");
                            Console.ResetColor();
                            FoodGroup = null;
                        }
                    }while(FoodGroup == null);
                    
                        //options to choose units of measurement
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
                                Console.ResetColor();
                                UnitOfMeasurement = null;

                            }

                        } while (UnitOfMeasurement == null);

                    //array list
                        Ingredients ingredient = new Ingredients(Name, Quantity, UnitOfMeasurement, Calories, FoodGroup );
                        recipe.Ingredients.Add(ingredient);
                        ingredients.Add(ingredient);
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("Successfully added!");
                        Console.ResetColor();
                        original.Add(new Ingredients(Name, Quantity, UnitOfMeasurement, Calories, FoodGroup));
                      
                }
                    //add steps
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
                    } while (NumberofStep <= 0);


                    //for loop to store steps after being input by the user
                    for (int i = 0; i < NumberofStep; i++)
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

                        RecipeSteps recipeStep = new RecipeSteps(Description);
                        recipe.RecipeSteps.Add(recipeStep);
                        recipeSteps.Add(recipeStep);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Step" + (i + 1) + " Has been saved");
                        Console.ResetColor();
                        
                    }

                        recipes.Add(recipe);
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("Successfully added!");
                        Console.ResetColor();
                }
                catch (FormatException)
                {
                Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input");
                    Console.ResetColor();
                }


            }

           

        public static void ChooseRecipeToDisplay()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes added");
                Console.ResetColor();
                return;
            }

            // Sort recipes alphabetically by name
            recipes = recipes.OrderBy(r => r.Name).ToList();

            // Display list of recipes with index numbers
            Console.WriteLine("List of Recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            // Prompt user to choose a recipe
            Console.Write("Enter the number of the recipe to display: ");
            if (int.TryParse(Console.ReadLine(), out int recipeIndex) && recipeIndex >= 1 && recipeIndex <= recipes.Count)
            {
                // Display selected recipe
                Recipe selectedRecipe = recipes[recipeIndex - 1];
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Recipe: {selectedRecipe.Name}");

                // Calculate total calories for the recipe
                double totalCalories = selectedRecipe.CalculateTotalCalories();
                Console.WriteLine($"Total Calories: {totalCalories}");

                // Notify if total calories exceed 300
                if (totalCalories > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Warning: Total calories exceed 300!");
                    Console.ResetColor();
                }
                    // Display ingredients
                    foreach (Ingredients ingredient in selectedRecipe.Ingredients)
                {
                    Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name}");
                }

                // Display recipe steps
                Console.WriteLine("\nSteps:");
                for (int i = 0; i < selectedRecipe.RecipeSteps.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {selectedRecipe.RecipeSteps[i].Descripion}");
                }
                // Display food group for each ingredients
                Console.WriteLine("\nFood Group:");
                foreach (Ingredients ingredient in selectedRecipe.Ingredients)
                {
                    Console.WriteLine($"{ingredient.Name}: {ingredient.FoodGroup}");
                }
                Console.WriteLine("------------------------------------");

                // Prompt user to continue
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid recipe number.");
                Console.ResetColor();
            }
        }

        //method scales recipe by the factor 0.5, 2 or 3
        public static void ScaleRecipe()
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (ingredients.Count == 0 && recipeSteps.Count == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)ConsoleColor.Red;
                    Console.WriteLine("No recipe added");
                    Console.ResetColor();
                    return;
                }
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
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Recipe scaled by {Factor}.");
                            Console.ResetColor();
                            break;
                        case 2:
                            Factor = 2.0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Recipe scaled by {Factor}.");
                            Console.ResetColor();
                            break;
                        case 3:
                            Factor = 3.0;
                            Console.ForegroundColor = ConsoleColor.Green;
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
                foreach (Ingredients ingredient in ingredients)
                {
                    if (ingredient.UnitOfMeasurement == "Teaspoon")
                    {
                        ingredient.Quantity *= Factor;
                        if (Factor >= 2.0 && ingredient.Quantity >= 3.0)
                        {
                            ingredient.Quantity /= 3.0;
                            ingredient.UnitOfMeasurement = "Tablespoon";
                        }
                    }
                    else if (ingredient.UnitOfMeasurement == "Tablespoon")
                    {
                        ingredient.Quantity *= Factor;
                        if (Factor >= 2.0 && ingredient.Quantity >= 16.0)
                        {
                            ingredient.Quantity /= 16.0;
                            ingredient.UnitOfMeasurement = "Cups";
                        }
                        else if (Factor == 0.5 && ingredient.Quantity < 1.0)
                        {
                            ingredient.Quantity *= 3.0;
                            ingredient.UnitOfMeasurement = "Teaspoon";
                        }
                    }
                    else if (ingredient.UnitOfMeasurement == "Cups")
                    {
                        ingredient.Quantity *= Factor;
                        if (Factor == 0.5 && ingredient.Quantity < 1.0)
                        {
                            ingredient.Quantity *= 16.0;
                            ingredient.UnitOfMeasurement = "Tablespoon";
                        }
                    }
                    else
                    {
                        ingredient.Quantity *= Factor;
                    }
                }
            }
            //method to reset ingredient quantity back to their original values after being scaled
            public static void ResetQuantity()
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (ingredients.Count == 0 && recipeSteps.Count == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)ConsoleColor.Red;
                    Console.WriteLine("No recipe added");
                    Console.ResetColor();
                    return;
                }
                //for loop 
                for (int i = 0; i <ingredients.Count; i++)
                {
                   ingredients[i].Quantity = original[i].Quantity;
                    ingredients[i].UnitOfMeasurement = original[i].UnitOfMeasurement;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Has been reset");
                Console.ResetColor();

            }
            //method which clears recipe that is enterd by the user
            public static void ClearAllData()
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (ingredients.Count == 0 && recipeSteps.Count == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)ConsoleColor.Red;
                    Console.WriteLine("No recipe added");
                    Console.ResetColor();
                    return;
                }
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
                            recipes.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Recipe cleared.");
                            Console.ResetColor();
                            break;
                        case 2:

                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Option.");
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


