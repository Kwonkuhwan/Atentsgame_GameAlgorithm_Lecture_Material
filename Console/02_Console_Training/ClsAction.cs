using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class Character_Info
    {
        Random random = new Random();

        private string str_UserName;
        private int n_HP;
        private int n_HP_Max;

        private int n_Attack;

        private int n_Magic_Count;
        private int n_Magic_Heal;

        private bool b_Turn;
        private bool b_Shield;

        private int n_Level;
        private int n_Str;
        private int n_Dex;
        private int n_Int;

        public void Init(string str_username, int n_hp)
        {
            str_UserName = str_username;
            n_HP = n_hp;
            n_HP_Max = n_hp;

            n_Magic_Count = 3;
            n_Attack = 0;
            n_Magic_Heal = 30;

            b_Turn = true;
            b_Shield = false;

            n_Level = 1;
            n_Str = random.Next(1, 6);
            n_Dex = random.Next(1, 6);
            n_Int = random.Next(1, 6);
        }

        // 프로퍼티 : 특수한 함수(메서드)
        public string UserName
        {
            get => str_UserName;
            set => str_UserName = value;
            //get
            //{
            //    return str_Tribe;
            //}
            //set
            //{
            //    str_Tribe = value;
            //}
        }

        public int HealthPoint
        {
            get     // n_HP을 읽을 때 실행 되는 함수.
            {
                return n_HP;
            }
            set     // n_HP 프로퍼티에 값을 넣을 때 실행되는 함수. 설정되는 값은 value라는 키워드에 들어있다.
            {
                n_HP = value;

                //if (n_HP > n_HP_Max)
                //    n_HP = n_HP_Max;
                //else if (n_HP < 0)
                //    n_HP = 0;

                // 최소 값과 최대 값 사이의 값은 그래도 리턴
                // 최소 값 아래의 값은 최소 값으로 리턴
                // 최대 값 위의 값은 최대 값으로 리턴 
                n_HP = Math.Clamp(n_HP, 0, n_HP_Max);
            }
        }

        public int AttackDamage
        {
            get
            {
                return n_Attack;
            }
            set
            {
                n_Attack = value;
            }
        }

        public int MaicCount
        {
            get
            {
                return n_Magic_Count;
            }
            set
            {
                n_Magic_Count = value;
            }
        }

        public int MagicHeal
        {
            get
            {
                return n_Magic_Heal;
            }
            set
            {
                n_Magic_Heal = value;
            }
        }

        public bool Turn
        {
            get
            {
                return b_Turn;
            }
            set
            {
                b_Turn = value;
            }
        }

        public bool Shield
        {
            get
            {
                return b_Shield;
            }
            set
            {
                b_Shield = value;
            }
        }

        public int Level
        {
            get
            {
                return n_Level;
            }
            set
            {
                n_Level = value;
            }
        }

        public int Strength
        {
            get
            {
                return n_Str;
            }
            set
            {
                n_Str = value;
            }
        }

        public int Dexterity
        {
            get
            {
                return n_Dex;
            }
            set
            {
                n_Dex = value;
            }
        }

        public int Intelligence
        {
            get
            {
                return n_Int;
            }
            set
            {
                n_Int = value;
            }
        }
    }

    class Action
    {
        Random random = new Random();
        public void Current_Status(Character_Info character1)
        {
            Console.WriteLine($"┌ 스테이터스 ────────────────────────┐");
            Console.WriteLine($"│ 이름 : [{character1.UserName}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 레벨 : [{character1.Level}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   힘 : [{character1.Strength}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│ 민첩 : [{character1.Dexterity}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 지능 : [{character1.Intelligence}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n\n");
        }

        public void LevelUp(Character_Info character1)
        {
            // 입력받은 횟수만큼 레벨업을 하는 코드 만들기
            Console.Write("레벨업 할 수를 입력해주세요 : ");
            int input_LevelUp = int.Parse(Console.ReadLine());

            int levelUp_Count = 0;

            Console.WriteLine($"┌ 스테이터스 ────────────────────────┐");
            Console.WriteLine($"│ 이름 : [{character1.UserName}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 레벨 : [{character1.Level}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   힘 : [{character1.Strength}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│ 민첩 : [{character1.Dexterity}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 지능 : [{character1.Intelligence}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n");

            while (levelUp_Count < input_LevelUp)
            {
                character1.Level++;
                character1.Strength++;
                character1.Dexterity++;
                character1.Intelligence++;
                levelUp_Count++;
            }

            Console.WriteLine($"┌ 바뀐 스테이터스 ───────────────────┐");
            Console.WriteLine($"│ 이름 : [{character1.UserName}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 레벨 : [{character1.Level}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   힘 : [{character1.Strength}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│ 민첩 : [{character1.Dexterity}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 지능 : [{character1.Intelligence}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n\n");

        }
        private void Attack_action(Character_Info character1, Character_Info character2)
        {
            character1.AttackDamage = random.Next(1, 20);

            int attack_Sccuess = random.Next(0, 15);

            if (attack_Sccuess > 3)
            {
                if (character2.Shield)
                {
                    Console.WriteLine($"[ {character2.UserName} 방어 성공 ]\n");
                    int oldAttack = character1.AttackDamage;
                    character1.AttackDamage /= 2;
                    Console.WriteLine($"[ {character1.UserName} 데미지 {oldAttack} -> {character1.AttackDamage} 변경 ]\n");
                }

                Console.WriteLine($"{character1.UserName} 공격 : [{character1.AttackDamage}]\n");
                Take_Damege(character1, character2);
            }
            else
            {
                Console.WriteLine($"[ {character1.UserName} 공격 실패... ]\n");
            }
        }

        private void Take_Damege(Character_Info character1, Character_Info character2)
        {
            int old_character2_HP = character2.HealthPoint;
            character2.HealthPoint -= character1.AttackDamage;

            Console.WriteLine($"[ {character2.UserName} hp : {old_character2_HP} -> {character2.HealthPoint} ]\n");
        }

        private bool Magic_Heal(Character_Info character1)
        {
            if (character1.MaicCount <= 0)
            { 
                Console.WriteLine($"[ {character1.UserName} 남은 Magic Count가 없습니다. ]\n");
                return false;
            }

            int old_character_HP = character1.HealthPoint;
            character1.HealthPoint += character1.MagicHeal;
            character1.MaicCount -= 1;

            Console.WriteLine($"[ {character1.UserName} 남은 Magic Count : {character1.MaicCount} ]\n");
            Console.WriteLine($"[ {character1.UserName} hp : {old_character_HP} -> {character1.HealthPoint} ]\n");

            return true;
        }

        private bool Run()
        {
            int n_Run_Success = random.Next(0, 15);

            if (n_Run_Success > 5)
                return false;
            else
                return true;
        }
        public void Attack(Character_Info character1, Character_Info character2)
        {
            character1.Turn = Convert.ToBoolean(random.Next(0, 2));        // 2보다 작은 수 리턴

            if (character1.Turn)
                character2.Turn = false;
            else
                character2.Turn = true;

            if (character1.Turn)
                Console.WriteLine($"지나가는 {character2.UserName}에게 싸움를 걸었다.\n");
            else
                Console.WriteLine($"지나가는 {character2.UserName}가 싸움를 걸었다.\n");

            while (true)
            {
                if (character1.HealthPoint <= 0)
                {
                    Console.WriteLine($"{character2.UserName}(이)가 {character1.UserName}을 처참히 죽였다.\n");
                    break;
                }
                else if (character2.HealthPoint <= 0)
                {
                    Console.WriteLine($"{character1.UserName}(이)가 {character2.UserName}을 처참히 죽였다.\n");
                    break;
                }

                if (character1.Turn)
                {
                    character1.Turn = false;
                    character2.Turn = true;
                    character1.Shield = false;

                    Console.WriteLine($"{character1.UserName} 턴 입니다.");
                    Console.WriteLine($"행동을 선택해주세요 : (1) 공격, (2) 힐, (3) 1턴 방어 [공격 1/2], (4) 턴 넘기기, (5) 도망간다");

                    int Control = -1;
                    try
                    {
                        Control = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        continue;
                    }

                    if (Control == 1)
                    {
                        Attack_action(character1, character2);
                    }
                    else if (Control == 2)
                    {
                        if(!Magic_Heal(character1))
                        {
                            character1.Turn = true;
                            character2.Turn = false;
                        }
                               
                    }
                    else if (Control == 3)
                    {
                        Console.WriteLine($"{character1.UserName} 방어\n");
                        character1.Shield = true;
                        continue;
                    }
                    else if (Control == 4)
                    {
                        Console.WriteLine($"{character1.UserName} 대기\n");
                        continue;
                    }
                    else if (Control == 5)
                    {
                        Console.WriteLine($"{character1.UserName} 도망\n");
                        if (Run())
                        {
                            Console.WriteLine($"{character1.UserName} 도망 성공\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{character1.UserName} 도망 실패\n");
                            continue;
                        }
                    }
                }
                else if (character2.Turn)
                {
                    character1.Turn = true;
                    character2.Turn = false;
                    character2.Shield = false;

                    Console.WriteLine($"{character2.UserName} 턴 입니다. (자동)");
                    int Control = random.Next(1, 4);
                    if (Control == 1)
                    {
                        Attack_action(character2, character1);
                    }
                    else if (Control == 2)
                    {
                        Console.WriteLine($"{character2.UserName} 방어\n");
                        character2.Shield = true;
                        continue;
                    }
                    else if (Control == 3)
                    {
                        Console.WriteLine($"{character2.UserName} 대기\n");
                        continue;
                    }
                }
            }
        }
    }
}
