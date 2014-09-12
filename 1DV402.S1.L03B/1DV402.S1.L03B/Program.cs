using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S1.L03B
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                //Titel
                Console.Title = "Godtycklig lönerevision - nivå B";

                //Variabler
                int numberOfSalaries = 0;
                int[] salaries;

                //Anropar metoder
                numberOfSalaries = ReadInt("Ange antal löner att mata in: ");
                salaries = new int[numberOfSalaries];
                salaries = ReadSalaries(numberOfSalaries);
                ViewResult(salaries);

                //Ger möjlighet för användare att repetera beräkning.
            } while (IsContinuing());
            return;
        }
        static private int GetDispersion(int[] source)
        {
            int dispersion = (source.Max() - source.Min());
            return dispersion;
        }
        static private int GetMedian(int[] source)
        {
            int length = source.GetLength(0);
            int[] cloneSource = new int[length];

            /*Kopiera Array i en ny Array, för att sedan sortera olika värden.
             * Jag behöver göra det, eftersom jag vill skriva ut array i metoden ViewResult
             * i ordningen hur användaren matade in lönerna.
             * Ett annat sätt var att skicka parametern till metoden GetMedian "by reference", för då 
             * kan jag manipulera med array i GetMedian metod utan att förändra
             * ordningen i den orginella array*/

            Array.Copy(source, cloneSource, length);
            Array.Sort(cloneSource);

            int rest = length % 2;

            if (rest != 0)
            {
                return (cloneSource[length / 2]);
            }
            else
            {
                int medium = ((cloneSource[(length / 2) - 1] + cloneSource[(length / 2)]) / 2);
                return medium;
            }
        }
        static private bool IsContinuing()
        {
            ViewMessage("\n► Tryck tangent för ny beräkning - Esc avslutar.", false);
            return (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
        static private int ReadInt(string prompt)
        {
            while (true)
            {
                string message = prompt;
                string errorMessage;
                Console.Write("{0}", message);
                string input = Console.ReadLine();
                try
                {
                    int numberOfSalaries = int.Parse(input);
                    if (numberOfSalaries < 2)
                    {
                        throw new OverflowException();
                    }
                    return numberOfSalaries;
                }
                catch (OverflowException)
                {
                    errorMessage = string.Format("\nDu måste mata in minst två löner för att kunna göra en beräkning!");
                    ViewMessage(errorMessage, true);
                    IsContinuing();
                }
                catch (FormatException)
                {
                    errorMessage = string.Format("\nFEL! \'{0}\' kan inte tolkas som ett heltal!", input);
                    ViewMessage(errorMessage, true);
                }
            }
        }
        static private int[] ReadSalaries(int count)
        {
            Console.WriteLine();
            string message = "Ange lön nummer ";
            int[] salaries = new int[count];
            for (int i = 1; i <= count; i++)
            {
                int arrayMember = ReadInt(message + i + " :");
                salaries[i - 1] = arrayMember;
            }
            return salaries;
        }
        static private void ViewMessage(string message, bool isError)
        {
            if (isError == true)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }
        static private void ViewResult(int[] salaries)
        {
            int[] income = salaries;
            int length = income.GetLength(0);

            Console.WriteLine("\n-------------------------------");
            int reminder = length % 2;
            Console.WriteLine("{0,-15}{1,15:c0}", "Medianlön:", GetMedian(income));
            Console.WriteLine("{0,-15}{1,15:c0}", "Medellön:", (double)income.Average());
            Console.WriteLine("{0,-15}{1,15:c0}", "Lönespridning: ", (GetDispersion(income)));
            Console.WriteLine("-------------------------------");
            Console.WriteLine();

            for (int i = 1; i <= length; i++)
            {
                if (i % 3 != 0)
                {
                    Console.Write("{0,8}", income[i - 1]);
                }
                else
                {
                    Console.Write("{0,8}", income[i - 1]);
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
