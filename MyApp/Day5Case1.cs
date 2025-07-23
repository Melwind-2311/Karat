using System;
class Program
    {
        static void Main(string[] args)
        {
            
            ArrayPrinter ap = new ArrayPrinter();
            int[] array1 = {23,25,27,29};
            ap.PrintElementAtIndex(array1, 2);
            ap.PrintElementAtIndex(array1, 4);
            ap.PrintElementAtIndex(array1, -1);
        }
    }

    class ArrayPrinter
    {
        public void PrintElementAtIndex(int[] arr, int index)
        {
            if (index >= 0 && index < arr.Length)
            {
                Console.WriteLine("Element: " + arr[index]);
            }
            else
            {
                Console.WriteLine("Invalid index");
            }
        }

    }
