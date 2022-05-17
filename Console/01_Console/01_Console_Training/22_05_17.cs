using System;

namespace _01_Console_Training
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = "";
            int userLevel = 0;
            float userExp = 0.0f;
            int userStr = 0;
            int userDex = 0;
            int userInt = 0;

            int inputindex = 0;

            try
            {
                Console.Write($"이름을 입력해주세요 : ");
                userName = Console.ReadLine();

                inputindex++;
                Console.Write($"레벨을 입력해주세요 : ");
                userLevel = int.Parse(Console.ReadLine());

                inputindex++;
                Console.Write($"경험치을 입력해주세요 : ");
                userExp = float.Parse(Console.ReadLine());

                inputindex++;
                Console.Write($"힘을 입력해주세요 : ");
                userStr = int.Parse(Console.ReadLine());

                inputindex++;
                Console.Write($"민첩을 입력해주세요 : ");
                userDex = int.Parse(Console.ReadLine());

                inputindex++;
                Console.Write($"지능을 입력해주세요 : ");
                userInt = int.Parse(Console.ReadLine());
            }
            catch
            {
                switch (inputindex)
                {
                    case 0:
                        Console.WriteLine($"이름 입력 에러");                        
                        break;
                    case 1:
                        Console.WriteLine($"레벨 입력 에러");
                        break;
                    case 2:
                        Console.WriteLine($"경험치 입력 에러");
                        break;
                    case 3:
                        Console.WriteLine($"힘 입력 에러");
                        break;
                    case 4:
                        Console.WriteLine($"민첩 입력 에러");
                        break;
                    case 5:
                        Console.WriteLine($"지능 입력 에러");
                        break;
                }
                return;
            }

            Console.WriteLine($"나의 이름은 {userName}이고 레벨은 {userLevel}이며 경험치는{userExp}%이다.\n힘 : {userStr}, 민첩 : {userDex}, 지능 : {userInt}");
        }
    }
}
