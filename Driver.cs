using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// RIKER QUINTANA
/// 816823248
/// 
/// File path goes to bin folder/debugging
/// Only need to insert file name OR file path if outside of bin folder
/// 
/// Example Valid Input:
/// -999
/// 999
/// *
/// Example Output:
/// -999
/// *999
/// -998001
/// </summary>
namespace InfInt
{
    class Driver
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("infint.txt");
            int i = 0;
            for (i = 0; i < lines.Length; i+=3)
            {
               var list1 = new InfInt(lines[i]);
                var list2 = new InfInt(lines[i + 1]);
                var characters = lines[i + 2].ToCharArray();
                char listOperand = characters[0];
                Console.Write($"{list1}{listOperand}{list2}=");
                //decides which method to call based off character operator inputted on every 3rd line
                if (listOperand == '+')
                {
                    list1.Plus(list2);
                }
                else if (listOperand == '-')
                    list1.Minus(list2);
                else if (listOperand == '*')
                    list1.Times(list2);
                else
                    Console.WriteLine("Doing Nothing.");      
            //Output the total to console
                Console.WriteLine($"{list1}\n");
            }
            Console.ReadLine();





        }
    }
}
