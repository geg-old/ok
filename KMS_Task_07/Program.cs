using System;

namespace KMS_Task_07
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 0;

            inputErr:
            Console.Write("Введите размер массива: ");
            try
            {
                size = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Введите число цифрами. Например, 7.");
                goto inputErr;
            }

            if (size <= 0)
            {
                Console.WriteLine("Размер массива не может быть отрицательным или равен нулю.");
                goto inputErr;
            }

            int[] arr = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++) arr[i] = rnd.Next(0, 100);

            Console.WriteLine("\nИсходный массив:");
            for (int i = 0; i < size; i++) Console.Write(arr[i] + " ");
            Console.WriteLine("\n\nИтоговый массив:");
            Shuffle(arr, size);
            for (int i = 0; i < size; i++) Console.Write(arr[i] + " ");
            Console.ReadKey();
        }
        static void Shuffle(int[] arr, int size)
        {
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                int rndSelect = rnd.Next(0, i + 1);
                int j = arr[rndSelect];
                arr[rndSelect] = arr[i];
                arr[i] = j;
            }
        }
    }
}