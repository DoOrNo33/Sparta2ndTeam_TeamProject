using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.skill
{
    internal class Player
    {
        static public List<Skill> skill = new List<Skill>();
        Random random = new Random();
        public string Name { get; }
        public int Job { get; protected set; }
        public int Level { get; } = 1;
        public int Atk { get; protected set; } = 10; 
        public int Def { get; protected set; } = 5; 
        public int Hp { get; set; }
        public int Mp { get; set; }
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
            Hp = 150;
            Mp = 50;

            skill.Add(new Skill("알파-스트라이크", 10, Atk * 2, 1, 1));
            skill.Add(new Skill("더블-스트라이크", 25, Atk * 2, 2, 2));

        }
    }

    internal class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            Job = 2;
            Atk = 12; // 마법사의 공격력 설정
            Def = 6; // 마법사의 방어력 설정
            Hp = 100;
            Mp = 150;
            skill.Add(new Skill("에너지 볼트", 10, Atk * 1, 2, 1));
            skill.Add(new Skill("썬더 볼트", 25, Atk * 2, 1, 2));
        }

    }
}
