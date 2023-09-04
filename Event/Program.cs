using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[5];
            NumberReader reader = new NumberReader();
            Names nameHandler = new Names();
            nameHandler.NamesEnteredEvent += nameHandler.GetSortedNames;
            try
            {
                Console.WriteLine($"Введите {names.Length} имени, начиная с фамилии");
                for (int i = 0; i < names.Length; i++)
                {
                    Console.Write($"Введите фамилию {i + 1}: ");
                    names[i] = Console.ReadLine();
                }
                Console.WriteLine("Введите либо 1, либо 2");
                int number = reader.Read();
                ShowNumber(number);
                nameHandler.GetSortedNames(names, number);
                foreach (string i in names)
                {
                    Console.WriteLine(i);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Введено некорректное значение");
            }
        }
        static int ShowNumber(int number)
        {
            switch (number)
            {
                case 1:
                    Console.WriteLine("Введено значение 1");
                    number = 1;
                    break;
                case 2:
                    Console.WriteLine("Введено значение 2");
                    number = 2;
                    break;
            }
            return number;
        }
        class Names
        {
            public delegate string[] NamesEnteredDelegate(string[] names, int number);
            public event NamesEnteredDelegate NamesEnteredEvent;

            public string[] GetSortedNames(string[] names, int number)
            {
                if (number == 1)
                {
                    Array.Sort(names);
                    return names;
                }
                else if (number == 2)
                {
                    Array.Sort(names, (x, y) => y.CompareTo(x)); // Сортировка Я-А
                    return names;
                }
                return names;
            }
        }
    }
    class NumberReader
    {
        public delegate void NumberEnteredDelegate(int number);
        public event NumberEnteredDelegate NumberEnteredEvent;
        public int Read()
        {
            int number = Convert.ToInt32(Console.ReadLine());
            if (number != 1 && number != 2)
            {
                throw new FormatException();
            }
            NumberEnteredEvent?.Invoke(number);
            return number;
        }
    }
}
