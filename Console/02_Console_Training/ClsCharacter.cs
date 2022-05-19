using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class ClsCharacter
    {
        protected Random random = new Random(DateTime.Now.Millisecond);

        protected string str_UserName;
        protected int n_HP;
        protected int n_HP_Max;

        protected int n_Attack;

        protected int n_Magic_Count;
        protected int n_Magic_Heal;

        protected bool b_Turn;
        protected bool b_Shield;

        protected int n_Level;
        protected int n_Str;
        protected int n_Dex;
        //protected int n_Int;

        public ClsCharacter()
        {
            UserName = "";
            MaxHealthPoint = 100;
            HealthPoint = 100;

            Level = 1;
            Strength = random.Next(1, 6);
            Dexterity = random.Next(1, 6);
            //Intelligence = random.Next(1, 6);

            MagicCount = 3;
            AttackDamage = Strength + Dexterity /* + Intelligence */;
            MagicHeal = 30;

            Turn = true;
            Shield = false;
        }

        public ClsCharacter(string str_username, int n_hp = 100)
        {
            UserName = str_username;
            MaxHealthPoint = n_hp;
            HealthPoint = n_hp;

            Level = 1;
            Strength = random.Next(1, 6);
            Dexterity = random.Next(1, 6);
            //Intelligence = random.Next(1, 6);

            MagicCount = 3;
            AttackDamage = Strength + Dexterity /* + Intelligence */;
            MagicHeal = 30;

            Turn = true;
            Shield = false;
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
                n_HP = Math.Clamp(n_HP, 0, n_HP_Max+1);
            }
        }

        public int MaxHealthPoint
        {
            get => n_HP_Max;            
            set => n_HP_Max = value;
        }

        public int AttackDamage
        {
            get => n_Attack;
            set => n_Attack = value;
        }

        public int MagicCount
        {
            get => n_Magic_Count;
            set => n_Magic_Count = value;
        }

        public int MagicHeal
        {
            get => n_Magic_Heal;
            set => n_Magic_Heal = value;
        }

        public bool Turn
        {
            get => b_Turn;
            set => b_Turn = value;
        }

        public bool Shield
        {
            get => b_Shield;
            set => b_Shield = value;
        }

        public int Level
        {
            get => n_Level;
            set => n_Level = value;
        }

        public int Strength
        {
            get => n_Str;
            set => n_Str = value;
        }

        public int Dexterity
        {
            get => n_Dex;
            set => n_Dex = value;
        }

        //public int Intelligence
        //{
        //    get => n_Int;
        //    set => n_Int = value;
        //}

        public virtual void Current_Status()
        {
            Console.WriteLine($"┌ 스테이터스 ────────────────────────┐");
            Console.WriteLine($"│   이름 : [{UserName}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   레벨 : [{Level}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│   체력 : [{HealthPoint}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│     힘 : [{Strength}]".PadRight(36, ' ') + "│");
            Console.WriteLine($"│   민첩 : [{Dexterity}]".PadRight(35, ' ') + "│");
            //Console.WriteLine($"│   지능 : [{Intelligence}]".PadRight(35, ' ') + "│");
            Console.WriteLine($"│ 공격력 : [{AttackDamage}]".PadRight(34, ' ') + "│");
            Console.WriteLine($"└────────────────────────────────────┘");
            Console.WriteLine($"\n\n");
        }

        public virtual void LevelUp()
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
                //Intelligence++;
                levelUp_Count++;
            }
            AttackDamage = Strength + Dexterity/* + Intelligence*/;

            Console.WriteLine("바뀐 스테이터스 표시 : ");
            Current_Status();
        }
    }
}
