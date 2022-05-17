using System;

namespace _02_Console_Training
{
    class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            Program program = new Program();
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

    class Character_Info
    {
        Random random = new Random();

        public string str_Tribe;
        public int n_HP;
        public int n_Attack;
        public bool b_Turn;

        public int n_Level = 1;
        public int n_Str = 3;
        public int n_Dex = 5;
        public int n_Int = 2;

        public void Init(string str_tribe, int n_hp)
        {
            str_Tribe = str_tribe;
            n_HP = n_hp;
            n_Attack = 0;
            b_Turn = true;

            n_Level = 1;
            n_Str = random.Next(1, 6);
            n_Dex = random.Next(1, 6);
            n_Int = random.Next(1, 6);
        }
    }

    class Action
    {
        Random random = new Random();
        public void Current_Status(Character_Info character1)
        {
            Console.WriteLine($"┌ 스테이터스 ────────────────────────┐");          
            Console.WriteLine($"│ 이름 : [{character1.str_Tribe}]".PadRight(35, ' ')  + "│");
            Console.WriteLine($"│ 레벨 : [{character1.n_Level}]".PadRight(35, ' ')    + "│");
            Console.WriteLine($"│   힘 : [{character1.n_Str}]".PadRight(36, ' ')      + "│");
            Console.WriteLine($"│ 민첩 : [{character1.n_Dex}]".PadRight(35, ' ')      + "│");
            Console.WriteLine($"│ 지능 : [{character1.n_Int}]".PadRight(35, ' ')      + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n\n");          
        }

        public void LevelUp(Character_Info character1)
        {
                // 입력받은 횟수만큼 레벨업을 하는 코드 만들기
                Console.Write("레벨업 할 수를 입력해주세요 : ");
                int input_LevelUp = int.Parse(Console.ReadLine());

                int LevelUp_Count = 0;

                Console.WriteLine($"[{character1.str_Tribe}] 현재 레벨 : {character1.n_Level}, 현재 스텟 : 힘({character1.n_Str}), 민({character1.n_Dex}), 지({character1.n_Int})");

                while (LevelUp_Count < input_LevelUp)
                {
                    character1.n_Level++;
                    character1.n_Str++;
                    character1.n_Dex++;
                    character1.n_Int++;
                    LevelUp_Count++;
                }

                Console.WriteLine($"[{character1.str_Tribe}] 레벨 업 후 레벨 : {character1.n_Level}, 레벨 업 후 스텟 : 힘({character1.n_Str}), 민({character1.n_Dex}), 지({character1.n_Int})\n\n");

        }
        private void Attack_action(Character_Info character1, Character_Info character2)
        {
            character1.n_Attack = random.Next(1, 20);

            int old_character2_HP = character2.n_HP;
            character2.n_HP -= character1.n_Attack;

            if (character2.n_HP < 0)
                character2.n_HP = 0;

            Console.WriteLine($"{character1.str_Tribe} 공격 : [ {character1.n_Attack} ]");
            Console.WriteLine($"[ {character2.str_Tribe} hp : {old_character2_HP} -> {character2.n_HP} ]\n");
        }

        public void Attack(Character_Info character1, Character_Info character2)
        {
            character1.b_Turn = Convert.ToBoolean(random.Next(0, 2));        // 2보다 작은 수 리턴

            if (character1.b_Turn)
                character2.b_Turn = false;
            else
                character2.b_Turn = true;

            while (true)
            {
                if (character1.n_HP <= 0)
                {
                    Console.WriteLine($"{character1.str_Tribe} 승\n");
                    break;
                }
                else if (character2.n_HP <= 0)
                {
                    Console.WriteLine($"{character1.str_Tribe} 승\n");
                    break;
                }

                if (character1.b_Turn)
                {
                    character1.b_Turn = false;
                    character2.b_Turn = true;

                    Console.WriteLine($"{character1.str_Tribe} 턴 입니다.");
                    Console.WriteLine($"행동을 선택해주세요 : (1) 공격, (2) 대기");
                    int Control = int.Parse(Console.ReadLine());
                    if (Control == 1)
                    {
                        Attack_action(character1, character2);
                    }
                    else if (Control == 2)
                    {
                        Console.WriteLine($"{character1.str_Tribe} 대기\n");
                        continue;
                    }
                }
                else if (character2.b_Turn)
                {
                    character1.b_Turn = true;
                    character2.b_Turn = false;

                    Console.WriteLine($"{character2.str_Tribe} 턴 입니다.(자동)");
                    int Control = random.Next(1, 3);
                    if (Control == 1)
                    {
                        Attack_action(character2, character1);
                    }
                    else if (Control == 2)
                    {
                        Console.WriteLine($"{character2.str_Tribe} 대기\n");
                        continue;
                    }
                }
            }
        }
    }
}

// 휴먼 클래스(플레이어) 오크 클래스(적)
// 프로그램 시작시
// 입력으로 공격 방어, 오크 반격(공격값 램덤)