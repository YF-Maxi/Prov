using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prov
{
    internal class UI
    {
        List list = new List();

        public void Run()
        {
            while (true)
            {
                MainMenu();
            }
        }

        //The main menu where you select functions by pressing the number keys
        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("(Use number keys 1-8)");
            Console.Write("1. Add value at the end of the list\n2. Add value at index\n3. Remove value at index\n4. Get list length\n5. Sort list\n6. Print list\n7. Clear list\n8. Exit program");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    ChooseValueType(true);
                    break;

                case ConsoleKey.D2:
                    ChooseValueType(false);
                    break;

                case ConsoleKey.D3:
                    RemoveValue();
                    break;

                case ConsoleKey.D4:
                    GetLength();
                    break;

                case ConsoleKey.D5:
                    SortList();
                    break;

                case ConsoleKey.D6:
                    PrintList();
                    break;
                
                case ConsoleKey.D7:
                    ClearList();
                    break;

                case ConsoleKey.D8:
                    Console.Clear();
                    Environment.Exit(0);
                    break;

                default:
                    MainMenu();
                    break;
            }
        }

        //Select which type of variable to add
        private void ChooseValueType(bool normalAdd) //If normalAdd is true, the value will get added with the regular add function. If false, it will use the add by index function
        {
            Console.Clear();
            Console.WriteLine("(Use number keys 1-6)");
            Console.Write("Select which type of variable to add:\n1. string\n2. integer\n3. float\n4. double\n5. long\n6. boolean");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    AddValueToList<string>(normalAdd); //Sending the bool to the next function
                    break;

                case ConsoleKey.D2:
                    AddValueToList<int>(normalAdd);
                    break;

                case ConsoleKey.D3:
                    AddValueToList<float>(normalAdd);
                    break;

                case ConsoleKey.D4:
                    AddValueToList<double>(normalAdd);
                    break;

                case ConsoleKey.D5:
                    AddValueToList<long>(normalAdd);
                    break;

                case ConsoleKey.D6:
                    AddValueToList<bool>(normalAdd);
                    break;

                default:
                    ChooseValueType(normalAdd);
                    break;
            }
        }

        //Reads the user's input and converts it from a string to the correct variable type. 
        private void AddValueToList<T>(bool normalAdd) 
        {
            bool canAdd = false;
            T valueToAdd = (T)Convert.ChangeType(0, typeof(T)); //Doing this so it doesn't complain about valueToAdd ponetially being null
            Console.Clear();
            Console.Write("Enter value to add: ");

            try //Simple try-catch that prevents the user from crashing the program if they input the wrong thing
            {
                valueToAdd = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                canAdd = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong format, try again.\n\n[Press any key to continue]");
                Console.ReadKey();
                AddValueToList<T>(normalAdd);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Failed to convert, try again.\n\n[Press any key to continue]");
                Console.ReadKey();
                AddValueToList<T>(normalAdd);
            }

            if (canAdd) 
            {
                if (normalAdd) //Using the bool to know if it should add with the normal function or the add function with an index
                {
                    list.AddValue(valueToAdd);
                }
                else if (!normalAdd)
                {
                    list.AddValueAtIndex(valueToAdd, GetIndex()); //Gets the index number from another function
                }
                Console.WriteLine("\nAdded " + valueToAdd.ToString() + " to the list\n\n[Press any key to continue]");
                Console.ReadKey();
            }
        }
        private int GetIndex()
        {
            Console.Clear();
            Console.WriteLine("(Index 0 is the start of the list)");
            Console.Write("Enter index to add value at: ");
            int index = 0;

            //Same try catch as before, but for intergers
            try
            {
                index = (int)Convert.ChangeType(Console.ReadLine(), typeof(int));
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong format, try again.\n\n[Press any key to continue]");
                Console.ReadKey();
                GetIndex();
            }  
            catch (InvalidCastException)
            {
                Console.WriteLine("Failed to convert, try again.\n\n[Press any key to continue]");
                Console.ReadKey();
                GetIndex();
            }

            //Abort if the index is negative
            if (index < 0)
            {
                Console.WriteLine("Index can't be negative.\n\n[Press any key to continue]");
                Console.ReadKey();
                GetIndex();
            }

            return index;
        }

        private void GetLength()
        {
            Console.Clear();
            if (list.GetLength() == 0)
            {
                Console.WriteLine("The list is empty.\n\n[Press any key to continue]");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("The list has " + list.GetLength() + " values.\n\n[Press any key to continue]");
                Console.ReadKey();
            }
        }

        private void SortList()
        {
            Console.Clear();
            list.Sort();
            Console.WriteLine("List sorted.\n\n[Press any key to continue]");
            Console.ReadKey();
        }

        private void RemoveValue()
        {
            Console.Clear();
            if (list.GetLength() == 0) //Can't remove a value if there is none to remove
            {
                Console.WriteLine("There are no values to remove.\n\n[Press any key to continue]");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Index 0 is the first value.\nThe list has " + list.GetLength() + " values.");
            Console.Write("\nEnter index of the value to remove: ");

            //Using a bool to see if the TryParse worked or not. If it didn't work, the user gets to try again to input something valid
            bool succeeded = int.TryParse(Console.ReadLine(), out int index);

            if (!succeeded)
            {
                Console.WriteLine("Index does not exist in the list, try again.\n\n[Press any key to continue]");
                Console.ReadKey();
                RemoveValue();
            }
            else
            {
                bool removed = list.RemoveAtIndex(index);
                if (removed == false)
                {
                    Console.WriteLine("Index does not exist in the list, try again.\n\n[Press any key to continue]");
                    Console.ReadKey();
                    RemoveValue();
                }
                else
                {
                    Console.WriteLine("\nRemoved value at index " + index + "\n\n[Press any key to continue]");
                    Console.ReadKey();
                    return;
                }
            }
        }

        private void PrintList()
        {
            Console.Clear();
            if (list.GetLength() == 0)
            {
                Console.WriteLine("The list is empty"); //Can't print an empty list ¯\_(ツ)_/¯
            }
            else
            {
                Console.WriteLine("The list:\n");
                list.PrintAll();
            }
            
            Console.WriteLine("\n[Press any key to continue]");
            Console.ReadKey();
        }

        private void ClearList()
        {
            Console.Clear();
            list.Clear();
            Console.WriteLine("List cleared.\n\n[Press any key to continue]");
            Console.ReadKey();
        }
    }
}
