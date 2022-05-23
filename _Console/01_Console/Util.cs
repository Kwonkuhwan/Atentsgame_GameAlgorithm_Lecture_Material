using System;
using System.Collections.Generic;
using System.Text;

namespace _01_Console
{
    class Util
    {
        /// <summary>
        /// 양자택일한 결과를 리턴하는 함수
        /// </summary>
        /// <param name="question">양자택일용 질문</param>
        /// <returns>yes면 true, 아니면 false</returns>
        static public bool ChoiceYesNo(string question) // static은 같은 종류의 클래스가 완전히 공유하는 것
        {

            Console.Write($"{question} (yes/no) : ");   // 질문 출력하고
            string answer = Console.ReadLine();
            if( answer == "yes" || answer == "Yes" || answer == "y" || answer == "Y" )  // yes 4종류일 때만 true 리턴
            {
                return true;
            }
            return false;
        }

        public const int WRONG_OPTION = 0; // 상수(항상 같은 수)
        static public int Choice123(string op1, string op2, string op3, string question = "번호를 선택해 주세요")
        {
            int result = WRONG_OPTION;

            Console.WriteLine($"1. {op1}     2. {op2}     3. {op3}");
            Console.Write($"{question} : ");

            string number = Console.ReadLine();
            int.TryParse(number, out result);

            return result;
        }

        static public bool Battle(Character character1, Character character2)
        {
            bool victory = false;
            while (true)
            {
                character1.Attack(character2);
                if (character2.HealthPoint <= 0)
                {
                    Console.WriteLine("\n\n승리!\n\n");
                    victory = true;
                    break;
                }
                character2.Attack(character1);
                if (character1.HealthPoint <= 0)
                {
                    Console.WriteLine("\n\n패배.....\n\n");
                    victory = false;
                    break;
                }
                Console.WriteLine("\n\n");
            }
            return victory;
        }
        //static public int ChiceSelection(params string[] ops)
        //{
        //    ops.Length;
        //    ops[0]
        //}

        static public void MoveTown(Player player, int _op)
        {
            Random random = new Random();
            if (_op == 1)
            {
                Console.WriteLine("고블린 마을 입성!!!!");
                int count = random.Next(1, 4);
                for (int i = 0; i < count; i++)
                {
                    Goblin enemy = new Goblin("새 고블린" + (i + 1).ToString());
                    Console.WriteLine($"{enemy.name} {i + 1}(이)가 나타났다.");
                    enemy.PrintStatus();

                    bool victory = Battle(player, enemy);

                    Console.WriteLine($"현재 {player.name} Status");
                    player.PrintStatus();

                    if (!victory)
                        break;
                }
            }
            else if (_op == 2)
            {
                Console.WriteLine("오크 마을 입성!!!!");

                int count = random.Next(1, 4);
                for(int i=0; i<count; i++)
                {
                    Orc enemy = new Orc("새 오크" + (i+1).ToString());
                    Console.WriteLine($"{enemy.name} {i+1}(이)가 나타났다.");
                    enemy.PrintStatus();

                    bool victory = Battle(player, enemy);

                    Console.WriteLine($"현재 {player.name} Status");
                    player.PrintStatus();

                    if (!victory)
                        break;
                }
            }
            else if (_op == 3)
            {
                Console.WriteLine($"집으로 돌아갑니다.");
                player.HealthPoint += 100;
                Console.WriteLine("\n\n");
                Console.WriteLine($"{player.name}의 HP가 100 채워졌다.");
                player.PrintStatus();
                Console.WriteLine("\n\n");
            }
        }
    }
}
