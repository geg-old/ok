using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Task_06
{
    class Program
    {
        private static string[,] _dossier = new string[0, 2]; 
        private static int _size = 0;

        static void Main(string[] args)
        {
            int inputCommand = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Отдел кадров");
                Console.WriteLine("1 - Добавить досье.");
                Console.WriteLine("2 - Вывести все досье.");
                Console.WriteLine("3 - Удалить досье.");
                Console.WriteLine("4 - Поиск по фамилии.");
                Console.WriteLine("5 - Выйти из программы.");
                Console.WriteLine("\nВведите номер команды:");

                inputCommandErr:
                try
                {
                    inputCommand = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Выберите число.");
                    goto inputCommandErr;
                }
                switch (inputCommand)
                {
                    case (1):
                        AddDossier();
                        break;
                    case (2):
                        ShowDossiers();
                        break;
                    case (3):
                        ShowDossiers();
                        RemoveDossier();
                        break;
                    case (4):
                        Console.WriteLine("Введите фамилию для поиска: ");
                        string surname = Console.ReadLine();
                        SearchDossier(surname);
                        break;
                    case (5):
                        Console.WriteLine("Завершение работы программы..");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Введеные данные некорректы.");
                        goto inputCommandErr;
                }
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                }
            }

        private static void AddDossier()
        {
            Console.Write("Введите фамилию: ");
            string fio = Console.ReadLine() + " ";

            Console.Write("Введите имя: ");
            fio += Console.ReadLine() + " ";

            Console.Write("Введите отчество: ");
            fio += Console.ReadLine();

            Console.Write("Введите должность: ");
            string post = Console.ReadLine();

            int newsize = _size + 1;

            string[,] cloneDossier = new string[newsize, 2];
            for (int i = 0; i < Math.Min(_size, newsize); i++)
            {
                cloneDossier[i, 0] = _dossier[i, 0];
                cloneDossier[i, 1] = _dossier[i, 1];
            }
            for (int i = _size; i < newsize; i++)
            { 
                cloneDossier[i, 0] = "";
                cloneDossier[i, 1] = "";
            }
            _dossier = cloneDossier;
            _size = newsize;

            _dossier[_size - 1, 0] = fio;
            _dossier[_size - 1, 1] = post;
            Console.WriteLine("Досье успешно добавлено!");
        }

       



        private static void ShowDossiers()
        {
            if (_size > 0) 
            { 
                Console.WriteLine("Список досье:");
                for (int i = 0; i < _size; i++) Console.WriteLine($"{i + 1}) {_dossier[i, 0]} - {_dossier[i, 1]}");
            }
            else
            {
                Console.WriteLine("Досье остутствуют.");
            }
        }



        private static void RemoveDossier()
        {
            int numberOfDossier;
            if (_size > 0)
            {
                inputNumberErr:
                Console.WriteLine("Введите номер для удаления досье");
                try
                {
                    numberOfDossier = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректный номер досье!");
                    goto inputNumberErr;
                }

                numberOfDossier -= 1;

                if (numberOfDossier >= _size || numberOfDossier < 0)
                {
                    Console.WriteLine("Неправильный индекс. Повторите попытку!");
                    goto inputNumberErr;
                }

                string[,] cloneDossier = new string[_size - 1, 2]; //Временная переменная для обновления списка(массива)
                int tempNum = 0;
                for (int i = 0; i < _size; i++)
                {
                    if (i != numberOfDossier)
                    {
                        cloneDossier[tempNum, 0] = _dossier[i, 0];
                        cloneDossier[tempNum, 1] = _dossier[i, 1];
                        tempNum++;
                    }
                    else
                    {
                        Console.WriteLine($"Досье {_dossier[i, 0]} успешно удалено!");
                    }
                }
                _dossier = cloneDossier;
                _size -= 1;
            }
        }





        private static void SearchDossier(string searchForSurname)
        {
            int count = 0;
            for (int i = 0; i < _size; i++)
            {
                if (_dossier[i, 0].IndexOf(searchForSurname) >= 0)
                {
                    Console.WriteLine($"Досье: {i + 1}) {_dossier[i, 0]} - {_dossier[i, 1]}");
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("Досье с такой фамилий не найдено!");
            }
        }
    }
}
