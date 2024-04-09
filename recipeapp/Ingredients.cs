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

        public static void Recipe() { }


    }
}
