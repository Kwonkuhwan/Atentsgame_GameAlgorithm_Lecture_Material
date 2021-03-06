using System;
using System.Collections.Generic;
using System.Text;

namespace _02_Console_Training
{
    class ClsAction
    {
        Random random = new Random();
              
        private void Attack_action(ClsCharacter character1, ClsCharacter character2)
        {
            //character1.AttackDamage = random.Next(1, 20);

            int attack_Sccuess = random.Next(0, 15);
            if (attack_Sccuess > 3)
            {               
                Take_Damege(character1, character2);
            }
            else
            {
                Console.WriteLine($"[ {character1.UserName} 공격 실패... ]\n");
            }
        }

        private void Take_Damege(ClsCharacter character1, ClsCharacter character2)
        {
            int AttackDamage = character1.AttackDamage;
            if (character2.Shield)
            {
                Console.WriteLine($"[ {character2.UserName} 방어 성공 ]\n");
                int oldAttack = AttackDamage;
                AttackDamage /= 2;
                Console.WriteLine($"[ {character1.UserName} 데미지 {oldAttack} -> {AttackDamage} 변경 ]\n");
            }

            int critical = random.Next(0, 100);
            if (critical != 99)
            {
                double crit = random.NextDouble();
                AttackDamage = (int)(AttackDamage * (1.0f + crit));
                Console.WriteLine($"{character1.UserName} 크리티컬 공격[{(1.0f + crit):F2}] : [{AttackDamage}]\n");
            }
            else
                Console.WriteLine($"{character1.UserName} 공격 : [{AttackDamage}]\n");

            int old_character2_HP = character2.HealthPoint;
            character2.HealthPoint -= AttackDamage;

            Console.WriteLine($"[ {character2.UserName} hp : {old_character2_HP} -> {character2.HealthPoint} ]\n");
        }

        private bool Magic_Heal(ClsCharacter character1)
        {
            if (character1.MagicCount <= 0)
            { 
                Console.WriteLine($"[ {character1.UserName} 남은 Magic Count가 없습니다. ]\n");
                return false;
            }

            int old_character_HP = character1.HealthPoint;
            character1.HealthPoint += character1.MagicHeal;
            character1.MagicCount -= 1;

            Console.WriteLine($"[ {character1.UserName} 남은 Magic Count : {character1.MagicCount} ]\n");
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
        public void Attack(ClsCharacter character1, ClsCharacter character2)
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
                    Console.WriteLine($"");

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
