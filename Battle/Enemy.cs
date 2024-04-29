using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Enemy
    {
        private string name;
        private int id;
        private int lv;
        private int hp;
        private int atk;
        private bool isDead;

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public int Lv { get { return lv; } }
        public int Hp { get { return hp; } }
        public int Atk { get { return atk; } }
        public bool IsDead { get { return isDead; } }

        public Enemy(string sName, int sId, int sLv, int sHp, int sAtk, bool sIsDead = false)
        {
            name = sName;
            id = sId;
            lv = sLv;
            hp = sHp;
            atk = sAtk;
            isDead = sIsDead;
        }

        public void PrintCurrentEnemies(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" {0} ", idx);
                Console.ResetColor();
            }
            else
            {
                Console.Write(" - ");
            }
            Console.Write("Lv.{0} ", Lv);
            Console.Write("{0} ", Name);
            if (!IsDead)
            {
                Console.WriteLine("HP {0}", Hp);
            }
            else
            {
                Console.WriteLine("Dead");
            }
        }

        public void PlayerAttack(int pAtk)                  // 플레이어 공격 체크
        {
            Console.Write("Lv.{0} {1} 을(를) 맞췄습니다.", Lv, Name);
            Console.WriteLine("데미지 : {0}", pAtk);
            Console.WriteLine("\nLv.{0} {1}", Lv, Name);

            int tempHp = hp;
            hp -= pAtk;
            
            if (hp > 0)   // 적의 남은 hp가 0보다 큰지 작은지
            {
                Console.WriteLine("HP {0} -> {1}", tempHp, hp);
            }
            if (hp <= 0)
            {
                hp = 0;
                Dead();
                Console.WriteLine("HP {0} -> {1}", tempHp, hp);
            }

            Console.WriteLine("\n0. 다음");
            Console.Write("\n>>");

            switch ((PlayerAttackPhase)ConsoleUtility.PromptMenuChoice(0, 0))
            {
                case PlayerAttackPhase.ToEnemyPhase:
                    break;
            }
        }

        private void Dead()
        {
            isDead = true;
        }

        public enum PlayerAttackPhase
        {
            ToEnemyPhase
        }
    }
}
