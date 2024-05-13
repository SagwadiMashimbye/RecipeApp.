using NUnit.Framework;
using RecipeApp_;

namespace TotalCalorieCalculation
{
    public class RecipeTests
    {
        [Test]
        public void CalculateTotalCalories_Returns_CorrectTotal()
        {
            // Arrange
            var recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredients("Ingredient1", 100, "Grams", 50, "FoodGroup1"));
            recipe.Ingredients.Add(new Ingredients("Ingredient2", 200, "Grams", 100, "FoodGroup2"));

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(150, totalCalories); // Adjust the expected value based on the actual values of ingredients
        }
    }
}
