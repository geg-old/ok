using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;

namespace KMS_Task_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Создайте пароль: ");
            string password = Console.ReadLine();
            Console.WriteLine("Пароль успешно создан.");

            string enterPass;
            string secretMessage;
            secretMessage = "Я секретное сообщение. Видимо код сработал =)";
            int count = 0;


            do
            {
                Console.Write("\nВведите пароль: ");
                enterPass = Console.ReadLine();

                if (enterPass != password)
                {
                    if (count < 2)
                    {
                        Console.Write("\nНеверный пароль.");
                        count = count + 1;
                    }
                    else
                    {
                        Console.WriteLine("\nПопытки исчерпаны");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("\n" + secretMessage);
                    break;
                }
            }
            while (enterPass != password);

            Console.ReadKey();
        }
    }
}

