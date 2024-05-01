using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject
{
    internal class Player
    {
        Random random = new Random();
        public string Name { get; }
        public int Job { get; set; }
        public int Level { get; } = 1;
        public int Atk { get; set; } = 10; 
        public int Def { get;  set; } = 5; 
        public int Hp { get; set; } = 100;
        public int Mp { get; set; } = 50;
        public int Gold { get; set; } = 1500;

        public Player(string name)
        {
            Name = name;
        }

        public bool Critical() // 치명타
        {
            int percent = random.Next(0, 101);

            if (percent <= 15)
                return true;

            return false;
        }

        public bool Avoid() // 회피
        {
            int percent = random.Next(0, 11);

            if (percent <= 1)
                return true;

            return false;
        }

    }
    class Warrior : Player
    {
        public Warrior(string name) : base(name)
        {
            Job = 1;
            Atk = 8; // 전사의 공격력 설정
            Def = 10; // 전사의 방어력 설정
        }

        public int Alpha_Strike() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if(Mp < 10)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 10;
            Console.WriteLine("알파-스트라이크 스킬 사용");
            return Atk * 2;
        }

        public int Double_Strike() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 20)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 20;
            Console.WriteLine("더블-스트라이크 스킬 사용");
            return Atk * 2;
        }
    }

    internal class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            Job = 2;
            Atk = 12; // 마법사의 공격력 설정
            Def = 6; // 마법사의 방어력 설정
        }

        public int Energy_Bolt() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 10)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 10;
            Console.WriteLine("에너지 볼트 스킬 사용");
            return Atk * 1;
        }

        public int Thunder_Bolt() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 25)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 25;
            Console.WriteLine("썬더 볼트 스킬 사용");
            return Atk * 2;
        }
    }
}
