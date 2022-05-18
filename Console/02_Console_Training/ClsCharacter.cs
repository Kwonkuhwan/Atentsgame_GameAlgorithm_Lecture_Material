using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class ClsCharacter
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

        public ClsCharacter()
        {
            UserName = "";
            HealthPoint = 100;
            MaxHealthPoint = 100;

            Level = 1;
            Strength = random.Next(1, 6);
            Dexterity = random.Next(1, 6);
            Intelligence = random.Next(1, 6);

            MagicCount = 3;
            AttackDamage = n_Str + n_Dex + n_Int;
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
            Intelligence = random.Next(1, 6);

            MagicCount = 3;
            AttackDamage = n_Str + n_Dex + n_Int;
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

        public int Intelligence
        {
            get => n_Int;
            set => n_Int = value;
        }
    }
}
