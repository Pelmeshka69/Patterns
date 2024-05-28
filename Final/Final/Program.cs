/*Завдання:
Реалізувати систему для вибору алгоритмів сортування.
Використати Strategy для вибору алгоритмів сортування.
Використати Template Method для визначення загальної структури алгоритму сортування.
Використати Command для запуску різних алгоритмів сортування через інтерфейс користувача.

Інструкції:
Реалізувати інтерфейс ISortStrategy та класи конкретних алгоритмів сортування (швидке сортування, сортування бульбашкою).
Створити базовий клас для алгоритмів сортування з використанням Template Method.
Реалізувати інтерфейс ICommand та класи команд для запуску конкретних алгоритмів сортування.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Final
{
    public interface ISortStrategy
    {
        void Sort(int[] array);
    }

    public class QuickSort : ISortStrategy
    {
        public void Sort(int[] array)
        {
            QuickSortArray(array, 0, array.Length - 1);
        }

        private void QuickSortArray(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);
                QuickSortArray(array, low, pi - 1);
                QuickSortArray(array, pi + 1, high);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            return i + 1;
        }
    }

    public class BubbleSort : ISortStrategy
    {
        public void Sort(int[] array)
        {
            for (int pass = 1; pass <= array.Length - 1; pass++)
            {
                for (int i = 0; i <= array.Length - 2; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
            }
        }
    }

    public abstract class SortAlgorithm
    {
        protected ISortStrategy sortStrategy;

        public void SetSortStrategy(ISortStrategy sortStrategy)
        {
            this.sortStrategy = sortStrategy;
        }

        public void Sort(int[] array)
        {
            sortStrategy.Sort(array);
        }
    }

    public class SortAlgorithmImplementation : SortAlgorithm
    {
        public SortAlgorithmImplementation()
        {
            this.sortStrategy = new QuickSort();
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class SortCommand : ICommand
    {
        private SortAlgorithm sortAlgorithm;
        private int[] array;

        public SortCommand(SortAlgorithm sortAlgorithm, int[] array)
        {
            this.sortAlgorithm = sortAlgorithm;
            this.array = array;
        }

        public void Execute()
        {
            sortAlgorithm.Sort(array);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть числа для сортування, розділені пробілами:");
            int[] array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            SortAlgorithm sortAlgorithm = new SortAlgorithmImplementation();

            Console.WriteLine("Виберіть стратегію сортування: 1 - швидке сортування, 2 - сортування бульбашкою");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    sortAlgorithm.SetSortStrategy(new QuickSort());
                    break;
                case "2":
                    sortAlgorithm.SetSortStrategy(new BubbleSort());
                    break;
                default:
                    Console.WriteLine("Невідома опція");
                    return;
            }

            ICommand sortCommand = new SortCommand(sortAlgorithm, array);

            sortCommand.Execute();

            Console.WriteLine("Відсортований масив:");
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
        }
    }
}
