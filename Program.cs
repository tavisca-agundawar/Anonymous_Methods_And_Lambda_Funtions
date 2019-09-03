using System;
using System.Collections.Generic;

namespace Anonymous_Fun
{
    class Program
    {
        //Explicit delegate declaration
        public delegate void Greet(string name);
        static void Main(string[] args)
        {
            //Assigning anonymous method to delegate
            Greet greet = delegate (string name)
            {
                Console.WriteLine($"Called using explicit delegate: Hello {name}!\n");
            };

            //Calling using delegate
            greet("John Doe");

            //Create list of Items
            List<Item> items = new List<Item>()
            {
                new Item{ID = 1, Name = "Bottle"},
                new Item{ID = 2, Name = "Charger"},
                new Item{ID = 3, Name = "Board"},
            };
            
            //Assign FindItem funtion to Predicate type delegate
            //This is required as List<>.Find() expects a Predicate<> Type method
            Predicate<Item> predicate = new Predicate<Item>(FindItem);

            var foundItem = items.Find(FindItem); //Expects Find(Predicate<Item> match)
                                                  //It will iterate through every item in items and call the Predicate<> to find a match.

            DisplayItem(foundItem, "Predicate");

            //Using anonymous function
            foundItem = items.Find(delegate(Item obj)
            {
                return obj.ID == 2;
            });

            DisplayItem(foundItem, "Anonymous");

            //Using Lambda function
            foundItem = items.Find(item => item.ID == 3);

            DisplayItem(foundItem, "Lambda");

            //--------------------- Action and Func ------------------------//

            Action<string> sayHello = delegate (string Name)
            {
                Console.WriteLine("Called using Action: Hello {0}\n", Name);
            };
            sayHello("Jane Doe");

            Func<int, int, int> addFunc = (num1, num2) => num1 + num2; //Func using lambda
            Console.WriteLine("Calling Func 10 + 20 = {0}\n", addFunc(10, 20));


            Console.ReadKey(true);
        }

        //Explicitly defining a function that returns bool
        public static bool FindItem(Item obj)
        {
            return obj.ID == 1;
        }

        private static void DisplayItem(Item foundItem, string Method)
        {
            Console.WriteLine($"Call from {Method} method:\nItem Id: {foundItem.ID}  Item Name: {foundItem.Name}\n");
        }

        
    }

    class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    /*
    Above code prints the following output:
    Called using explicit delegate: Hello John Doe!

    Call from Predicate method:
    Item Id: 1  Item Name: Bottle

    Call from Anonymous method:
    Item Id: 2  Item Name: Charger

    Call from Lambda method:
    Item Id: 3  Item Name: Board

    Called using Action: Hello Jane Doe

    Calling Func 10 + 20 = 30
    */
}
