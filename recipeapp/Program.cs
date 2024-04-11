﻿using RecipeApp_;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;
            Console.WriteLine("Welcome to My Recipe App!!");
            //do loop
            do  
            {
                //try and catch
                try
                {
                    //displays option for user to pick
                    Console.Write("Choose option:\n" +
                "1. Add Recipe\n" +
                "2. Display Recipe\n" +
                "3. Scale Recipe\n" +
                "4. Reset Quantity\n" +
                "5. Clear All Data\n" +
                "6. Close the app\nOption: ");

                    input = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    //added the switch
                    switch (input)
                    {
                        case 1:
                            Application.Recipe();
                            break;
                        case 2:
                            Application.DisplayRecipe();
                            break;
                        case 3:
                            Application.ScaleRecipe();
                            break;
                        case 4:
                            Application.ResetQuantity();
                            break;
                        case 5:
                            Application.ClearAllData();
                            break;
                        case 6:
                            Console.WriteLine("Thank you for using the app");
                            break;
                        default:
                            Console.WriteLine("Invalid Option\n");
                            break;

                    }
                }
                catch (FormatException)
                {

                    Console.WriteLine("Invalid Option\n");
                    input = -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Option\n");
                    input = -1;
                }
               
                
            }
            while (input != 6);

        }
    }
}
