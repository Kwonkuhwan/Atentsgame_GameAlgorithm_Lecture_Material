using System;

namespace _02_Console_Training
{
    class _22_05_17
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            _22_05_17 program = new _22_05_17();
            Action action = new Action();

            Console.Write($"게임에 사용할 닉네임을 정해주세요 : ");
            string userName = Console.ReadLine();

            Character_Info human = new Character_Info();
            human.Init(userName, 100);

            Character_Info ork = new Character_Info();
            ork.Init("Ork", 100);

            while (true)
            {
                Console.WriteLine($"사용할 메뉴 선택 : (1) 현재 스테이터스, (2) 레벨업, (3) 전투 시작, (99) 종료");
                Console.Write($"선택 창 : ");
                int inputControl = int.Parse(Console.ReadLine());
                Console.WriteLine("");

                if (inputControl == 1)
                {
                    Console.WriteLine($"현재 스테이터스 선택");
                    action.Current_Status(human);
                }
                else if (inputControl == 2)
                {
                    Console.WriteLine($"레벨업 선택");
                    action.LevelUp(human);
                }
                else if(inputControl == 3)
                {
                    Console.WriteLine($"전투 선택");
                    action.Attack(human, ork);
                }
                else if (inputControl == 99)
                {
                    Console.WriteLine($"종료 선택");
                    break;
                }
            }
        }
    }   
}