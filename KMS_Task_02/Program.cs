using System;

namespace KMS_Task_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;

            Console.WriteLine("Программа вводит текст до тех пор, пока пользователь не введет 'exit'. \n");
            Console.WriteLine("Введите текст: ");

            do
            {
                text = Console.ReadLine();
            }
            while (text != "exit");

            Console.WriteLine("\nПрограмма отработала. Нажмите любую кнопку, чтобы продолжить");
            Console.ReadKey();
        }
    }
}
