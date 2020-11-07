using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Task_04
{
    class Program
    {
        static void Main(string[] args)
        {
            int bossHealth = 666;
            int bossDodge = 0;
            int playerHealth = 300;
            int playerAttack = 0;
            Random rnd = new Random();
            Random bossRnd = new Random();
            Random health = new Random();
            int chooseWhoAttack = rnd.Next(1, 2);
            int bossAttack = rnd.Next(1, 4);
            int borningAttack = 30;
            int hp = 0;
            int tryToHealthCount = 0;
            int actionCount = 0;

            Console.Write("Вы - обычный работяга, который работает на обычной работе.\nНо обычным утром, когда вы шли выпить обычного кофе перед работой \nна вас напал необычный неформал демон. ");
            Console.WriteLine("Сражения не избежать.");
            Console.WriteLine("\nСписок ваших возможностей:");
            Console.WriteLine("1. Обычная атака - вы просто бьете демона обычным ударом.\nНачальный урон 30.");
            Console.WriteLine("2. Эффектная атака - красиво.\nУрон 20.");
            Console.WriteLine("3. Бросок камня.\nУрон 10.");
            Console.WriteLine("4. Поход в больницу - восстанавливает немного жизней.\nВозвращает от 10 до 60 hp.");
            Console.WriteLine("5. КиК ФлиК - ульта, заряжается 5 ходов.\nУрон 200.");
            Console.WriteLine("6. Секция по боксу.\nУсиливает обычную атаку в 2 раза.");

            while (true)
            {
                Console.WriteLine($"\n     Ваше здоровье: {playerHealth} \n     Здоровье противника: {bossHealth}");
                if (chooseWhoAttack == 1)
                {
                    Console.WriteLine("\n\n\nВаш очередь нападать!");
                    Console.WriteLine("Введите 0, если не помните свои атаки");
                inputAttackErr:
                    try
                    {
                        playerAttack = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Атакуй цифрами, все боятся математику.");
                        goto inputAttackErr;
                    }

                    bossDodge += 1;

                    switch (playerAttack)
                    {
                        case (0):
                            Console.WriteLine("\nСписок ваших атак: \n1. Обычная атака \n2. Эффектная атака \n3. Бросок камня \n4.Восстановление hp \n5.КиК ФлиК \n6.Бокс");
                            goto inputAttackErr;
                        case (1):
                            Console.Write("Вы совершили обычную атаку как обычный персонаж");
                            if (bossDodge != 6)
                            {
                                Console.WriteLine($" и нанесли обычный урон. \nПротивник теряет {borningAttack}  hp");
                                bossHealth -= borningAttack;
                            }
                            else
                            {
                                Console.Write(", но противник увернулся от нее.");
                                bossDodge = 0;
                            }
                            chooseWhoAttack = 2;
                            break;
                        case (2):
                            Console.WriteLine("Атака эффектная, но не эффиктивная. Красота требует жертв.");
                            if (bossDodge != 6)
                            {
                                Console.WriteLine("Противник теряет 20 hp.\nА вы возможность сыграть следующий ход.");
                                bossHealth -= 20;
                            }
                            else
                            {
                                Console.WriteLine("И скорости. Противник увернулся.\nА вы потеряли следующий ход.");
                            }
                            chooseWhoAttack = 2;
                            break;
                        case (3):
                            Console.Write("Ну... Вы бросили в черта камень...");
                            if (bossDodge != 6)
                            {
                                Console.WriteLine("Серьезно?");
                                Console.WriteLine("Противник теряет 10 hp");
                                bossHealth -= 10;
                            }
                            else
                            {
                                Console.WriteLine("Еще и не попали...");
                                bossDodge = 0;
                            }
                            chooseWhoAttack = 2;
                            break;
                        case (4):
                            if (tryToHealthCount < 2)
                            {
                                Console.WriteLine("Вы легли в больницу, чтобы подлечиться.\nПротивник не может вас найти.\nА значит не может атаковать еще ход.");
                                hp = health.Next(10, 60);
                                Console.WriteLine($"Вы получили {hp} hp");
                                playerHealth += hp;
                                tryToHealthCount += 1;
                            }
                            else
                            {
                                Console.WriteLine("Вы начали казаться подозрительным для бабушек в поликлинике и они вас выгнали.");
                                chooseWhoAttack = 2;
                                tryToHealthCount = 0;
                            }
                            break;
                        case (5):
                            if (actionCount < 5)
                            {
                                Console.WriteLine("Кик ФлиК у вас нестабильный, нужно больше ходов на подготовку.");
                                chooseWhoAttack = 2;
                            }
                            else
                            {
                                if (bossDodge == 6)
                                {
                                    Console.Write("Черт попытался увернуться. \nНо ");
                                }
                                Console.WriteLine("Вы сделали Кик ФлиК и поразили всех.");
                                Console.WriteLine("Противник теряет 200 hp и возможость совершить ход");
                                bossHealth -= 200;
                                actionCount = -1;
                            }
                            break;
                        case (6):
                            Console.WriteLine("Без особого желания вас приняли в секцию по боксу.");
                            Console.WriteLine("Вы усилили обычную атаку в два раза");
                            Console.WriteLine("И потеряли половину зарплаты.");
                            borningAttack *= 2;
                            chooseWhoAttack = 2;
                            break;
                        default:
                            Console.WriteLine("Ну попытка была...");
                            break;
                    }
                    actionCount += 1;
                    if (playerHealth > 300)
                    {
                        playerHealth = 300;
                    }
                }
                else
                {
                    Console.WriteLine("\n\n\nАтака противника!");
                    if (bossAttack == 0)
                    {
                        bossAttack = 6;
                    }
                    else if (playerAttack != 3)
                    {
                        bossAttack = bossRnd.Next(0, 4);
                    }
                    else if (bossAttack == 6)
                    {
                        bossAttack = bossRnd.Next(1, 4);
                    }
                    else
                    {
                        bossAttack = 5;
                    }

                    switch (bossAttack)
                    {
                        case (0):
                            Console.WriteLine("Черт хотел восстановить жизни. \nНо его не приняли в поликлинике без полиса. \nОн пропускает ход.");
                            break;
                        case (1):
                            Console.WriteLine("Черт ударил вас. \nЭто наверняка больно, он ведь бьет с нечеловеческой силой.");
                            Console.WriteLine("Вы теряете 40 hp");
                            playerHealth -= 40;
                            break;
                        case (2):
                            Console.WriteLine("В противнике проснулась ярость и он провел на вас серию ударов.");
                            Console.WriteLine("Вы теряете 100 hp");
                            playerHealth -= 100;
                            break;
                        case (3):
                            Console.WriteLine("Противник дал вам подзатыльник, считая ваши атаки детскими проказами.");
                            Console.WriteLine("Вы теряете 30 hp");
                            playerHealth -= 30;
                            break;
                        case (4):
                            Console.WriteLine("Черт не стал вас бить и включил треки корейских поп групп.");
                            Console.WriteLine("От стыда вы ударили себе по лбу и потеряли 10 hp");
                            playerHealth -= 10;
                            break;
                        case (5):
                            Console.WriteLine("В вас в ответ на камень кинули булыжником. \nНе стоит бросаться предметами в следующий раз.");
                            Console.WriteLine("Вы теряете 150 hp и уверенность в себе");
                            playerHealth -= 150;
                            break;
                        case (6):
                            Console.WriteLine("В этот раз он захватил полис.");
                            Console.WriteLine("Противник получил 50 hp");
                            bossHealth += 50;
                            break;
                    }

                    if (bossHealth > 666)
                    {
                        bossHealth = 666;
                    }

                    chooseWhoAttack = 1;

                }
                if (bossHealth <= 0) break;
                if (playerHealth <= 0) break;
            }

            if (playerHealth <= 0)
            {
                if (bossAttack == 4)
                {
                    Console.WriteLine("\n\nА также победу. Вы выбили себя из игры.");
                }
                else
                {
                    Console.WriteLine("\n\nКак и ожидалось - вы проиграли. Черт ликует!");
                }
            }
            else
            {
                if (playerAttack == 1)
                {
                    Console.WriteLine("\n\nВы одержали обычную победу в обычной игре.");
                }
                else
                {
                    Console.WriteLine("\n\nКаким- то чудом вы одержали победу. Черт повержен!");
                }
            }
            Console.ReadKey();
        }
    }
}
