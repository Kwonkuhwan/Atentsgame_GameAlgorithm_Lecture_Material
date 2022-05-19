using System;

namespace _02_Console_Training
{
    class _22_05_17
    {
        Random random = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args)
        {
            _22_05_17 program = new _22_05_17();
            ClsAction action = new ClsAction();

            Console.Write($"게임에 사용할 닉네임을 정해주세요 : ");
            string userName = Console.ReadLine();

            ClsHuman human = new ClsHuman(userName);
            ClsOrk ork = new ClsOrk("Ork", program.random.Next(100, 201));

            while (true)
            {
                Console.WriteLine($"사용할 메뉴 선택 : (1) 현재 스테이터스, (2) 레벨업, (3) 전투 시작, (99) 종료");
                Console.Write($"선택 창 : ");
                int inputControl = int.Parse(Console.ReadLine());
                Console.WriteLine("");

                if (inputControl == 1)
                {
                    while (true)
                    {
                        Console.WriteLine($"현재 스테이터스 선택");
                        Console.WriteLine($"사용할 메뉴 선택 : (1) 자신, (2) 오크, (99) 뒤로가기");
                        int select = int.Parse(Console.ReadLine());
                        if (select == 1)
                            human.Current_Status();
                        else if (select == 2)
                            ork.Current_Status();
                        else if (select == 99)
                            break;
                    }
                }
                else if (inputControl == 2)
                {
                    while (true)
                    {
                        Console.WriteLine($"레벨업 선택");
                        Console.WriteLine($"사용할 메뉴 선택 : (1) 자신, (2) 오크, (99) 뒤로가기");
                        int select = int.Parse(Console.ReadLine());
                        if (select == 1)
                            human.LevelUp();
                        else if (select == 2)
                            ork.Current_Status();
                        else if (select == 99)
                            break;
                    }
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