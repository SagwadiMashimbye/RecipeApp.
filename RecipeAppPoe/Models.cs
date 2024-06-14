namespace RecipeAppPoe.Models
{
    public class Ingredient
    {
        private double originalQuantity;

        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient()
        {
            originalQuantity = Quantity;
        }

        public void ResetQuantity()
        {
            Quantity = originalQuantity;
        }
        public void SetOriginalQuantity()
           {
                originalQuantity = Quantity;
            }
        }

    }

namespace RecipeAppPoe.Models
{
    public class RecipeStep
    {
        public string Description { get; set; }
    }
}

namespace RecipeAppPoe.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipeStep> Steps { get; set; } // Ensure this property exists

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<RecipeStep>();
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ResetQuantity();
            }
        }
    }
}
