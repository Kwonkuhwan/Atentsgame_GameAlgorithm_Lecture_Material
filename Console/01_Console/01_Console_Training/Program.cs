using System;

namespace _01_Console_Training
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] C_info = new string[6];
            for(int i=0; i<6; i++)
            {
                string info = "";
                switch (i)
                {
                    case 0:
                        info = "이름";
                        break;
                    case 1:
                        info = "레벨";
                        break;
                    case 2:
                        info = "경험치";
                        break;
                    case 3:
                        info = "힘";
                        break;
                    case 4:
                        info = "민첩";
                        break;
                    case 5:
                        info = "지능";
                        break;
                }
                Console.Write($"{info}를 입력해주세요 : ");
                C_info[i] = Console.ReadLine();
            }

            Console.WriteLine($"나의 이름은 {C_info[0]}이고 레벨은 {C_info[1]}이며 경험치는{C_info[2]}%이다.\n힘 : {C_info[3]}, 민첩 : {C_info[4]}, 지능 : {C_info[5]}");
        }
    }
}
