using RecipeAppPoe.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeAppPoe
{
    public static class ApplicationHelper
    {
        private static List<Recipe> recipes = new List<Recipe>();

        public static void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public static List<Recipe> GetAllRecipes()
        {
            return recipes;
        }

        public static void ScaleRecipe(string recipeName, double scaleFactor)
        {
            var recipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (recipe != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    // Scale the quantity based on the factor
                    ingredient.Quantity *= scaleFactor;

                    // Example logic to handle specific units and conditions
                    if (ingredient.Unit == "Teaspoon")
                    {
                        // Example conversion logic
                        if (scaleFactor >= 2.0 && ingredient.Quantity >= 3.0)
                        {
                            ingredient.Quantity /= 3.0;
                            ingredient.Unit = "Tablespoon";
                        }
                    }
                    else if (ingredient.Unit == "Tablespoon")
                    {
                        // Example conversion logic
                        if (scaleFactor >= 2.0 && ingredient.Quantity >= 16.0)
                        {
                            ingredient.Quantity /= 16.0;
                            ingredient.Unit = "Cups";
                        }
                        else if (scaleFactor == 0.5 && ingredient.Quantity < 1.0)
                        {
                            ingredient.Quantity *= 3.0;
                            ingredient.Unit = "Teaspoon";
                        }
                    }
                    else if (ingredient.Unit == "Cups")
                    {
                        // Example conversion logic
                        if (scaleFactor == 0.5 && ingredient.Quantity < 1.0)
                        {
                            ingredient.Quantity *= 16.0;
                            ingredient.Unit = "Tablespoon";
                        }
                    }
                }
            }
        }

        public static void ResetQuantity(string recipeName)
        {
            var recipe = recipes.FirstOrDefault(r => r.Name == recipeName);
            recipe?.ResetQuantities();
        }

        public static void ClearAllData()
        {
            recipes.Clear();
        }
    }
}
