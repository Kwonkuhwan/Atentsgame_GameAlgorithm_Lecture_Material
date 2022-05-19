using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class ClsHuman : ClsCharacter
    {
        protected int n_Int;

        public ClsHuman(string username) : base()
        {
            if (username == "")
                UserName = "사용자";
            else
                UserName = username;

            Intelligence = random.Next(1, 6);

            AttackDamage = Strength + Dexterity + Intelligence;
        }
        public int Intelligence
        {
            get => n_Int;
            set => n_Int = value;
        }

        public override void Current_Status()
        {
            Console.WriteLine($"┌ 스테이터스 ────────────────────────┐");
            Console.WriteLine($"│   이름 : [{UserName}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   레벨 : [{Level}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   체력 : [{HealthPoint}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│     힘 : [{Strength}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│   민첩 : [{Dexterity}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   지능 : [{Intelligence}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 공격력 : [{AttackDamage}]".PadRight(34, ' ') + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n\n");
        }

        public override void LevelUp()
        {
            // 입력받은 횟수만큼 레벨업을 하는 코드 만들기
            Console.Write("레벨업 할 수를 입력해주세요 : ");
            int input_LevelUp = int.Parse(Console.ReadLine());
            Console.Write("\n");

            int levelUp_Count = 0;

            Console.WriteLine("현재 스테이터스 표시 : ");
            Current_Status();

            while (levelUp_Count < input_LevelUp)
            {
                Level++;
                Strength++;
                Dexterity++;
                Intelligence++;
                levelUp_Count++;
            }
            AttackDamage = Strength + Dexterity + Intelligence;

            Console.WriteLine("바뀐 스테이터스 표시 : ");
            Current_Status();
        }
    }
}
