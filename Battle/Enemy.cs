using Sparta2ndTeam_TeamProject.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Enemy
    {
        protected string name;
        protected int id;
        protected int lv;
        protected int hp;
        protected int atk;
        protected bool isDead = false;
        protected int exp;
        protected int[] drops = new int[2];
        protected Random random = new Random();

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public int Lv { get { return lv; } }
        public int Hp { get { return hp; } }
        public int Atk { get { return atk; } }
        public bool IsDead { get { return isDead; } }
        public int[] Drops { get { return drops; } }
        public int Exp { get { return exp; } }



        protected virtual void SetLevel()
        {
            lv += 0;
            hp += 0;
            atk += 0;
            exp += 0;
        }

        public void PrintCurrentEnemies(bool withNumber = false, int idx = 0)
        {
            if (isDead)     // 죽었다면 모두 검은색으로 처리
            { 
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (withNumber)
                {
                    Console.Write("{0:D2} ", idx);
                }
                else
                {
                    Console.Write("-  ");
                }
                Console.Write("Lv. {0} ", Lv);
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 13));
                Console.WriteLine(" 처치");
                Console.ResetColor();
            }
            else                                // 아니라면
            {
                if (withNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0:D2} ", idx);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("-  ");
                }
                Console.Write("Lv. {0} ", Lv);
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 13));
                Console.WriteLine("HP {0}", Hp);
            }
        }

        public int PlayerAttack()                  // 플레이어 공격 체크
        {
            string pName = GameManager.player.Name;
            int adAtk = (int)Math.Ceiling(GameManager.player.Atk * 0.1f);                                   //보정 공격, 10%의 올림치
            int pAtk = random.Next((GameManager.player.Atk - adAtk), (GameManager.player.Atk + adAtk + 1)); //보정 공격치
            bool isCri = false;

            // 치명타 판정
            if (GameManager.player.Critical())
            {
                pAtk = (int)Math.Ceiling(pAtk * 1.6f);                                                      // 치명타 데미지
                isCri = true;
            }

            // 적의 회피 판정
            if (GameManager.player.Avoid())                 // 적이 회피했다면(10%)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                Console.WriteLine("{0} 의 공격!", pName);
                Console.WriteLine("Lv. {0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", Lv, Name);
                ConsoleUtility.PromptReturn();
                return 0;
            }

            else                                            // 적이 맞았다면
            {

                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                Console.WriteLine("{0} 의 공격!", pName);
                Console.Write("Lv. {0} {1} 을(를) 맞췄습니다.", Lv, Name);
                Console.Write(" [데미지 : {0}]", pAtk);

                if (isCri)
                {
                    Console.Write(" - 치명타 공격!!");
                }

                Console.WriteLine("\n\nLv. {0} {1}", Lv, Name);

                int tempHp = hp;
                hp -= pAtk;

                if (hp > 0)   // 적의 남은 hp가 0보다 큰지 작은지
                {
                    Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                    //Console.Write("HP {0} -> ", tempHp);
                    //ConsoleUtility.AnimationMinus(Console.CursorLeft, Console.CursorTop, tempHp, hp);
                    ConsoleUtility.PromptReturn();
                    return 0;
                }
                else
                {
                    hp = 0;
                    Dead();
                    Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                    //Console.Write("HP {0} -> ", tempHp);
                    //ConsoleUtility.AnimationMinus(Console.CursorLeft, Console.CursorTop, tempHp, hp);
                    ConsoleUtility.PromptReturn();
                    return 1;
                }
            }
            

            


        }

        public void EnemyAttack()  // 플레이어 이름, 플레이어 체력
        {

            if (!isDead && GameManager.player.Hp > 0)
            {
                bool petAvoid = false;

                foreach (Pet pet in GameManager.pets)// 펫 스킬 들어갈 타이밍
                {
                    if (pet.isEquipped)
                    {
                        if (pet.PetType == Items.PetType.Defense)
                        {
                            petAvoid = true;
                        }
                    }
                }

                if (GameManager.player.Avoid(petAvoid))                         // 플레이어 회피
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                    Console.WriteLine("Lv. {0} {1} 의 공격!", Lv, Name);
                    Console.WriteLine("{0} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", GameManager.player.Name);
                    ConsoleUtility.PromptReturn();
                }

                else                                                    // 플레이어 피격
                {
                    float damageReduce = Atk * (1 - (GameManager.player.Def * 0.01f)); // 플레이어의 방어력%만큼 피해 경감
                    if (damageReduce < 2)
                    {
                        damageReduce = 2;
                    }
                    int damageRange = random.Next((int)Math.Ceiling(damageReduce) - 1, (int)Math.Ceiling(damageReduce) + 2);     // 경감 데미지에서 -1 ~ +1 값 설정
                    int adAtk = damageRange;             

                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                    Console.WriteLine("Lv. {0} {1} 의 공격!", Lv, Name);
                    Console.Write("{0} 을(를) 맞췄습니다. ", GameManager.player.Name);
                    Console.WriteLine("[데미지 : {0}]", adAtk);

                    int tempHp = GameManager.player.Hp;
                    GameManager.player.Hp -= adAtk;

                    if (GameManager.player.Hp > 0)   // 플레이어의 남은 hp가 0보다 큰지 작은지
                    {
                        Console.WriteLine("체  력 : {0} -> {1}", tempHp, GameManager.player.Hp);
                    }
                    if (GameManager.player.Hp <= 0)
                    {
                        GameManager.player.Hp = 0;
                        Console.WriteLine("체  력 : {0} -> {1}", tempHp, GameManager.player.Hp);
                    }
                    ConsoleUtility.PromptReturn();
                }
            }
        }

        public void PetDamage(int damage)           // 펫 데미지 적용 용
        {
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        public void Dead()
        {
            isDead = true;
        }

        public enum PlayerPhase
        {
            ToEnemyPhase
        }

        public enum EnemyPhase
        {
            Next
        }


    }
}
