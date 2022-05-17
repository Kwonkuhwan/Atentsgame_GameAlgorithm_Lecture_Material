using System;

namespace _01_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            // int, float, bool, string
            // int 정수 -> 소수점이 없는 숫자
            // float 실수 -> 소수점이 있는 숫자
            // bool 불 -> true 아니면 false만 가지는 변수
            // string 문자열 -> ""안에 있는 문장을 저장하는 변수

            // 변수의 이름은 알파벳으로 시작한다. _와 숫자를 붙일 수 있다.
            // 변수의 이름은 단어의 의미만으로도 알아볼 수 있도록 작성하는 것을 권장.

            // 변수 작성법
            // (변수의 종류) (변수의 이름) = (초기값);

            int strlength;
            int dex = 20;
            int intelligence = 10;

            float exp1 = 35;        // 운이 좋았다...? C#이 int타입을 자동으로 float변경한 경우
            float exp2 = 35f;       // 정답
            float exp3 = 35.0f;     // 정답

            bool t = true;
            bool f = false;

            string My_Name = "권구환";

            int level = 5;
            float exp = 95.0f;
            string C_Name = "인간";

            // 비효율적인 방법(메모리 낭비)
            Console.WriteLine("나는 " + C_Name + "이고 레벨은 " + level + "고 경험치는 " + exp + "%이다.");

            // 적당히 쓸만한 방법
            Console.WriteLine("나는 {0}이고 레벨은 {1}고 경험치는 {2}%이다.", C_Name, level, exp);
            
            // 가장 최신 방법
            Console.WriteLine($"나는 {C_Name}이고 레벨은 {level}고 경험치는 {exp}%이다.");

        }
    }
}
