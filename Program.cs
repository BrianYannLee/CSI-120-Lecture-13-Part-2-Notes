
//Programmer: Brian Lee
//Date: 05/30/2024

//Title: CSI 120 Lecture 13 Part 2 Notes
//-------------------------------------------------------------------------------

using System.Reflection.Emit;
using System.Text.RegularExpressions;

//----------------------------------------------------------------------------
namespace CSI_120_Lecture_13_Part_2_Notes
{
    internal class Program
    {
        //-----------Preload--------------------------
        public static FoodItem[] foodItems = new FoodItem[5];
        public static void PreloadData()
        {
            foodItems[0] = new FoodItem("Apple", 7, 95);
            foodItems[1] = new FoodItem("Banana", 4, 105);
            foodItems[2] = new FoodItem("Chicken Breast", 8, 165);
            foodItems[3] = new FoodItem("Broccoli", 5, 55);
            foodItems[4] = new FoodItem("Almonds", 7, 70);
        }//end of PreloadData(method)
        //-------------------------------------------
        static void Main(string[] args)
        {
            //------Preload------
            bool exitProgram = false;
            PreloadData();
            //-------------------

            do
            {
                exitProgram = MenuSelection.MainMenu(foodItems);
                Console.WriteLine($"Main: FoodItem Size {foodItems.Length}");
            } while (!exitProgram);
        }//end of Main
    }//end of Program (class)
    public class InputChecker
    {
        public static int MenuChecker(int firstChoice, int lastChoice)
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < firstChoice || userInput > lastChoice)
            {
                Console.WriteLine("Inalid Input. Try Again.");
            }
            Console.WriteLine();
            return userInput;
        }//end of MenuChecker(method)
        public static int IntChecker()
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Invalid Input. Try Again");
            }
            return userInput;
        }//end of IntChecker(method)
        public static double DoubleChecker()
        {
            double userInput;
            while (!double.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Invalid Input. Try Again");
            }
            return userInput;
        }//end of DoubleChecker(method)
        public static string StringChecker()
        {
            string? userInput;
            string pattern = @"^[a-zA-Z]+$";
            while ((userInput = Console.ReadLine()) != null && !Regex.IsMatch(userInput, pattern))
            {
                Console.WriteLine("Invalid Input. Try Again");
            }
            return userInput ?? string.Empty;
        }//end of StringChecker(method)

    }//end of InputChecker(class)
    public class MenuSelection
    {
        public static bool MainMenu(FoodItem[] foodItems)
        {
            const int firstChoice = 1;
            const int lastChoice = 3;
            int userInput;
            bool exitProgram = false;

            Console.WriteLine("Please Select Method");
            Console.WriteLine("1. Total Calories");
            Console.WriteLine("2. Add Food");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            userInput = InputChecker.MenuChecker(firstChoice, lastChoice);

            switch (userInput)
            {
                case 1:
                    Console.WriteLine("Total Calories");
                    Console.WriteLine("-------------------");
                    MethodList.TotalCalories(foodItems);
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("Add Food");
                    Console.WriteLine("-------------------");
                    MethodList.AddFood(foodItems);
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine("Exiting Program");
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("An Error has occured in MainMenu");
                    break;
            }
            return exitProgram;
        }//end of MainMenu(method)

    }//end of MenuSelection(class)
    public class MethodList
    {
        public static void TotalCalories(FoodItem[] foodItems)
        {
            Console.WriteLine(foodItems.Length);
            double[] totalCalories = new double[foodItems.Length];
            for (int i = 0; i < foodItems.Length; i++)
            {
                totalCalories[i] = foodItems[i].Qty * foodItems[i].Calories;
            }
            Console.WriteLine("Total Calories Calculated");
            Console.WriteLine();
            DisplayInfo(foodItems);
        }//end of TotalCalories(method)
        public static void DisplayInfo(FoodItem[] foodItems)
        {
            for (int i = 0; i < foodItems.Length; i++)
            {
                var item = foodItems[i];
                double itemTotalCalories = item.Qty * item.Calories;
                Console.WriteLine($"{item.Name} | {item.Qty} | {item.Calories} | {itemTotalCalories}");
            }
        }//end of DisplayInfo(method)

        public static void AddFood(FoodItem[] foodItems)
        {
            int index;
            string userName;
            double userQty;
            double userCalories;
            Console.WriteLine("Type the Name of the Food");
            userName = InputChecker.StringChecker();
            index = FindName(foodItems, userName);
            Console.WriteLine(index);
            if (index != -1)
            {
                Console.WriteLine("The Food is already on the list");
            }
            else
            {
                Console.WriteLine("Type the Quantity");
                userQty = InputChecker.DoubleChecker();
                Console.WriteLine();
                Console.WriteLine("Type the Calories");
                userCalories = InputChecker.DoubleChecker();

                index = FindEmptySlot(foodItems);
                if (index != -1)
                {
                    foodItems[index] = new FoodItem(userName, userQty, userCalories);
                    Console.WriteLine("Food Item Added Successfully");
                }
                else
                {
                    foodItems = DoubleArray(foodItems);
                    Console.WriteLine($"Lenght of foodItem {foodItems.Length}");
                    index = FindEmptySlot(foodItems);
                    Console.WriteLine(index);
                    foodItems[index] = new FoodItem(userName, userQty, userCalories);
                    Console.WriteLine($"{foodItems[index].Name}, {foodItems[index].Qty}, {foodItems[index].Calories}");
                }
            }

        }
        public static int FindName(FoodItem[] foodItems,string userName)
        {
            Console.WriteLine("Finding Name...");
            int index = Array.FindIndex(foodItems, item => item?.Name.ToLower() == userName.ToLower());
            Console.WriteLine(index);
            return index;
        }
        public static int FindEmptySlot(FoodItem[] foodItems)
        {
            int index = -1;
            for (int i = 0; i < foodItems.Length; i++)
            {
                if (foodItems[i] == null)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public static FoodItem[] DoubleArray(FoodItem[] foodItems)
        {
            int arraySize = foodItems.Length;
            Console.WriteLine($"the array size is {arraySize}");
            FoodItem[] tempArray = new FoodItem[arraySize * 2];
            for (int i = 0; i < arraySize; i++)
            {
                tempArray[i] = foodItems[i];
            }
            foodItems = tempArray;
            Console.WriteLine($"the new array size is {foodItems.Length}");
            return foodItems;
        }

    }//end of MethodList(class)
    public class FoodItem
    {
        public string Name { get; set; }
        public double Qty { get; set; }
        public double Calories { get; set; }

        public FoodItem(string name, double qty, double calories)
        {
            Name = name;
            Qty = qty;
            Calories = calories;
        }
        public FoodItem()
        {
            Name = "No Name";
            Qty = -1;
            Calories = -1;
        }
    }//end of FoodItem(class)

}
