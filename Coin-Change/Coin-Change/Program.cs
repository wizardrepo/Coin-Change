using System;
using System.Collections.Generic;
/*
  User input through ReadLine() in the CLI. 

 */
class Operations
{
    /* Coin change problem modified to also save all combinations to a list,
      */
    public static int RecordCombinations(int[] arr, int length, int target, List<List<int>> solutions, string coins)

    {
        /*target reduced to zero, so combination found
        Also returns 0 if target input at 0*/
        if (target == 0)
        {
            List<int> S = new List<int>();
            /* swaps string to int and removes seperator char*/
            S = FilterChar(coins, '-');
            solutions.Add(S);
            return 1;
        }

        if (target < 0)
        {
            return 0;
        }
        /* There are no possible combinations */
        if (length <= 0 && target >= 1)
        {
            return 0;
        }
        /* Steps backwards through array and on each recursive split either 1) decrements index by 1 or 2)
         * subtracts index from target and stores it inside coin variable. Runs in exponential time. 
              
      */
        return RecordCombinations(arr, length - 1, target, solutions, coins)
        + RecordCombinations(arr, length, target - arr[length - 1], solutions, coins += "-" + arr[length - 1].ToString());
    }


    /* Function to filter a seperation char, c, out of string and 
      append the chars in between to an integer list. 
       */
    public static List<int> FilterChar(string s, char c)
    {
        List<int> s2 = new List<int>();
        string s_temp = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == c)
            {
                Int32.TryParse(s_temp, out int result);
                if (result > 0)
                {
                    s2.Add(result);
                    s_temp = "";
                }
            }

            else
            {
                s_temp += s[i];
            }
        }

        /* catch fall through case */
        if (s_temp != "")
        {
            Int32.TryParse(s_temp, out int result);
            s2.Add(result);
        }

        return s2;
    }

    public static List<int> FindSmallestList(List<List<int>> s)
    {
        /* Base case to ensure not passed list <= len(1) */
        if (s.Count > 1)
        {
            List<int> min = s[0];
            foreach (List<int> k in s)
            {
                if (k.Count < min.Count)
                {
                    min = k;
                }
            }

            return min;
        }
        return s[0];
    }
}


class Program
{
    public static int Main()
    {
        string input = "";
        int[] orders = { 0, 0, 0 };
        string[] ids = { "SH3", "YT2", "TR" };

        string id = "";
        int order_size;
        List<List<int>> solutions = new List<List<int>>();
        int[] arr = { 1, 2, 3 };
        string coins = "";

        int[] SH3_keys = { 3, 5 };
        double[] SH3_vals = { 2.99, 4.49 };
        int[] YT2_keys = { 4, 10, 15 };
        double[] YT2_vals = { 4.95, 9.95, 13.95 };
        int[] TR_keys = { 3, 5, 9 };
        double[] TR_vals = { 2.95, 4.45, 7.99 };
        /* dictionary struct: Product ID[Product Quantity][Quantity Price] 
        */
        Dictionary<string, Dictionary<int, double>> id_quantity = new Dictionary<string, Dictionary<int, double>>();

        Dictionary<int, double> SH3 = new Dictionary<int, double>();
        Dictionary<int, double> YT2 = new Dictionary<int, double>();
        Dictionary<int, double> TR = new Dictionary<int, double>();
        for (int i = 0; i < SH3_keys.Length; i++)
        {
            SH3.Add(SH3_keys[i], SH3_vals[i]);
        }

        for (int i = 0; i < YT2_keys.Length; i++)
        {
            YT2.Add(YT2_keys[i], YT2_vals[i]);
            TR.Add(TR_keys[i], TR_vals[i]);
        }

        id_quantity.Add("SH3", SH3);
        id_quantity.Add("YT2", YT2);
        id_quantity.Add("TR", TR);

        while (true)
        {
            Console.WriteLine("Input amount or -q to quit:\n");
            input = Console.ReadLine();
            if (input == "-q")
            {
                return -1;
            }
            if (!Int32.TryParse(input, out int val))
            {
                Console.WriteLine("recieved a non-int value, try again\n");
            }
            else
            {
                order_size = val;
                break;
            }
        }

        while (true)
        {
            Console.WriteLine("input product Code (SH3, YT2, TR or -q to quit): \n");
            input = Console.ReadLine();

            if (input == "-q")
            {
                return -1;
            }

            if (id_quantity.ContainsKey(input))
            {
                id = input;
                Console.WriteLine("got\n");
                break;
            }

        }

        List<int> nums_list = new List<int>(id_quantity[id].Keys);
        int[] nums = nums_list.ToArray();

        if (order_size < 30 && Operations.RecordCombinations(nums, nums.Length, order_size, solutions, coins) > 0)
        {
            List<int> smallest = Operations.FindSmallestList(solutions);

            double sum = 0;

            foreach (int item in smallest)
            {
                sum += id_quantity[id][item];
                Console.WriteLine("Quantity: {0} \t price: {1}\n", item, id_quantity[id][item]);

            }

            Console.WriteLine("final price: {0} with {1} packages", sum, smallest.Count);
        }
        else
        {
            Console.WriteLine("No matches found for order_size or order size above 30: {0}", order_size);
            return -1;
        }

        return 0;
    }
}

