using System;
using System.Collections.Generic;
using System.Text;

namespace _01_Console
{
    class _22_05_17
    {
        static void Main(string[] args)
        {
            // 제어문
            // 조건에 따라 다른 코드를 실행하거나 특정 횟수만큰 반복하도록 하게 하는 문구
            // 조건문 : if, else if, else
            // 반복문 : for, foreach, while, do-while

            // 레벨이 10보다 크면 "고렙이다"를 출력, 10보다 작거나 같으면 "저렙이다"
            int userLevel = 15;

            if (userLevel > 10)
                Console.WriteLine("고렙이다~~~~~~~");
            else
                Console.WriteLine("저렙이다~~~~~~~");

            // 레벨이 10 이하면 저렙, 11~20 이면 중렙, 21~30이면 고렙
            if (userLevel <= 10)
                Console.WriteLine("저렙이다");
            else if (userLevel > 10 && userLevel <= 20)
                Console.WriteLine("중렙이다");
            else
                Console.WriteLine("고렙이다");

            // 반복문
            // while, for
            int Count_while = 0;
            while(Count_while < 10)
            {
                Console.WriteLine($"while : {Count_while}");
                Count_while++;
            }

            for (int Count_for = 0; Count_for < 10; Count_for++)
            {
                Console.WriteLine($"for : {Count_while}");
            }

        }
    }
}
