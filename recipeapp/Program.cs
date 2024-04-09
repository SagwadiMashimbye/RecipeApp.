using RecipeApp_;
using System;
using System.Collections;
using System.Diagnostics;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to My Recipe App!!");
            while (true) {
                Console.Write("Choose option:\n" +
                "1. Add Recipe\n" +
                "2. Display Recipe\n" +
                "3. Scale Recipe\n" +
                "4. Reset Quantity\n" +
                "5. Clear All Data\n Option: ");

                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Application.Recipe();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                        
                }
            }
            
        }
    }
}
